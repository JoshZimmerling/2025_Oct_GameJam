using System.Collections.Generic;
using UnityEngine;
using static Constants;

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
        RUBY_FISH,
        STRING_FISH
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

    public static List<CraftableItem> AllCraftableItems = new List<CraftableItem> { 
        //FISHING ROD HANDLES
        new FishingRodHandle("Simple Speedy Handle", "A simple, speedy rod.\n\nFishing Time: 1.25s\nQTE Size: 10%\nQTE Range: 60%-80%", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 5 }, { FishType.BRONZE_FISH, 3 } }, ItemType.FISHING_ROD_HANDLE, null, 1.25f, .1f, .6f, .8f),
        new FishingRodHandle("Simple Steady Handle", "A simple rod to steady any shaky hands.\n\nFishing Time: 1.5s\nQTE Size: 15%\nQTE Range: 60%-80%", new Dictionary<FishType, int> { { FishType.BRONZE_FISH, 4 }, { FishType.IRON_FISH, 1 } }, ItemType.FISHING_ROD_HANDLE, null, 1.5f, .15f, .6f, .8f),
        new FishingRodHandle("Simple Risky Handle", "A simple risky rod for those looking for any edge they can.\n\nFishing Time: 1.75s\nQTE Size: 10%\nQTE Range: 50%-75%", new Dictionary<FishType, int> { { FishType.STONE_FISH, 4 }, { FishType.IRON_FISH, 2 } }, ItemType.FISHING_ROD_HANDLE, null, 1.75f, .1f, .5f, .75f),
        new FishingRodHandle("Intermediate Speedy Handle", "A more advanced speed rod.\n\nFishing Time: 1s\nQTE Size: 15%\nQTE Range: 65%-85%", new Dictionary<FishType, int> { { FishType.SILVER_FISH, 10 } }, ItemType.FISHING_ROD_HANDLE, null, 1f, .15f, .65f, .85f),
        new FishingRodHandle("Intermediate Steady Handle", "A dynamically supported rod for more ease of use.\n\nFishing Time: 1.3s\nQTE Size: 20%\nQTE Range: 60%-80%", new Dictionary<FishType, int> { { FishType.BRONZE_FISH, 12 }, { FishType.GOLD_FISH, 3 } }, ItemType.FISHING_ROD_HANDLE, null, 1.3f, .2f, .6f, .8f),
        new FishingRodHandle("Intermediate Risky Handle", "When a little bit of risk doesn't cut it.\n\nFishing Time: 1.6s\nQTE Size: 15%\nQTE Range: 30%-60%", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 20 }, { FishType.IRON_FISH, 10 }, { FishType.GOLD_FISH, 1 } }, ItemType.FISHING_ROD_HANDLE, null, 1.6f, .15f, .3f, .6f),
        new FishingRodHandle("Gamblers Handle", "LETS GO GAMBLING.\n\nFishing Time: 4.0s\nQTE Size: 5%\nQTE Range: 5%-50%", new Dictionary<FishType, int> { { FishType.IRON_FISH, 7 }, { FishType.SILVER_FISH, 7 }, { FishType.GOLD_FISH, 7 } }, ItemType.FISHING_ROD_HANDLE, null, 4f, .05f, .05f, .5f),
        new FishingRodHandle("Complex Speedy Handle", "For the fastest fishers alive.\n\nFishing Time: 0.75s\nQTE Size: 10%\nQTE Range: 55%-75%", new Dictionary<FishType, int> { { FishType.IRON_FISH, 15 }, { FishType.GOLD_FISH, 10 }, { FishType.DIAMOND_FISH, 4 } }, ItemType.FISHING_ROD_HANDLE, null, .75f, .1f, .55f, .75f),
        new FishingRodHandle("Complex Steady Handle", "You can't miss this one right?\n\nFishing Time: 1.1s\nQTE Size: 25%\nQTE Range: 65%-80%", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 30 }, { FishType.BRONZE_FISH, 20 }, { FishType.SILVER_FISH, 5 }, { FishType.GOLD_FISH, 5 } }, ItemType.FISHING_ROD_HANDLE, null, 1.1f, .25f, .65f, .8f),
        new FishingRodHandle("Complex Risky Handle", "Only for the most talented fishers.\n\nFishing Time: 1.5s\nQTE Size: 10%\nQTE Range: 10%-60%", new Dictionary<FishType, int> { { FishType.GOLD_FISH, 15 }, { FishType.DIAMOND_FISH, 5 } }, ItemType.FISHING_ROD_HANDLE, null, 1.5f, .1f, .1f, .6f),
        new FishingRodHandle("Masters Handle", "Basically just grabbing the fish right out of the water at this point.\n\nFishing Time: 0.5s\nQTE Size: 0%\nQTE Range: 0%", new Dictionary<FishType, int> { { FishType.SAPPHIRE_FISH, 2 }, { FishType.EMERALD_FISH, 2 }, { FishType.RUBY_FISH, 2 } }, ItemType.FISHING_ROD_HANDLE, null, .5f, 0f, 0f, 0f),
        //FISHING ROD SHAFTS
        new FishingRodShaft("Simple Shaft", "A basic piece of hardware to get you going.\n\nFish Type: ALL\nChance to Multiply: 10%\nMutiplier Amount: 2x", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 3 }, { FishType.STONE_FISH, 3 }, { FishType.IRON_FISH, 3 } }, ItemType.FISHING_ROD_SHAFT, null, new List<FishType> { FishType.WOOD_FISH, FishType.STONE_FISH, FishType.BRONZE_FISH, FishType.IRON_FISH, FishType.SILVER_FISH, FishType.GOLD_FISH, FishType.DIAMOND_FISH, FishType.SAPPHIRE_FISH, FishType.EMERALD_FISH, FishType.RUBY_FISH, FishType.STRING_FISH}, 10, 2),
        new FishingRodShaft("Advanced Shaft", "The good stuff.\n\nFish Type: ALL\nChance to Multiply: 20%\nMutiplier Amount: 2x", new Dictionary<FishType, int> { { FishType.BRONZE_FISH, 3 }, { FishType.IRON_FISH, 3 }, { FishType.GOLD_FISH, 3 } }, ItemType.FISHING_ROD_SHAFT, null, new List<FishType> { FishType.WOOD_FISH, FishType.STONE_FISH, FishType.BRONZE_FISH, FishType.IRON_FISH, FishType.SILVER_FISH, FishType.GOLD_FISH, FishType.DIAMOND_FISH, FishType.SAPPHIRE_FISH, FishType.EMERALD_FISH, FishType.RUBY_FISH, FishType.STRING_FISH}, 20, 2),
        new FishingRodShaft("Complex Shaft", "Big brain fish gain.\n\nFish Type: ALL\nChance to Multiply: 30%\nMutiplier Amount: 2x", new Dictionary<FishType, int> { { FishType.SILVER_FISH, 3 }, { FishType.GOLD_FISH, 3 }, { FishType.DIAMOND_FISH, 3 } }, ItemType.FISHING_ROD_SHAFT, null, new List<FishType> { FishType.WOOD_FISH, FishType.STONE_FISH, FishType.BRONZE_FISH, FishType.IRON_FISH, FishType.SILVER_FISH, FishType.GOLD_FISH, FishType.DIAMOND_FISH, FishType.SAPPHIRE_FISH, FishType.EMERALD_FISH, FishType.RUBY_FISH, FishType.STRING_FISH}, 30, 2),
        new FishingRodShaft("Gambler's Shaft", "Greed is good.\n\nFish Type: ALL\nChance to Multiply: 10%\nMutiplier Amount: 3x", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 5 }, { FishType.GOLD_FISH, 5 } }, ItemType.FISHING_ROD_SHAFT, null, new List<FishType> { FishType.WOOD_FISH, FishType.STONE_FISH, FishType.BRONZE_FISH, FishType.IRON_FISH, FishType.SILVER_FISH, FishType.GOLD_FISH, FishType.DIAMOND_FISH, FishType.SAPPHIRE_FISH, FishType.EMERALD_FISH, FishType.RUBY_FISH, FishType.STRING_FISH}, 10, 3),
        new FishingRodShaft("Wood Shaft", "High performance for those woody fellas.\n\nFish Type: WOOD\nChance to Multiply: 50%\nMutiplier Amount: 2x", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 8 }, { FishType.IRON_FISH, 2 } }, ItemType.FISHING_ROD_SHAFT, null, new List<FishType> { FishType.WOOD_FISH }, 50, 2),
        new FishingRodShaft("Hard Shaft", "Rock solid.\n\nFish Type: STONE\nChance to Multiply: 50%\nMutiplier Amount: 2x", new Dictionary<FishType, int> { { FishType.STONE_FISH, 8 }, { FishType.BRONZE_FISH, 2 } }, ItemType.FISHING_ROD_SHAFT, null, new List<FishType> { FishType.STONE_FISH }, 50, 2),
        new FishingRodShaft("Metal Shaft", "Make Carnegie proud.\n\nFish Type: BRONZE + IRON\nChance to Multiply: 50%\nMutiplier Amount: 2x", new Dictionary<FishType, int> { { FishType.STONE_FISH, 5 }, { FishType.BRONZE_FISH, 5 }, { FishType.IRON_FISH, 5 } }, ItemType.FISHING_ROD_SHAFT, null, new List<FishType> { FishType.BRONZE_FISH, FishType.IRON_FISH }, 50, 2),
        new FishingRodShaft("Precious Metal Shaft", "More clang for your buck.\n\nFish Type: SILVER + GOLD\nChance to Multiply: 50%\nMutiplier Amount: 2x", new Dictionary<FishType, int> { { FishType.IRON_FISH, 5 }, { FishType.SILVER_FISH, 5 }, { FishType.GOLD_FISH, 5 } }, ItemType.FISHING_ROD_SHAFT, null, new List<FishType> { FishType.SILVER_FISH, FishType.GOLD_FISH }, 50, 2),
        new FishingRodShaft("Treasure Shaft", "Bling that thing.\n\nFish Type: SAPPHIRE + EMERALD + RUBY\nChance to Multiply: 50%\nMutiplier Amount: 2x", new Dictionary<FishType, int> { { FishType.SAPPHIRE_FISH, 2 }, { FishType.EMERALD_FISH, 2 }, { FishType.RUBY_FISH, 2 } }, ItemType.FISHING_ROD_SHAFT, null, new List<FishType> { FishType.SAPPHIRE_FISH, FishType.EMERALD_FISH, FishType.RUBY_FISH }, 50, 2),
        //FISHING ROD BAIT
        new FishingRodBait("Pickaxe", "Mining in Fishcraft.\n\nFish Type: STONE\nFind Chance Increase: 1.25x", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 5 }, { FishType.STONE_FISH, 4 }, { FishType.IRON_FISH, 1 } }, ItemType.FISHING_ROD_BAIT, null, new List<FishType> { FishType.STONE_FISH }, 1.25f),
        new FishingRodBait("Magnet", "Metal attracts metal.\n\nFish Type: IRON\nFind Chance Increase: 1.5x", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 5 }, { FishType.IRON_FISH, 5 } }, ItemType.FISHING_ROD_BAIT, null, new List<FishType> { FishType.IRON_FISH }, 1.5f),
        new FishingRodBait("Medal", "Fresh off the podium.\n\nFish Type: BRONZE + SILVER + GOLD\nFind Chance Increase: 1.333x", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 10 }, { FishType.STONE_FISH, 20 } }, ItemType.FISHING_ROD_BAIT, null, new List<FishType> { FishType.BRONZE_FISH, FishType.SILVER_FISH, FishType.GOLD_FISH }, 1.333f),
        new FishingRodBait("Shiny Magnet", "Gold plated for extra attraction.\n\nFish Type: IRON + GOLD\nFind Chance Increase: 1.75x", new Dictionary<FishType, int> { { FishType.IRON_FISH, 15 }, { FishType.GOLD_FISH, 5 } }, ItemType.FISHING_ROD_BAIT, null, new List<FishType> { FishType.IRON_FISH, FishType.GOLD_FISH }, 1.75f),
        new FishingRodBait("Recycled Bait", "Reduce, Reuse, Recycle.\n\nFish Type: WOOD + BRONZE + EMERALD\nFind Chance Increase: 2x", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 13 }, { FishType.STONE_FISH, 11 }, { FishType.BRONZE_FISH, 7 }, { FishType.SILVER_FISH, 2 } }, ItemType.FISHING_ROD_BAIT, null, new List<FishType> { FishType.WOOD_FISH, FishType.BRONZE_FISH, FishType.EMERALD_FISH }, 2f),
        new FishingRodBait("Mistletoe", "Tis the season for white, green, and red.\n\nFish Type: SILVER + EMERALD + RUBY\nFind Chance Increase: 2x", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 30 }, { FishType.GOLD_FISH, 3 } }, ItemType.FISHING_ROD_BAIT, null, new List<FishType> { FishType.SILVER_FISH, FishType.EMERALD_FISH, FishType.RUBY_FISH }, 2f),
        new FishingRodBait("Wedding Ring", "It has just the right sparkle to it.\n\nFish Type: DIAMOND\nFind Chance Increase: 1.75x", new Dictionary<FishType, int> { { FishType.GOLD_FISH, 7 }, { FishType.DIAMOND_FISH, 1 } }, ItemType.FISHING_ROD_BAIT, null, new List<FishType> { FishType.DIAMOND_FISH }, 1.75f),
        new FishingRodBait("Donut", "For the boys in blue.\n\nFish Type: IRON + SAPPHIRE + DIAMOND\nFind Chance Increase: 1.5x", new Dictionary<FishType, int> { { FishType.BRONZE_FISH, 25 }, { FishType.SAPPHIRE_FISH, 1 }, { FishType.RUBY_FISH, 1 } }, ItemType.FISHING_ROD_BAIT, null, new List<FishType> { FishType.IRON_FISH, FishType.SAPPHIRE_FISH, FishType.DIAMOND_FISH }, 1.5f),
        //FISHING ROD LINE
        new FishingRodLine("20m Fishing Line", "Some more fishing line to explore what lies below.\n\nFishing Depth: 20m", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 15 }, { FishType.STONE_FISH, 10 }, { FishType.IRON_FISH, 3 }, { FishType.STRING_FISH, 1 } }, ItemType.FISHING_ROD_LINE, null, FishingDepth.D_20_METERS),
        new FishingRodLine("30m Fishing Line", "Metal reinforced fishing line to trek even deeper.\n\nFishing Depth: 30m", new Dictionary<FishType, int> { { FishType.BRONZE_FISH, 25 }, { FishType.SILVER_FISH, 10 }, { FishType.GOLD_FISH, 3 }, { FishType.STRING_FISH, 1 } }, ItemType.FISHING_ROD_LINE, null, FishingDepth.D_30_METERS),
        new FishingRodLine("40m Fishing Line", "To reach the depths.\n\nFishing Depth: 40m", new Dictionary<FishType, int> { { FishType.GOLD_FISH, 30 }, { FishType.DIAMOND_FISH, 10 }, { FishType.STRING_FISH, 1 } }, ItemType.FISHING_ROD_LINE, null, FishingDepth.D_40_METERS),
    };

    public static List<CraftingCategory> AllCraftingCategories = new List<CraftingCategory> {
        new CraftingCategory("Fishing Rod Handles", "Upgrades that will help with handling while fishing. This can be faster overall fishing, or upgrades to the Quick Time Event.", ItemType.FISHING_ROD_HANDLE, null),
        new CraftingCategory("Fishing Rod Shafts", "Shafts can improve your yield when making a successful catch. Some will generally help with catching more fish per cast while others specialize in certain species.", ItemType.FISHING_ROD_SHAFT, null),
        new CraftingCategory("Fishing Rod Baits", "Choosing the right bait can help make sure you find the type of fish you’re looking for.", ItemType.FISHING_ROD_BAIT, null),
        new CraftingCategory("Fishing Rod Lines", "Unlock further depths to find brand new types of fish.", ItemType.FISHING_ROD_LINE, null),
        new CraftingCategory("Wands", "desc", ItemType.WAND, null),
        new CraftingCategory("Active Spells", "desc", ItemType.ACTIVE_SPELL, null),
        new CraftingCategory("Passive Spells", "desc", ItemType.PASSIVE_SPELL, null),
        new CraftingCategory("Charms", "desc", ItemType.CHARM, null),
        new CraftingCategory("Boots", "desc", ItemType.BOOTS, null)
    };

    public class CraftingCategory
    {
        public string categoryName;
        public string categoryDescription;
        public ItemType correspondingItemType;
        public string categoryImageName;

        public CraftingCategory(string name, string desc, ItemType itemType, string image)
        {
            categoryName = name;
            categoryDescription = desc;
            categoryImageName = image;
            correspondingItemType = itemType;
        }
    }

    public class CraftableItem
    {
        public string itemName;
        public string itemDescription;
        public Dictionary<FishType, int> craftingCosts;
        public ItemType itemType;
        public string itemImageName;

        public CraftableItem(string name, string desc, Dictionary<FishType, int> costs, ItemType type, string image)
        {
            itemName = name;
            itemDescription = desc;
            craftingCosts = costs;
            itemType = type;
            itemImageName = image;
        }
    }

    public class FishingRodHandle : CraftableItem
    {
        public float fishingTime;
        public float QTESize;
        public float QTELowerBound;
        public float QTEUpperBound;

        public FishingRodHandle(string name, string desc, Dictionary<FishType, int> costs, ItemType type, string image, float time, float size, float qteLB, float qteUB) : base (name, desc, costs, type, image)
        {
            fishingTime = time;
            QTESize = size;
            QTELowerBound = qteLB;
            QTEUpperBound = qteUB;
        }
    }

    public class FishingRodShaft : CraftableItem
    {
        public List<FishType> fishTypesToMultiply;
        public int chanceToMultiply;
        public int multiplierAmount;

        public FishingRodShaft(string name, string desc, Dictionary<FishType, int> costs, ItemType type, string image, List<FishType> types, int chance, int multi) : base(name, desc, costs, type, image)
        {
            fishTypesToMultiply = types;
            chanceToMultiply = chance;
            multiplierAmount = multi;
        }
    }

    public class FishingRodBait : CraftableItem
    {
        public List<FishType> fishTypesToBait;
        public float baitMultiplier;

        public FishingRodBait(string name, string desc, Dictionary<FishType, int> costs, ItemType type, string image, List<FishType> types, float multi) : base(name, desc, costs, type, image)
        {
            fishTypesToBait = types;
            baitMultiplier = multi;
        }
    }

    public class FishingRodLine : CraftableItem
    {
        public FishingDepth fishingDepth;

        public FishingRodLine(string name, string desc, Dictionary<FishType, int> costs, ItemType type, string image, FishingDepth depth) : base(name, desc, costs, type, image)
        {
            fishingDepth = depth;
        }
    }
}
