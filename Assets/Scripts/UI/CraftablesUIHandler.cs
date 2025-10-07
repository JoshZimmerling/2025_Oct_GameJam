using System;
using UnityEngine;
using UnityEngine.UI;

public class CraftablesUIHandler : MonoBehaviour
{
    [SerializeField] Button resetShopButton;
    [SerializeField] GameObject craftingCategoryPrefab;
    [SerializeField] Transform listToPopulate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resetShopButton.onClick.AddListener(ResetCraftingMenu);

        SetupCraftingCategories();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetCraftingMenu()
    {
        foreach (Transform child in listToPopulate)
        {
            Destroy(child.gameObject);
        }
        SetupCraftingCategories();
    }

    private void SetupCraftingCategories()
    {
        int yPos = 350;
        foreach (Constants.UICraftingCategory category in Constants.AllCraftingCategories)
        {
            GameObject createdPrefab = Instantiate(craftingCategoryPrefab, listToPopulate);
            createdPrefab.transform.localPosition = new Vector3(350, yPos, 0);
            createdPrefab.GetComponent<CraftingCategory>().SetupCraftingCategory(category, this);
            yPos -= 75;
        }
    }

    public void SetupCraftablesForCategory(Constants.ItemType category)
    {
        int yPos = 350;
        foreach (CraftableItem item in Constants.AllCraftableItems)
        {
            if(item.itemType == category)
            {
                Debug.Log(item.itemName);
                GameObject createdPrefab = Instantiate(craftingCategoryPrefab, listToPopulate);
                yPos -= 75;
            }
        }
    }
}
