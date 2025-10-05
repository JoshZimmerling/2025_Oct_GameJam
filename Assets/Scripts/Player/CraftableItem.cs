using System.Collections.Generic;
using UnityEngine;

public class CraftableItem : MonoBehaviour
{
    public string itemName;
    public Dictionary<Constants.FishType, int> craftingCosts;
    public Constants.ItemType itemType;

    public CraftableItem(string name, Dictionary<Constants.FishType, int> costs, Constants.ItemType type)
    {
        itemName = name;
        craftingCosts = costs;
        itemType = type;
    }
}
