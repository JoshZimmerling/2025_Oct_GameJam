
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static Constants;
using Random = UnityEngine.Random;

public class Fishing : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject chargesBar;
    [SerializeField] List<Sprite> fishSpriteList;

    static GameObject fishingBar;
    GameObject fishingBarGreenArea;
    GameObject fishingBarSlider;
    GameObject fishingBarBackground;
    GameObject fishingQTEIndicator;
    GameObject fishingReward;
    GameObject fishingRewardImage;
    GameObject fishingRewardText;
    private bool hasInputForQTE;
    private bool canStartFishing = true;
    private static float fishingStartTime = -1f;
    private float currentFishingTime = 0f;

    PolygonCollider2D playerCollider;
    PolygonCollider2D myCollider;

    Player playerScript;
    PlayerInventory playerInventoryScript;

    private int fishingChargesNeeded = 5;

    private FishingRodHandle equippedHandle;
    private FishingRodShaft equippedShaft;
    //DEFAULT STATS IN CASE STATS DO NOT LOAD
    private float timeToFish = 1.5f;
    private float widthOfGreenZone = 0.1f;
    private float greenZoneLB = 0.6f;
    private float greenZoneUB = 0.8f;
    public FishingDepth fishingDepth = FishingDepth.D_10_METERS;

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
        fishingReward = GameObject.Find("Fishing Reward");
        fishingRewardImage = fishingReward.transform.Find("Fishing Reward Image").gameObject;
        fishingRewardText = fishingReward.transform.Find("Fishing Reward Text").gameObject;
        fishingReward.SetActive(false);
        fishingBar.SetActive(false);

        EmptyFishingChargesProgress();
    }
    
    void Update()
    {
        // When right click is first pressed, start fishing
        if (Mouse.current.rightButton.wasPressedThisFrame && playerCollider.IsTouching(myCollider) && canStartFishing)
        {
            UpdateFishingStats();
            StartFishing();
        }
        // FishingStartTime will be -1 when we are not fishing, so this check is to check if we are currently fishing
        if(fishingStartTime != -1f)
        {
            // If right click is released cancel the current fishing
            if (Mouse.current.rightButton.wasReleasedThisFrame)
            {
                CancelCurrentFishing();
            }
            // If right click is still held, update the counter for how long we have been fishing, and check if we have compeleted one full fishing charge
            else
            {
                currentFishingTime = Time.time - fishingStartTime;
                UpdateFishingBar(currentFishingTime);

                if (currentFishingTime >= timeToFish)
                {
                    ResetFishing(); 
                    UpdateFishingChargesProgress();
                }

                // Detecting the space bar input for QTE, then checking if it is in the correct area for success
                if (Keyboard.current.spaceKey.wasPressedThisFrame && hasInputForQTE)
                {
                    hasInputForQTE = false;
                    fishingQTEIndicator.SetActive(false);
                    if (CheckForGreenBox() == true)
                    {
                        ResetFishing();
                        UpdateFishingChargesProgress();
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

    private void UpdateFishingStats()
    {
        equippedHandle = (FishingRodHandle) playerInventoryScript.GetEquippedItemByItemType(ItemType.FISHING_ROD_HANDLE);
        equippedShaft = (FishingRodShaft)playerInventoryScript.GetEquippedItemByItemType(ItemType.FISHING_ROD_SHAFT);
        timeToFish = equippedHandle.fishingTime;
        widthOfGreenZone = equippedHandle.QTESize;
        greenZoneLB = equippedHandle.QTELowerBound;
        greenZoneUB = equippedHandle.QTEUpperBound;
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
        fishingBarGreenArea.transform.localPosition = new Vector3(Random.Range(greenZoneLB - 0.5f, greenZoneUB - 0.5f), 0, -.1f);
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
        if (fishingStartTime != -1f)
        {
            fishingBar.SetActive(true);
        }
        fishingChargesNeeded = 5;
        fishingReward.SetActive(false);
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
        FishType fishCaught = FishType.WOOD_FISH; //Could not be left blank so needed a default, it should never use this
        int numFishCaught = 1; //Set this later

        switch (fishingDepth)
        {
            case FishingDepth.D_10_METERS:
                if(randomNumber <= 40)
                {
                    fishCaught = FishType.WOOD_FISH;
                }
                else if (randomNumber <= 70)
                {
                    fishCaught = FishType.STONE_FISH;
                }
                else if (randomNumber <= 85)
                {
                    fishCaught = FishType.BRONZE_FISH;
                }
                else if (randomNumber <= 95)
                {
                    fishCaught = FishType.IRON_FISH;
                }
                else
                {
                    fishCaught = FishType.SAPPHIRE_FISH;
                }
                break;
            case FishingDepth.D_20_METERS:
                if (randomNumber <= 40)
                {
                    fishCaught = FishType.BRONZE_FISH;
                }
                else if (randomNumber <= 70)
                {
                    fishCaught = FishType.IRON_FISH;
                }
                else if (randomNumber <= 85)
                {
                    fishCaught = FishType.SILVER_FISH;
                }
                else if (randomNumber <= 95)
                {
                    fishCaught = FishType.GOLD_FISH;
                }
                else
                {
                    fishCaught = FishType.EMERALD_FISH;
                }
                break;
            case FishingDepth.D_30_METERS:
                if (randomNumber <= 50)
                {
                    fishCaught = FishType.SILVER_FISH;
                }
                else if (randomNumber <= 85)
                {
                    fishCaught = FishType.GOLD_FISH;
                }
                else if (randomNumber <= 95)
                {
                    fishCaught = FishType.DIAMOND_FISH;
                }
                else
                {
                    fishCaught = FishType.RUBY_FISH;
                }
                break;
            case FishingDepth.D_40_METERS:
                break;
        }
        if (equippedShaft.fishTypesToMultiply.Contains(fishCaught) && Random.Range(1,100) < equippedShaft.chanceToMultiply)
        {
            numFishCaught = equippedShaft.multiplierAmount;
        }
        playerInventoryScript.AddFish(fishCaught, numFishCaught);
        fishingReward.SetActive(true);
        fishingRewardImage.GetComponent<SpriteRenderer>().sprite = fishSpriteList[(int)fishCaught];
        fishingRewardText.GetComponent<TextMeshPro>().text = "+" + numFishCaught;
    }
}
