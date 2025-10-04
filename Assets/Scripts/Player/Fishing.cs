using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fishing : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] PolygonCollider2D myCollider;

    PolygonCollider2D playerCollider;

    private int fishingChargesNeeded;
    private float fishingStartTime = -1;

    private void Start()
    {
        myCollider = GetComponent<PolygonCollider2D>();
        playerCollider = player.GetComponent<PolygonCollider2D>();

        fishingChargesNeeded = 5;
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
                if((int)currentFishingTime == 1)
                {
                    updateFishingProgress();
                    fishingStartTime += currentFishingTime;
                    currentFishingTime -= 1f;
                }
            }
        }
    }

    private void updateFishingProgress()
    {
        Debug.Log("Fishing progress made, you still need to fish for " + fishingChargesNeeded + " more seconds.");
        fishingChargesNeeded -= 1;
        if(fishingChargesNeeded == 0)
        {
            //Give Reward
        }
    }
}
