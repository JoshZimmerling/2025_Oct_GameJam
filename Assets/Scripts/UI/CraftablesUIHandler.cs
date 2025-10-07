using System;
using UnityEngine;
using UnityEngine.UI;
using static Constants;

public class CraftablesUIHandler : MonoBehaviour
{
    [SerializeField] Button closeMenuButton;
    [SerializeField] GameObject craftingCategoryPrefab;
    [SerializeField] GameObject craftableItemPrefab;
    [SerializeField] Transform categoriesListUI;
    [SerializeField] Transform craftablesListUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closeMenuButton.onClick.AddListener(CloseCraftingMenu);

        SetupCraftingCategories();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CloseCraftingMenu()
    {
        EmptyCraftablesList();
        gameObject.SetActive(false);
    }

    private void EmptyCraftablesList()
    {
        foreach (Transform child in craftablesListUI)
        {
            Destroy(child.gameObject);
        }
    }
    private void SetupCraftingCategories()
    {
        int yPos = 350;
        foreach (Constants.UICraftingCategory category in Constants.AllCraftingCategories)
        {
            GameObject createdPrefab = Instantiate(craftingCategoryPrefab, categoriesListUI);
            createdPrefab.transform.localPosition = new Vector3(300, yPos, 0);
            createdPrefab.GetComponent<CraftingCategoryUI>().SetupCraftingCategory(category, this);
            yPos -= 75;
        }
    }

    public void SetupCraftablesForCategory(Constants.ItemType category)
    {
        EmptyCraftablesList();
        int yPos = 350;
        foreach (UICraftableItem item in Constants.AllCraftableItems)
        {
            if(item.itemType == category)
            {
                GameObject createdPrefab = Instantiate(craftableItemPrefab, craftablesListUI);
                createdPrefab.transform.localPosition = new Vector3(850, yPos, 0);
                createdPrefab.GetComponent<CraftableItemUI>().SetupCraftableItem(item, this);
                yPos -= 75;
            }
        }
    }

    public void ShowCategoryDetailsInPanel(UICraftingCategory category)
    {
        TurnOffAllCategoryBackgrounds();
    }

    public void ShowItemDetailsInPanel(UICraftableItem item)
    {
        TurnOffAllItemBackgrounds();
    }

    private void TurnOffAllCategoryBackgrounds()
    {
        foreach (Transform category in categoriesListUI)
        {
            category.gameObject.GetComponent<CraftingCategoryUI>().TurnOffBackgroundColor();
        }
    }

    private void TurnOffAllItemBackgrounds()
    {
        foreach (Transform item in craftablesListUI)
        {
            item.gameObject.GetComponent<CraftableItemUI>().TurnOffBackgroundColor();
        }
    }
}
