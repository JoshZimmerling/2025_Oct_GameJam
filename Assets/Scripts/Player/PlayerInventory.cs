using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<Constants.FishType, int> countOfFish;

    private void Start()
    {
        SetupEmptyInventory();
    }

    public void SetupEmptyInventory()
    {
        countOfFish = Enum.GetValues(typeof(Constants.FishType)).Cast<Constants.FishType>().ToDictionary(fish => fish, fish => 0);
        PrintInventory();
    }

    public void AddFish(Constants.FishType fish, int countToAdd)
    {
        countOfFish[fish] += countToAdd;
        PrintInventory();
    }

    private void PrintInventory()
    {
        String printStatement = "| ";
        foreach (var entry in countOfFish)
        {
            printStatement += entry.Key + ": " + entry.Value + " | ";
        }
        Debug.Log(printStatement);
    }
}
