using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Constants;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<FishType, int> countOfFish;
    private List<CraftableItem> craftedItems;
    private List<CraftableItem> equippedItems;

    private void Start()
    {
        SetupStartingInventory();
    }

    public void SetupStartingInventory()
    {
        countOfFish = Enum.GetValues(typeof(FishType)).Cast<FishType>().ToDictionary(fish => fish, fish => 0);
        craftedItems = new List<CraftableItem>();
        equippedItems = new List<CraftableItem>
        {
            //new FishingRodHandle("Starting Handle", "Your every day handle; nothing special..\n\nFishing Time: 1.5s\nQTE Size: 10%\nQTE Range: 60%-80%", new Dictionary<FishType, int>(), ItemType.FISHING_ROD_HANDLE, null, 1.5f, .1f, .6f, .8f),
            //new FishingRodShaft("Starting Shaft", "Your every day shaft; nothing special..\n\nFish Type: ALL\nChance to Multiply: 0%\nMutiplier Amount: N/A", new Dictionary<FishType, int>(), ItemType.FISHING_ROD_SHAFT, null, new List<FishType>(), 0, 1),
            //new FishingRodLine("10m Fishing Line", "Your every day fishing line; nothing special.\n\nFishing Depth: 10m", new Dictionary<FishType, int> (), ItemType.FISHING_ROD_LINE, null, FishingDepth.D_10_METERS),
            new FishingRodHandle("Test Handle", "You should not have found this...\n\nFishing Time: 0.2s\nQTE Size: 100%\nQTE Range: 0%-100%", new Dictionary<FishType, int>(), ItemType.FISHING_ROD_HANDLE, null, .2f, 1f, .5f, .5f),
            new FishingRodShaft("Test Shaft", "You should not have found this...\n\nFish Type: ALL\nChance to Multiply: 50%\nMutiplier Amount: 5x", new Dictionary<FishType, int>(), ItemType.FISHING_ROD_SHAFT, null, new List<FishType> { FishType.WOOD_FISH, FishType.STONE_FISH, FishType.BRONZE_FISH, FishType.IRON_FISH, FishType.SILVER_FISH, FishType.GOLD_FISH, FishType.DIAMOND_FISH, FishType.SAPPHIRE_FISH, FishType.EMERALD_FISH, FishType.RUBY_FISH, FishType.STRING_FISH}, 50, 5),
            new FishingRodLine("Test Fishing Line", "You should not have found this...\n\nFishing Depth: 10m", new Dictionary<FishType, int> (), ItemType.FISHING_ROD_LINE, null, FishingDepth.D_10_METERS),
            new FishingRodBait("Test Fishing Bait", "You should not have found this...\n\nFish Type: SAPPHIRE\n Find Chance Increase: 10x", new Dictionary<FishType, int>(), ItemType.FISHING_ROD_BAIT, null, new List<FishType> { FishType.SAPPHIRE_FISH }, 10f),
        };
    }

    public void AddFish(FishType fish, int countToAdd)
    {
        countOfFish[fish] += countToAdd;
        PrintInventory();
    }

    public bool SpendFish(Dictionary<FishType, int> fishSpent)
    {
        //Check if we have each available resource, if not return false
        foreach (KeyValuePair<FishType, int> fish in fishSpent)
        {
            if (countOfFish[fish.Key] < fish.Value)
            {
                return false;
            }
        }
        //If we have enough of each type of fish, now we can subtract those
        foreach (KeyValuePair<FishType, int> fish in fishSpent)
        {
            countOfFish[fish.Key] -= fish.Value;
        }
        return true;
    }

    public int GetFish(FishType fish)
    {
        return countOfFish[fish];
    }

    public void AddCraftedItem(CraftableItem item)
    {
        craftedItems.Add(item);
        PrintInventory();
    }

    public List<CraftableItem> GetAllCraftedItems()
    {
        return craftedItems;
    }

    public void EquipItem(CraftableItem item)
    {
        CraftableItem equippedItem = GetEquippedItemByItemType(item.itemType);
        if (equippedItem != null)
        {
            UnequipItem(equippedItem);
        }
        equippedItems.Add(item);
        craftedItems.Remove(item);
    }

    public void UnequipItem(CraftableItem item)
    {
        equippedItems.Remove(item);
        craftedItems.Add(item);
    }

    public List<CraftableItem> GetAllEquippedItems()
    {
        return equippedItems;
    }

    public CraftableItem GetEquippedItemByItemType(ItemType itemType)
    {
        foreach (CraftableItem item in equippedItems)
        {
            if(item.itemType == itemType)
            {
                return item;
            }
        }
        return null;
    }

    public bool PlayerOwnsItem(CraftableItem item)
    {
        if(craftedItems.Contains(item) || equippedItems.Contains(item))
        {
            return true;
        }
        return false;
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
