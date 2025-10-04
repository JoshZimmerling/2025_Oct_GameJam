using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fishing : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] PolygonCollider2D myCollider;

    PolygonCollider2D playerCollider;

    private float fishingChargesNeeded;
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
        if(fishingStartTime != -1 && (Mouse.current.rightButton.wasReleasedThisFrame || !playerCollider.IsTouching(myCollider)))
        {
            int totalFishingTime = (int)(Time.time - fishingStartTime);
            fishingChargesNeeded -= totalFishingTime;
            Debug.Log("Fishing Stopped, " + totalFishingTime + " seconds completed, you still need to fish for " + fishingChargesNeeded + " more seconds.");
            fishingStartTime = -1;
        }
    }
}
