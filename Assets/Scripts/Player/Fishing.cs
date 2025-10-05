using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Fishing : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] PolygonCollider2D myCollider;
    [SerializeField] GameObject chargesBar;

    static GameObject fishingBar;
    GameObject fishingBarGreenArea;
    GameObject fishingBarSlider;
    GameObject fishingBarBackground;
    private bool hasInputForQTE;

    PolygonCollider2D playerCollider;
     
    private int fishingChargesNeeded = 5;
    private static float fishingStartTime = -1f;
    private float currentFishingTime = 0f;

    private void Start()
    {
        myCollider = GetComponent<PolygonCollider2D>();
        playerCollider = player.GetComponent<PolygonCollider2D>();

        fishingBar = GameObject.Find("Fishing Bar");
        fishingBarGreenArea = fishingBar.transform.Find("Green Area").gameObject;
        fishingBarSlider = fishingBar.transform.Find("Slider").gameObject;
        fishingBarBackground = fishingBar.transform.Find("Black Background").gameObject;
        fishingBar.SetActive(false);

        EmptyFishingChargesProgress();
    }
    
    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame && playerCollider.IsTouching(myCollider))
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

                if ((int)currentFishingTime == 1)
                {
                    UpdateFishingChargesProgress();
                    ResetFishing();
                }

                if (Keyboard.current.spaceKey.wasPressedThisFrame && hasInputForQTE)
                {
                    hasInputForQTE = false;
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

        fishingBar.SetActive(true);
        fishingBarSlider.transform.localPosition = new Vector3(-.5f, 0, -.2f);
        fishingBarBackground.transform.localScale = new Vector3(0, 1, 1);
        fishingBarBackground.transform.localPosition = new Vector3(-.5f, 0, -.2f);
        SetGreenBox();
     }

    private void UpdateFishingBar(float currentTime)
    {
        fishingBarSlider.transform.localPosition = new Vector3(-.5f + currentTime, 0, -.2f);
        fishingBarBackground.transform.localScale = new Vector3(currentTime, 1, 1);
        fishingBarBackground.transform.localPosition = new Vector3(-.5f +( currentTime/2f), 0, -.2f);
    }

    private void ResetFishing()
    {
        fishingStartTime += currentFishingTime;
        currentFishingTime = 0f;
        hasInputForQTE = true;
        SetGreenBox();
    }

    private void SetGreenBox()
    {
        fishingBarGreenArea.transform.localPosition = new Vector3(Random.Range(0.1f, 0.3f), 0, -.1f);
    }

    private bool CheckForGreenBox()
    {
        float sliderPosition = fishingBarSlider.transform.position.x;
        float greenAreaPostion = fishingBarGreenArea.transform.position.x;
        float greenAreaScale = fishingBarGreenArea.transform.localScale.x;

        Debug.Log(greenAreaPostion - (greenAreaScale / 2f) + " | " + sliderPosition + " | " + greenAreaPostion + (greenAreaScale / 2f));
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
            EmptyFishingChargesProgress();
            //Give Reward
        }
    }
}
