using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Constants
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

    public enum ItemType
    {
        FISHING_ROD_HANDLE,
        FISHING_ROD_BAIT,
        FISHING_ROD_SHAFT,
        FISHING_ROD_LINE,
        WAND,
        ACTIVE_SPELL,
        PASSIVE_SPELL,
        CHARM,
        BOOTS
    }

    public static List<CraftableItem> AllCraftableItems = new List<CraftableItem> { 
        new CraftableItem("Test Fishing Rod Handle",
            new Dictionary<FishType, int> { 
                { FishType.WOOD_FISH, 5 },
                { FishType.STONE_FISH, 3 }
            },
            ItemType.FISHING_ROD_HANDLE),
    };
}
