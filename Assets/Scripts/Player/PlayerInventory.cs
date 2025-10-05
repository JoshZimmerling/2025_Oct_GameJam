using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public enum FishType
    {
        WOOD_FISH,
        STONE_FISH,
        BRONZE_FISH,
        IRON_FISH,
        SILVER_FISH,
        GOLD_FISH,
        DIAMOND_FISH,
        SAPPHIRE_FISH,
        EMERALD_FISH,
        RUBY_FISH
    }

    private Dictionary<FishType, int> countOfFish;

    private void Start()
    {
        SetupEmptyInventory();
    }

    public void SetupEmptyInventory()
    {
        countOfFish = Enum.GetValues(typeof(FishType)).Cast<FishType>().ToDictionary(fish => fish, fish => 0);
        PrintInventory();
    }

    public void AddFish(FishType fish, int countToAdd)
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
