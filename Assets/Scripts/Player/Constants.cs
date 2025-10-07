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

    public enum FishingDepth
    {
        D_10_METERS,
        D_20_METERS,
        D_30_METERS,
        D_40_METERS
    }

    public enum ItemType
    {
        FISHING_ROD_HANDLE, // QTE easier, QTE earlier, Fishing Faster
        FISHING_ROD_SHAFT, // Multiplier for certain catch type, or a random chance for all catches
        FISHING_ROD_BAIT, // What you catch more of
        FISHING_ROD_LINE, // Fishing Depth
        WAND,
        ACTIVE_SPELL,
        PASSIVE_SPELL,
        CHARM,
        BOOTS
    }

    public static List<UICraftableItem> AllCraftableItems = new List<UICraftableItem> { 
        new UICraftableItem("Test Fishing Rod Handle", "desc", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 5 }, { FishType.STONE_FISH, 3 } },ItemType.FISHING_ROD_HANDLE, null),
        new UICraftableItem("Test Fishing Rod Handle 2", "desc", new Dictionary<FishType, int> { { FishType.IRON_FISH, 12 }, { FishType.SAPPHIRE_FISH, 1 } },ItemType.FISHING_ROD_HANDLE, null),
    };

    public static List<UICraftingCategory> AllCraftingCategories = new List<UICraftingCategory> {
        new UICraftingCategory("Fishing Rod Handles", "Upgrades that will help with handling while fishing. This can be faster overall fishing, or upgrades to the Quick Time Event.", ItemType.FISHING_ROD_HANDLE, null),
        new UICraftingCategory("Fishing Rod Shafts", "desc", ItemType.FISHING_ROD_SHAFT, null),
        new UICraftingCategory("Fishing Rod Baits", "desc", ItemType.FISHING_ROD_BAIT, null),
        new UICraftingCategory("Fishing Rod Lines", "desc", ItemType.FISHING_ROD_LINE, null),
        new UICraftingCategory("Wands", "desc", ItemType.WAND, null),
        new UICraftingCategory("Active Spells", "desc", ItemType.ACTIVE_SPELL, null),
        new UICraftingCategory("Passive Spells", "desc", ItemType.PASSIVE_SPELL, null),
        new UICraftingCategory("Charms", "desc", ItemType.CHARM, null),
        new UICraftingCategory("Boots", "desc", ItemType.BOOTS, null)
    };

    public class UICraftingCategory
    {
        public string categoryName;
        public string categoryDescription;
        public ItemType correspondingItemType;
        public Sprite categoryImage;

        public UICraftingCategory(string name, string desc, ItemType itemType, Sprite image)
        {
            categoryName = name;
            categoryDescription = desc;
            categoryImage = image;
            correspondingItemType = itemType;
        }
    }

    public class UICraftableItem
    {
        public string itemName;
        public string itemDescription;
        public Dictionary<FishType, int> craftingCosts;
        public ItemType itemType;
        public Sprite itemImage;

        public UICraftableItem(string name, string desc, Dictionary<FishType, int> costs, ItemType type, Sprite image)
        {
            itemName = name;
            itemDescription = desc;
            craftingCosts = costs;
            itemType = type;
            itemImage = image;
        }
    }
}
