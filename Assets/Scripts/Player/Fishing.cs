using System;
using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Fishing : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject chargesBar;

    static GameObject fishingBar;
    GameObject fishingBarGreenArea;
    GameObject fishingBarSlider;
    GameObject fishingBarBackground;
    GameObject fishingQTEIndicator;
    GameObject fishingRewardImage;
    private bool hasInputForQTE;
    private bool canStartFishing = true;
    private static float fishingStartTime = -1f;
    private float currentFishingTime = 0f;

    PolygonCollider2D playerCollider;
    PolygonCollider2D myCollider;

    Player playerScript;
    PlayerInventory playerInventoryScript;

    //VARIABLES WE CAN CHANGE AS NEEDED
    private int fishingChargesNeeded = 5;
    private float timeToFish = 1.5f;
    private float widthOfGreenZone = 0.1f;
    public FishingDepth fishingDepth = FishingDepth.D_10_METERS;

    public enum FishingDepth
    {
        D_10_METERS,
        D_20_METERS,
        D_30_METERS,
        D_40_METERS
    }

    private void Start()
    {
        myCollider = gameObject.GetComponent<PolygonCollider2D>();
        playerCollider = player.GetComponent<PolygonCollider2D>();
        playerScript = player.GetComponent<Player>();
        playerInventoryScript = playerScript.inventory;

        fishingBar = GameObject.Find("Fishing Bar");
        fishingBarGreenArea = fishingBar.transform.Find("Green Area").gameObject;
        fishingBarSlider = fishingBar.transform.Find("Slider").gameObject;
        fishingBarBackground = fishingBar.transform.Find("Black Background").gameObject;
        fishingQTEIndicator = fishingBar.transform.Find("QTE Available Indicator").GetChild(0).gameObject;
        fishingRewardImage = GameObject.Find("Fishing Reward Image");
        fishingRewardImage.SetActive(false);
        fishingBar.SetActive(false);

        EmptyFishingChargesProgress();
    }
    
    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame && playerCollider.IsTouching(myCollider) && canStartFishing)
        {
            StartFishing();
        }
        if(fishingStartTime != -1f)
        {
            if (Mouse.current.rightButton.wasReleasedThisFrame)
            {
                CancelCurrentFishing();
            }
            else
            {
                currentFishingTime = Time.time - fishingStartTime;
                UpdateFishingBar(currentFishingTime);

                if (currentFishingTime >= timeToFish)
                {
                    UpdateFishingChargesProgress();
                    ResetFishing();
                }

                if (Keyboard.current.spaceKey.wasPressedThisFrame && hasInputForQTE)
                {
                    hasInputForQTE = false;
                    fishingQTEIndicator.SetActive(false);
                    if (CheckForGreenBox() == true)
                    {
                        UpdateFishingChargesProgress();
                        ResetFishing();
                    }
                }
            }
        }
    }

    public static void CancelCurrentFishing()
    {
        if (fishingStartTime != -1)
        {
            fishingStartTime = -1f;
            fishingBar.SetActive(false);
        }
    }

    private void StartFishing()
    {
        fishingStartTime = Time.time;
        hasInputForQTE = true;
        fishingQTEIndicator.SetActive(true);

        fishingBar.SetActive(true);
        fishingBarSlider.transform.localPosition = new Vector3(-.5f, 0, -.2f);
        fishingBarBackground.transform.localScale = new Vector3(0, 1, 1);
        fishingBarBackground.transform.localPosition = new Vector3(-.5f, 0, -.2f);
        SetGreenBox();
     }

    private void UpdateFishingBar(float currentTime)
    {
        fishingBarSlider.transform.localPosition = new Vector3(((-timeToFish / 2) + currentTime) / timeToFish, 0, -.2f);
        fishingBarBackground.transform.localScale = new Vector3(currentTime / timeToFish, 1, 1);
        fishingBarBackground.transform.localPosition = new Vector3(((-timeToFish / 2) + (currentTime/2f)) / timeToFish, 0, -.2f);
    }

    private void ResetFishing()
    {
        fishingStartTime += currentFishingTime;
        currentFishingTime = 0f;
        hasInputForQTE = true;
        fishingQTEIndicator.SetActive(true);
        SetGreenBox();
    }

    private void SetGreenBox()
    {
        fishingBarGreenArea.transform.localPosition = new Vector3(Random.Range(0.1f, 0.3f), 0, -.1f);
        fishingBarGreenArea.transform.localScale = new Vector3(widthOfGreenZone, 1, 1);
    }

    private bool CheckForGreenBox()
    {
        float sliderPosition = fishingBarSlider.transform.position.x;
        float greenAreaPostion = fishingBarGreenArea.transform.position.x;
        float greenAreaScale = fishingBarGreenArea.transform.localScale.x;

        if (sliderPosition < greenAreaPostion + (greenAreaScale / 2f) && sliderPosition > greenAreaPostion - (greenAreaScale / 2f))
            return true;
        return false;
    }

    private void EmptyFishingChargesProgress()
    {
        foreach (Transform progressBarSquare in chargesBar.transform)
        {
            progressBarSquare.gameObject.SetActive(false);
        }
        fishingChargesNeeded = 5;
        fishingRewardImage.SetActive(false);
    }

    private void UpdateFishingChargesProgress()
    {
        fishingChargesNeeded -= 1;

        foreach (Transform progressBarSquare in chargesBar.transform)
        {
            if (progressBarSquare.gameObject.activeSelf == false) {
                progressBarSquare.gameObject.SetActive(true);
                break;
            }
        }
        if (fishingChargesNeeded == 0)
        {
            StartCoroutine(FishingChargesCompleted());
        }
    }

    private IEnumerator FishingChargesCompleted()
    {
        CancelCurrentFishing();
        canStartFishing = false;
        GiveFishingReward();
        yield return new WaitForSeconds(1f);
        EmptyFishingChargesProgress();
        canStartFishing = true;
    }

    private void GiveFishingReward()
    {
        int randomNumber = Random.Range(1, 100);
        fishingRewardImage.SetActive(true);

        switch (fishingDepth)
        {
            case FishingDepth.D_10_METERS:
                if(randomNumber <= 40)
                {
                    playerInventoryScript.AddFish(PlayerInventory.FishType.WOOD_FISH, 1);
                }
                else if (randomNumber <= 70)
                {
                    playerInventoryScript.AddFish(PlayerInventory.FishType.STONE_FISH, 1);
                }
                else if (randomNumber <= 85)
                {
                    playerInventoryScript.AddFish(PlayerInventory.FishType.BRONZE_FISH, 1);
                }
                else if (randomNumber <= 95)
                {
                    playerInventoryScript.AddFish(PlayerInventory.FishType.IRON_FISH, 1);
                }
                else
                {
                    playerInventoryScript.AddFish(PlayerInventory.FishType.SAPPHIRE_FISH, 1);
                }
                    break;
            case FishingDepth.D_20_METERS:
                break;
            case FishingDepth.D_30_METERS:
                break;
            case FishingDepth.D_40_METERS:
                break;
        }
    }
}
