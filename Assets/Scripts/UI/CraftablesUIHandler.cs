using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Constants;

public class CraftablesUIHandler : MonoBehaviour
{
    public Player playerScript;

    [SerializeField] Button closeMenuButton;
    [SerializeField] GameObject craftingCategoryPrefab;
    [SerializeField] GameObject craftableItemPrefab;
    [SerializeField] GameObject resourceCountPrefab;
    [SerializeField] Transform categoriesListUI;
    [SerializeField] Transform craftablesListUI;
    [SerializeField] Transform resourceListUI;

    [SerializeField] GameObject categoryInfoPanel;
    [SerializeField] GameObject itemInfoPanel;
    [SerializeField] GameObject craftingResourcePrefab;
    [SerializeField] Transform craftingResourceListUI;
    [SerializeField] Button craftButton;

    private CraftableItem currentlySelectedItem = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closeMenuButton.onClick.AddListener(CloseCraftingMenu);
        craftButton.onClick.AddListener(AttemptToCraft);

        SetupCraftingCategories();
    }

    public void OpenCraftingMenu()
    {
        itemInfoPanel.SetActive(false);
        UpdateResourceCount();
        EmptyCraftablesList();
        TurnOffAllCategoryBackgrounds();
        gameObject.SetActive(true);
    }

    public void CloseCraftingMenu()
    {
        itemInfoPanel.SetActive(false);
        EmptyCraftablesList();
        TurnOffAllCategoryBackgrounds();
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
        categoryInfoPanel.SetActive(false);
        itemInfoPanel.SetActive(false);

        int yPos = 350;
        foreach (Constants.CraftingCategory category in Constants.AllCraftingCategories)
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
        foreach (CraftableItem item in Constants.AllCraftableItems)
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

    public void ShowCategoryDetailsInPanel(CraftingCategory category)
    {
        TurnOffAllCategoryBackgrounds();
        categoryInfoPanel.SetActive(true);
        itemInfoPanel.SetActive(false);
        categoryInfoPanel.transform.Find("Category Name").GetComponent<TextMeshProUGUI>().text = category.categoryName;
        categoryInfoPanel.transform.Find("Category Description").GetComponent<TextMeshProUGUI>().text = category.categoryDescription;
    }

    public void ShowItemDetailsInPanel(CraftableItem item)
    {
        TurnOffAllItemBackgrounds();
        
        itemInfoPanel.SetActive(true);
        categoryInfoPanel.SetActive(false);
        currentlySelectedItem = item;
        itemInfoPanel.transform.Find("Item Name").GetComponent<TextMeshProUGUI>().text = item.itemName;
        itemInfoPanel.transform.Find("Item Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item Sprites/" + item.itemImageName);
        itemInfoPanel.transform.Find("Item Description").GetComponent<TextMeshProUGUI>().text = item.itemDescription;

        if (playerScript.inventory.PlayerOwnsItem(item))
        {
            itemInfoPanel.transform.Find("Sold Out Label").gameObject.SetActive(true);
        }
        else
        {
            itemInfoPanel.transform.Find("Sold Out Label").gameObject.SetActive(false);
        }

        UpdateCraftingCostCounts(item);
    }

    private void UpdateCraftingCostCounts(CraftableItem item)
    {
        foreach (Transform child in craftingResourceListUI)
        {
            Destroy(child.gameObject);
        }
        int yPos = 75;
        foreach (KeyValuePair<Constants.FishType, int> craftingResource in item.craftingCosts)
        {
            GameObject createdPrefab = Instantiate(craftingResourcePrefab, craftingResourceListUI);
            createdPrefab.transform.localPosition = new Vector3(0, yPos, 0);
            createdPrefab.transform.Find("Resource Image").GetComponent<Image>().sprite = playerScript.inventory.HasSeenFish(craftingResource.Key) ? Resources.Load<Sprite>("Fish Sprites/" + craftingResource.Key.ToString()) : Resources.Load<Sprite>("Fish Sprites/QUESTION_MARK");
            createdPrefab.transform.Find("Resource Count").GetComponent<TextMeshProUGUI>().text = playerScript.inventory.GetFish(craftingResource.Key) + "/" + craftingResource.Value;
            //Setting the text color
            if(playerScript.inventory.GetFish(craftingResource.Key) < craftingResource.Value)
            {
                createdPrefab.transform.Find("Resource Count").GetComponent<TextMeshProUGUI>().color = Color.red;
            }
            else
            {
                createdPrefab.transform.Find("Resource Count").GetComponent<TextMeshProUGUI>().color = Color.green;
            }
            yPos -= 50;
        }
    }

    private void UpdateResourceCount()
    {
        foreach (Transform child in resourceListUI)
        {
            Destroy(child.gameObject);
        }
        int xPos = -210;
        int yPos = 15;
        int counter = 0;
        foreach (KeyValuePair<FishType, int> fish in playerScript.inventory.GetInventoryFishCount())
        {
            GameObject createdPrefab = Instantiate(resourceCountPrefab, resourceListUI);
            createdPrefab.transform.localPosition = new Vector3(xPos, yPos, 0);
            createdPrefab.GetComponentInChildren<Image>().sprite = playerScript.inventory.HasSeenFish(fish.Key) ? Resources.Load<Sprite>("Fish Sprites/" + fish.Key.ToString()) : Resources.Load<Sprite>("Fish Sprites/QUESTION_MARK");
            createdPrefab.GetComponentInChildren<TextMeshProUGUI>().text = fish.Value.ToString();
            counter++;
            if (counter % 6 == 0)
            {
                yPos -= 30;
                xPos -= 400;
            }
            else
                xPos += 80;
        }
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

    private void AttemptToCraft()
    {
        PlayerInventory inventory = playerScript.inventory;

        if(inventory.SpendFish(currentlySelectedItem.craftingCosts) == true)
        {
            UpdateCraftingCostCounts(currentlySelectedItem);
            UpdateResourceCount();
            itemInfoPanel.transform.Find("Sold Out Label").gameObject.SetActive(true);
            inventory.AddCraftedItem(currentlySelectedItem);
        }
    }
}
