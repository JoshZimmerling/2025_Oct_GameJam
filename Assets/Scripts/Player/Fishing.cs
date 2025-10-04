using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fishing : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] PolygonCollider2D myCollider;
    [SerializeField] GameObject chargesBar;

    PolygonCollider2D playerCollider;
     
    private int fishingChargesNeeded = 5;
    private float fishingStartTime = -1;

    private void Start()
    {
        myCollider = GetComponent<PolygonCollider2D>();
        playerCollider = player.GetComponent<PolygonCollider2D>();

        emptyFishingProgress();
    }
    
    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame && playerCollider.IsTouching(myCollider))
        {
            //When pressed get the start time
            fishingStartTime = Time.time;
        }
        if(fishingStartTime != -1)
        {
            float currentFishingTime = 0f;
            if (Mouse.current.rightButton.wasReleasedThisFrame || !playerCollider.IsTouching(myCollider))
            {
                //Finished Fishing
                fishingStartTime = -1;
            }
            else if (playerCollider.IsTouching(myCollider))
            {
                currentFishingTime += Time.time - fishingStartTime;
                //Display UI with the currentFishingTime
                if ((int)currentFishingTime == 1)
                {
                    updateFishingProgress();
                    fishingStartTime += currentFishingTime;
                    currentFishingTime -= 1f;
                }
            }
        }
    }

    private void emptyFishingProgress()
    {
        foreach (Transform progressBarSquare in chargesBar.transform)
        {
            progressBarSquare.gameObject.SetActive(false);
        }
        fishingChargesNeeded = 5;
    }

    private void updateFishingProgress()
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
            emptyFishingProgress();
            //Give Reward
        }
    }
}
