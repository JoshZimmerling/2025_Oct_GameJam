using System.Collections.Generic;

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
        PRIMARY_ACTIVE_SPELL,
        SECONDARY_ACTIVE_SPELL,
        PASSIVE_SPELL,
        CHARM,
        BOOTS
    }

    public static List<CraftableItem> AllCraftableItems = new List<CraftableItem> { 
        //FISHING ROD HANDLES
        new FishingRodHandle("Simple Speedy Handle", "A simple, speedy rod.\n\nFishing Time: 1.25s\nQTE Size: 10%\nQTE Range: 60%-80%", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 6 }, { FishType.BRONZE_FISH, 3 } }, ItemType.FISHING_ROD_HANDLE, "handle_simple_speedy", 1.25f, .1f, .6f, .8f),
        new FishingRodHandle("Simple Steady Handle", "A simple rod to steady any shaky hands.\n\nFishing Time: 1.5s\nQTE Size: 15%\nQTE Range: 60%-80%", new Dictionary<FishType, int> { { FishType.BRONZE_FISH, 4 }, { FishType.IRON_FISH, 1 } }, ItemType.FISHING_ROD_HANDLE, "handle_simple_steady", 1.5f, .15f, .6f, .8f),
        new FishingRodHandle("Simple Risky Handle", "A simple rod for those looking for any edge they can.\n\nFishing Time: 1.75s\nQTE Size: 10%\nQTE Range: 45%-70%", new Dictionary<FishType, int> { { FishType.STONE_FISH, 4 }, { FishType.IRON_FISH, 2 } }, ItemType.FISHING_ROD_HANDLE, "handle_simple_risky", 1.75f, .1f, .45f, .7f),
        new FishingRodHandle("Intermediate Speedy Handle", "A more advanced speed rod.\n\nFishing Time: 1s\nQTE Size: 15%\nQTE Range: 65%-85%", new Dictionary<FishType, int> { { FishType.SILVER_FISH, 10 } }, ItemType.FISHING_ROD_HANDLE, "handle_intermediate_speedy", 1f, .15f, .65f, .85f),
        new FishingRodHandle("Intermediate Steady Handle", "A dynamically supported rod for more ease of use.\n\nFishing Time: 1.3s\nQTE Size: 20%\nQTE Range: 60%-80%", new Dictionary<FishType, int> { { FishType.BRONZE_FISH, 12 }, { FishType.GOLD_FISH, 3 } }, ItemType.FISHING_ROD_HANDLE, "handle_intermediate_steady", 1.3f, .2f, .6f, .8f),
        new FishingRodHandle("Intermediate Risky Handle", "When a little bit of risk doesn't cut it.\n\nFishing Time: 1.6s\nQTE Size: 15%\nQTE Range: 30%-60%", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 20 }, { FishType.IRON_FISH, 10 }, { FishType.GOLD_FISH, 1 } }, ItemType.FISHING_ROD_HANDLE, "handle_intermediate_risky", 1.6f, .15f, .3f, .6f),
        new FishingRodHandle("Gamblers Handle", "LETS GO GAMBLING.\n\nFishing Time: 4.0s\nQTE Size: 5%\nQTE Range: 5%-50%", new Dictionary<FishType, int> { { FishType.IRON_FISH, 7 }, { FishType.SILVER_FISH, 7 }, { FishType.GOLD_FISH, 7 } }, ItemType.FISHING_ROD_HANDLE, "handle_gamblers", 4f, .05f, .05f, .5f),
        new FishingRodHandle("Complex Speedy Handle", "For the fastest fishers alive.\n\nFishing Time: 0.75s\nQTE Size: 10%\nQTE Range: 55%-75%", new Dictionary<FishType, int> { { FishType.IRON_FISH, 15 }, { FishType.GOLD_FISH, 10 }, { FishType.DIAMOND_FISH, 4 } }, ItemType.FISHING_ROD_HANDLE, "handle_complex_speedy", .75f, .1f, .55f, .75f),
        new FishingRodHandle("Complex Steady Handle", "You can't miss this one right?\n\nFishing Time: 1.1s\nQTE Size: 25%\nQTE Range: 65%-80%", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 30 }, { FishType.BRONZE_FISH, 20 }, { FishType.SILVER_FISH, 5 }, { FishType.GOLD_FISH, 5 } }, ItemType.FISHING_ROD_HANDLE, "handle_complex_steady", 1.1f, .25f, .65f, .8f),
        new FishingRodHandle("Complex Risky Handle", "Only for the most talented fishers.\n\nFishing Time: 1.5s\nQTE Size: 10%\nQTE Range: 10%-60%", new Dictionary<FishType, int> { { FishType.GOLD_FISH, 15 }, { FishType.DIAMOND_FISH, 5 } }, ItemType.FISHING_ROD_HANDLE, "handle_complex_risky", 1.5f, .1f, .1f, .6f),
        new FishingRodHandle("Masters Handle", "Basically just grabbing the fish right out of the water at this point.\n\nFishing Time: 0.5s\nQTE Size: 0%\nQTE Range: 0%", new Dictionary<FishType, int> { { FishType.SAPPHIRE_FISH, 2 }, { FishType.EMERALD_FISH, 2 }, { FishType.RUBY_FISH, 2 } }, ItemType.FISHING_ROD_HANDLE, "handle_masters", .5f, 0f, 0f, 0f),
        //FISHING ROD SHAFTS
        new FishingRodShaft("Simple Shaft", "A basic piece of hardware to get you going.\n\nFish Type: ALL\nChance to Multiply: 10%\nMutiplier Amount: 2x", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 5 }, { FishType.STONE_FISH, 4 }, { FishType.IRON_FISH, 3 } }, ItemType.FISHING_ROD_SHAFT, "shaft_simple", new List<FishType> { FishType.WOOD_FISH, FishType.STONE_FISH, FishType.BRONZE_FISH, FishType.IRON_FISH, FishType.SILVER_FISH, FishType.GOLD_FISH, FishType.DIAMOND_FISH, FishType.SAPPHIRE_FISH, FishType.EMERALD_FISH, FishType.RUBY_FISH, FishType.STRING_FISH}, 10, 2),
        new FishingRodShaft("Wood Shaft", "High performance for those woody fellas.\n\nFish Type: WOOD\nChance to Multiply: 50%\nMutiplier Amount: 2x", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 10 }, { FishType.IRON_FISH, 2 } }, ItemType.FISHING_ROD_SHAFT, "shaft_wood", new List<FishType> { FishType.WOOD_FISH }, 50, 2),
        new FishingRodShaft("Hard Shaft", "Rock solid.\n\nFish Type: STONE\nChance to Multiply: 50%\nMutiplier Amount: 2x", new Dictionary<FishType, int> { { FishType.STONE_FISH, 8 }, { FishType.BRONZE_FISH, 4 } }, ItemType.FISHING_ROD_SHAFT, "shaft_hard", new List<FishType> { FishType.STONE_FISH }, 50, 2),
        new FishingRodShaft("Metal Shaft", "Make Carnegie proud.\n\nFish Type: BRONZE + IRON\nChance to Multiply: 50%\nMutiplier Amount: 2x", new Dictionary<FishType, int> { { FishType.STONE_FISH, 8 }, { FishType.BRONZE_FISH, 5 }, { FishType.IRON_FISH, 3 } }, ItemType.FISHING_ROD_SHAFT, "shaft_metal", new List<FishType> { FishType.BRONZE_FISH, FishType.IRON_FISH }, 50, 2),
        new FishingRodShaft("Advanced Shaft", "The good stuff.\n\nFish Type: ALL\nChance to Multiply: 20%\nMutiplier Amount: 2x", new Dictionary<FishType, int> { { FishType.BRONZE_FISH, 5 }, { FishType.IRON_FISH, 4 }, { FishType.GOLD_FISH, 3 } }, ItemType.FISHING_ROD_SHAFT, "shaft_advanced", new List<FishType> { FishType.WOOD_FISH, FishType.STONE_FISH, FishType.BRONZE_FISH, FishType.IRON_FISH, FishType.SILVER_FISH, FishType.GOLD_FISH, FishType.DIAMOND_FISH, FishType.SAPPHIRE_FISH, FishType.EMERALD_FISH, FishType.RUBY_FISH, FishType.STRING_FISH}, 20, 2),
        new FishingRodShaft("Gambler's Shaft", "Greed is good.\n\nFish Type: ALL\nChance to Multiply: 10%\nMutiplier Amount: 3x", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 15 }, { FishType.GOLD_FISH, 5 } }, ItemType.FISHING_ROD_SHAFT, "shaft_gamblers", new List<FishType> { FishType.WOOD_FISH, FishType.STONE_FISH, FishType.BRONZE_FISH, FishType.IRON_FISH, FishType.SILVER_FISH, FishType.GOLD_FISH, FishType.DIAMOND_FISH, FishType.SAPPHIRE_FISH, FishType.EMERALD_FISH, FishType.RUBY_FISH, FishType.STRING_FISH}, 10, 3),
        new FishingRodShaft("Precious Metal Shaft", "More clang for your buck.\n\nFish Type: SILVER + GOLD\nChance to Multiply: 50%\nMutiplier Amount: 2x", new Dictionary<FishType, int> { { FishType.IRON_FISH, 5 }, { FishType.SILVER_FISH, 5 }, { FishType.GOLD_FISH, 5 } }, ItemType.FISHING_ROD_SHAFT, "shaft_precious_metal", new List<FishType> { FishType.SILVER_FISH, FishType.GOLD_FISH }, 50, 2),
        new FishingRodShaft("Complex Shaft", "Big brain fish gain.\n\nFish Type: ALL\nChance to Multiply: 30%\nMutiplier Amount: 2x", new Dictionary<FishType, int> { { FishType.SILVER_FISH, 5 }, { FishType.GOLD_FISH, 4 }, { FishType.DIAMOND_FISH, 3 } }, ItemType.FISHING_ROD_SHAFT, "shaft_complex", new List<FishType> { FishType.WOOD_FISH, FishType.STONE_FISH, FishType.BRONZE_FISH, FishType.IRON_FISH, FishType.SILVER_FISH, FishType.GOLD_FISH, FishType.DIAMOND_FISH, FishType.SAPPHIRE_FISH, FishType.EMERALD_FISH, FishType.RUBY_FISH, FishType.STRING_FISH}, 30, 2),
        new FishingRodShaft("Treasure Shaft", "Bling that thing.\n\nFish Type: SAPPHIRE + EMERALD + RUBY\nChance to Multiply: 50%\nMutiplier Amount: 2x", new Dictionary<FishType, int> { { FishType.SAPPHIRE_FISH, 3 }, { FishType.EMERALD_FISH, 2 }, { FishType.RUBY_FISH, 1 } }, ItemType.FISHING_ROD_SHAFT, "shaft_treasure", new List<FishType> { FishType.SAPPHIRE_FISH, FishType.EMERALD_FISH, FishType.RUBY_FISH }, 50, 2),
        //FISHING ROD BAIT
        new FishingRodBait("Pickaxe", "Mining in Fishcraft.\n\nFish Type: STONE\nFind Chance Increase: 1.25x", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 5 }, { FishType.STONE_FISH, 4 }, { FishType.IRON_FISH, 1 } }, ItemType.FISHING_ROD_BAIT, "bait_pickaxe", new List<FishType> { FishType.STONE_FISH }, 1.25f),
        new FishingRodBait("Magnet", "Metal attracts metal.\n\nFish Type: IRON\nFind Chance Increase: 1.5x", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 10 }, { FishType.IRON_FISH, 3 } }, ItemType.FISHING_ROD_BAIT, "bait_magnet", new List<FishType> { FishType.IRON_FISH }, 1.5f),
        new FishingRodBait("Medal", "Fresh off the podium.\n\nFish Type: BRONZE + SILVER + GOLD\nFind Chance Increase: 1.333x", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 15 }, { FishType.STONE_FISH, 15 } }, ItemType.FISHING_ROD_BAIT, "bait_medal", new List<FishType> { FishType.BRONZE_FISH, FishType.SILVER_FISH, FishType.GOLD_FISH }, 1.333f),
        new FishingRodBait("Shiny Magnet", "Gold plated for extra attraction.\n\nFish Type: IRON + GOLD\nFind Chance Increase: 1.75x", new Dictionary<FishType, int> { { FishType.IRON_FISH, 10 }, { FishType.GOLD_FISH, 5 } }, ItemType.FISHING_ROD_BAIT, "bait_shiny_magnet", new List<FishType> { FishType.IRON_FISH, FishType.GOLD_FISH }, 1.75f),
        new FishingRodBait("Recycled Bait", "Reduce, Reuse, Recycle.\n\nFish Type: WOOD + BRONZE + EMERALD\nFind Chance Increase: 2x", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 13 }, { FishType.STONE_FISH, 11 }, { FishType.BRONZE_FISH, 7 }, { FishType.SILVER_FISH, 2 } }, ItemType.FISHING_ROD_BAIT, "bait_recycled", new List<FishType> { FishType.WOOD_FISH, FishType.BRONZE_FISH, FishType.EMERALD_FISH }, 2f),
        new FishingRodBait("Mistletoe", "Tis the season for white, green, and red.\n\nFish Type: SILVER + EMERALD + RUBY\nFind Chance Increase: 2x", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 30 }, { FishType.GOLD_FISH, 3 } }, ItemType.FISHING_ROD_BAIT, "bait_mistletoe", new List<FishType> { FishType.SILVER_FISH, FishType.EMERALD_FISH, FishType.RUBY_FISH }, 2f),
        new FishingRodBait("Wedding Ring", "It has just the right sparkle to it.\n\nFish Type: DIAMOND\nFind Chance Increase: 1.75x", new Dictionary<FishType, int> { { FishType.GOLD_FISH, 7 }, { FishType.DIAMOND_FISH, 1 } }, ItemType.FISHING_ROD_BAIT, "bait_diamond_ring", new List<FishType> { FishType.DIAMOND_FISH }, 1.75f),
        new FishingRodBait("Donut", "For the boys in blue.\n\nFish Type: IRON + SAPPHIRE + DIAMOND\nFind Chance Increase: 1.5x", new Dictionary<FishType, int> { { FishType.BRONZE_FISH, 25 }, { FishType.SAPPHIRE_FISH, 1 }, { FishType.RUBY_FISH, 1 } }, ItemType.FISHING_ROD_BAIT, "bait_donut", new List<FishType> { FishType.IRON_FISH, FishType.SAPPHIRE_FISH, FishType.DIAMOND_FISH }, 1.5f),
        //FISHING ROD LINE
        new FishingRodLine("20m Fishing Line", "Some more fishing line to explore what lies below.\n\nFishing Depth: 20m", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 15 }, { FishType.STONE_FISH, 10 }, { FishType.IRON_FISH, 3 }, { FishType.STRING_FISH, 1 } }, ItemType.FISHING_ROD_LINE, "line_20m", FishingDepth.D_20_METERS),
        new FishingRodLine("30m Fishing Line", "Metal reinforced fishing line to trek even deeper.\n\nFishing Depth: 30m", new Dictionary<FishType, int> { { FishType.BRONZE_FISH, 25 }, { FishType.SILVER_FISH, 10 }, { FishType.GOLD_FISH, 3 }, { FishType.STRING_FISH, 1 } }, ItemType.FISHING_ROD_LINE, "line_30m", FishingDepth.D_30_METERS),
        new FishingRodLine("40m Fishing Line", "To reach the depths.\n\nFishing Depth: 40m", new Dictionary<FishType, int> { { FishType.GOLD_FISH, 30 }, { FishType.DIAMOND_FISH, 10 }, { FishType.STRING_FISH, 1 } }, ItemType.FISHING_ROD_LINE, "line_40m", FishingDepth.D_40_METERS),
        //WAND
        new WandItem("Wand of the Novice", "A beginners wand to teach you the basics.\n\nDMG MOD: 1x\nSIZE MOD: 1.5x\nRANGE MOD: 1x\nCD MOD: 1x", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 4 }, { FishType.STONE_FISH, 3 }, { FishType.BRONZE_FISH, 2 } }, ItemType.WAND, "", 1f, 1.5f, 1f, 1f),
        new WandItem("Apprentice's Wand", "To be given to all apprentices on their first day.\n\nDMG MOD: 1x\nSIZE MOD: 1x\nRANGE MOD: 1.5x\nCD MOD: 1x", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 5 }, { FishType.BRONZE_FISH, 1 }, { FishType.IRON_FISH, 1 } }, ItemType.WAND, "", 1f, 1f, 1.5f, 1f),
        new WandItem("Beginners Combat Wand", "Built for your first duel.\n\nDMG MOD: 1.1x\nSIZE MOD: 1x\nRANGE MOD: 1x\nCD MOD: 0.8x", new Dictionary<FishType, int> { { FishType.BRONZE_FISH, 3 }, { FishType.IRON_FISH, 2 } }, ItemType.WAND, "", 1.1f, 1f, 1f, 0.8f),
        new WandItem("Merlin's* Wand", "*Not that Merlin but, a Merlin?\n\nDMG MOD: 1.2x\nSIZE MOD: 1.2x\nRANGE MOD: 1.2x\nCD MOD: 1.5x", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 12 }, { FishType.STONE_FISH, 6 } }, ItemType.WAND, "", 1.2f, 1.2f, 1.2f, 1.5f),
        new WandItem("Wand of Swiftness", "A simple speedy spell slinger.\n\nDMG MOD: 0.75x\nSIZE MOD: 0.75x\nRANGE MOD: 1.2x\nCD MOD: 0.5x", new Dictionary<FishType, int> { { FishType.STONE_FISH, 8 }, { FishType.IRON_FISH, 2 }, { FishType.SAPPHIRE_FISH, 1} }, ItemType.WAND, "", 0.75f, 0.75f, 1.2f, 0.5f),
        new WandItem("Pocket Wand", "You’ve heard of a pocket knife.\n\nDMG MOD: 2.5x\nSIZE MOD: 0.5x\nRANGE MOD: 0.25x\nCD MOD: 0.75x", new Dictionary<FishType, int> { { FishType.IRON_FISH, 15 }, { FishType.SILVER_FISH, 10 } }, ItemType.WAND, "", 2.5f, 0.5f, 0.25f, 0.75f),
        new WandItem("Large Caliber Wand", "Steady...\n\nDMG MOD: 2x\nSIZE MOD: 1.25x\nRANGE MOD: 2x\nCD MOD: 2x", new Dictionary<FishType, int> { { FishType.STONE_FISH, 15 }, { FishType.BRONZE_FISH, 10 }, { FishType.GOLD_FISH, 3 } }, ItemType.WAND, "", 2f, 1.25f, 2f, 2f),
        new WandItem("Wand of the Mage", "A professional’s choice.\n\nDMG MOD: 1.25x\nSIZE MOD: 1.25x\nRANGE MOD: 1.25x\nCD MOD: 0.75x", new Dictionary<FishType, int> { { FishType.WOOD_FISH, 30 }, { FishType.EMERALD_FISH, 1 } }, ItemType.WAND, "", 1.25f, 1.25f, 1.25f, .75f),
        new WandItem("Rapid Fire Wand", "So anyways I started blasting.\n\nDMG MOD: 0.5x\nSIZE MOD: 1x\nRANGE MOD: 1x\nCD MOD: 0.2x", new Dictionary<FishType, int> { { FishType.SILVER_FISH, 15 }, { FishType.GOLD_FISH, 10 } }, ItemType.WAND, "", 0.5f, 1f, 1f, .2f),
        new WandItem("Wand of the Wizard", "For once you have truly mastered casting spells.\n\nDMG MOD: 1.4x\nSIZE MOD: 1.4x\nRANGE MOD: 1.4x\nCD MOD: 0.7x", new Dictionary<FishType, int> { { FishType.IRON_FISH, 10 }, { FishType.GOLD_FISH, 5 }, { FishType.RUBY_FISH, 1 } }, ItemType.WAND, "", 1.4f, 1.4f, 1.4f, .7f),
        //PRIMARY ACTIVE SPELL
        new SpellItem("Lightning Spell", "Shoot a beam of lightning towards your target, dealing good damage across multiple enemies.\n\nDMG: 2\nRANGE: SHORT\nCOOLDOWN: 0.5s\nPIERCING: YES", new Dictionary<FishType, int> { { FishType.BRONZE_FISH, 4 }, { FishType.IRON_FISH, 1 } }, ItemType.PRIMARY_ACTIVE_SPELL, "", "LightningSpell"),
        //SECONDARY ACTIVE SPELL
        new SpellItem("Meteor Spell", "Call down a large meteor that does damage in an area around the cast location.\n\nDMG: 10\nRANGE: LONG\nCOOLDOWN: 15s\nPIERCING: YES", new Dictionary<FishType, int> { { FishType.STONE_FISH, 15 } }, ItemType.SECONDARY_ACTIVE_SPELL, "", "MeteorSpell"),
    };

    public static List<CraftingCategory> AllCraftingCategories = new List<CraftingCategory> {
        new CraftingCategory("Fishing Rod Handle", "Upgrades that will help with handling while fishing. This can be faster overall fishing, or upgrades to the Quick Time Event.", ItemType.FISHING_ROD_HANDLE, "handle_starting"),
        new CraftingCategory("Fishing Rod Shaft", "Shafts can improve your yield when making a successful catch. Some will generally help with catching more fish per cast while others specialize in certain species.", ItemType.FISHING_ROD_SHAFT, "shaft_starting"),
        new CraftingCategory("Fishing Rod Bait", "Choosing the right bait can help make sure you find the type of fish you’re looking for.", ItemType.FISHING_ROD_BAIT, "bait_starter"),
        new CraftingCategory("Fishing Rod Line", "Unlock further depths to find brand new types of fish.", ItemType.FISHING_ROD_LINE, "line_10m"),
        new CraftingCategory("Wand", "Your tool for casting spells; choosing the right wand is imperative to fighting off those pesky critters.", ItemType.WAND, null),
        new CraftingCategory("Primary Spell", "desc", ItemType.PRIMARY_ACTIVE_SPELL, null),
        new CraftingCategory("Secondary Spell", "desc", ItemType.SECONDARY_ACTIVE_SPELL, null),
        new CraftingCategory("Passive Spell", "desc", ItemType.PASSIVE_SPELL, null),
        new CraftingCategory("Charm", "desc", ItemType.CHARM, null),
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

    public class WandItem : CraftableItem
    {
        public float damageModifier;
        public float sizeModifier;
        public float rangeModifier;
        public float cooldownModifier;

        public WandItem(string name, string desc, Dictionary<FishType, int> costs, ItemType type, string image, float dmgMod, float sizeMod, float rangeMod, float cdMod) : base(name, desc, costs, type, image)
        {
            damageModifier = dmgMod;
            sizeModifier = sizeMod;
            rangeModifier = rangeMod;
            cooldownModifier = cdMod;
        }
    }

    public class SpellItem : CraftableItem
    {
        public string spellName;

        public SpellItem(string name, string desc, Dictionary<FishType, int> costs, ItemType type, string image, string spell) : base(name, desc, costs, type, image)
        {
            spellName = spell;
        }
    }
}
