using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Constants;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<Constants.FishType, int> countOfFish;
    private List<Constants.CraftableItem> craftedItems;
    private List<Constants.CraftableItem> equippedItems;

    private void Start()
    {
        SetupEmptyInventory();
    }

    public void SetupEmptyInventory()
    {
        countOfFish = Enum.GetValues(typeof(Constants.FishType)).Cast<Constants.FishType>().ToDictionary(fish => fish, fish => 0);
        craftedItems = new List<Constants.CraftableItem>();
        equippedItems = new List<Constants.CraftableItem>();
    }

    public void AddFish(Constants.FishType fish, int countToAdd)
    {
        countOfFish[fish] += countToAdd;
        PrintInventory();
    }

    public bool SpendFish(Dictionary<FishType, int> fishSpent)
    {
        //Check if we have each available resource, if not return false
        foreach (KeyValuePair<Constants.FishType, int> fish in fishSpent)
        {
            if (countOfFish[fish.Key] <= fish.Value)
            {
                return false;
            }
        }
        //If we have enough of each type of fish, now we can subtract those
        foreach (KeyValuePair<Constants.FishType, int> fish in fishSpent)
        {
            countOfFish[fish.Key] -= fish.Value;
        }
        return true;
    }

    public int GetFish(Constants.FishType fish)
    {
        return countOfFish[fish];
    }

    public void AddCraftedItem(Constants.CraftableItem item)
    {
        craftedItems.Add(item);
        PrintInventory();
    }

    public List<Constants.CraftableItem> GetAllCraftedItems()
    {
        return craftedItems;
    }

    public void EquipItem(Constants.CraftableItem item)
    {
        foreach (Constants.CraftableItem equippedItem in equippedItems)
        {
            if(equippedItem.itemType == item.itemType)
            {
                UnequipItem(equippedItem);
                equippedItems.Add(item);
                craftedItems.Remove(item);
                return;
            }
        }
        equippedItems.Add(item);
        craftedItems.Remove(item);
    }

    public void UnequipItem(Constants.CraftableItem item)
    {
        equippedItems.Remove(item);
        craftedItems.Add(item);
    }

    public List<Constants.CraftableItem> GetAllEquippedItems()
    {
        return equippedItems;
    }

    private void PrintInventory()
    {
        String fishPrintStatement = "Current Fish: | ";
        String craftedItemsPrintStatement = "Crafted Items: | ";
        String equippedItemsPrintStatement = "Equipped Items: | ";
        foreach (var entry in countOfFish)
        {
            fishPrintStatement += entry.Key + ": " + entry.Value + " | ";
        }
        foreach (var entry in craftedItems)
        {
            craftedItemsPrintStatement += entry.itemName + " | ";
        }
        foreach (var entry in equippedItems)
        {
            equippedItemsPrintStatement += entry.itemName + " | ";
        }
        Debug.Log(fishPrintStatement);
        Debug.Log(craftedItemsPrintStatement);
        Debug.Log(equippedItemsPrintStatement);
    }
}
