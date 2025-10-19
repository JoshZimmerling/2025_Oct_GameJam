using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Constants;

public class InventoryUIHandler : MonoBehaviour
{
    public Player playerScript;

    [SerializeField] Button closeMenuButton;
    [SerializeField] GameObject equippedItemPrefab;
    [SerializeField] GameObject inventoryItemPrefab;
    [SerializeField] GameObject resourceCountPrefab;
    [SerializeField] Transform equippedListUI;
    [SerializeField] Transform inventoryListUI;
    [SerializeField] Transform resourceListUI;

    [SerializeField] GameObject itemInfoPanel;
    [SerializeField] Button equipButton;

    private CraftableItem currentlySelectedItem = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closeMenuButton.onClick.AddListener(CloseInventoryMenu);
        equipButton.onClick.AddListener(EquipItem);

        PopulateEquippedItemsList();
        OpenInventoryMenu();
    }

    public void OpenInventoryMenu()
    {
        playerScript.gameObject.GetComponent<PlayerMovement>().DisableMovement();
        itemInfoPanel.SetActive(false);
        UpdateResourceCount();
        PopulateInventoryItemsList();
        gameObject.SetActive(true);
    }

    public void CloseInventoryMenu()
    {
        playerScript.gameObject.GetComponent<PlayerMovement>().EnableMovement();
        itemInfoPanel.SetActive(false);
        gameObject.SetActive(false);
        TurnOffEquippedItemBackgrounds();
        TurnOffInventoryItemBackgrounds();
    }

    private void EmptyInventoryItemsList()
    {
        foreach (Transform child in inventoryListUI)
        {
            Destroy(child.gameObject);
        }
    }

    private void EmptyEquippedItemsList()
    {
        foreach (Transform child in equippedListUI)
        {
            Destroy(child.gameObject);
        }
    }

    private void PopulateInventoryItemsList()
    {
        EmptyInventoryItemsList();
        PlayerInventory inventory = playerScript.GetComponent<PlayerInventory>();
        int xPos = -260;
        int yPos = 300;
        int counter = 0;
        foreach (CraftableItem item in inventory.GetAllCraftedItems())
        {
            GameObject createdPrefab = Instantiate(inventoryItemPrefab, inventoryListUI);
            createdPrefab.transform.localPosition = new Vector3(xPos, yPos, 0);
            createdPrefab.GetComponent<InventoryItemUI>().SetupInventoryItem(item, this);
            counter++;
            if (counter % 6 == 0)
            {
                yPos -= 105;
                xPos -= 525;
            }
            else
                xPos += 105;
        }
    }

    private void PopulateEquippedItemsList()
    {
        EmptyEquippedItemsList();
        PlayerInventory inventory = playerScript.GetComponent<PlayerInventory>();
        int xPos = -125;
        int yPos = 300;
        int counter = 0;
        foreach (Constants.CraftingCategory category in Constants.AllCraftingCategories)
        {
            GameObject createdPrefab = Instantiate(equippedItemPrefab, equippedListUI);
            createdPrefab.transform.localPosition = new Vector3(xPos, yPos, 0);
            createdPrefab.transform.Find("Item Category Name").GetComponent<TextMeshProUGUI>().text = category.categoryName;
            createdPrefab.GetComponent<EquippedItemUI>().SetupEquippedItem(inventory.GetEquippedItemByItemType(category.correspondingItemType), this);
            counter++;
            if (counter % 2 == 0)
            {
                yPos -= 150;
                xPos -= 250;
            }
            else
                xPos += 250;
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

    public void ShowItemDetailsInPanel(CraftableItem item)
    {
        TurnOffInventoryItemBackgrounds();
        TurnOffEquippedItemBackgrounds();

        PlayerInventory inventory = playerScript.GetComponent<PlayerInventory>();
        currentlySelectedItem = item;
        itemInfoPanel.SetActive(true);
        itemInfoPanel.transform.Find("Item Name").GetComponent<TextMeshProUGUI>().text = item.itemName;
        itemInfoPanel.transform.Find("Item Category").GetComponent<TextMeshProUGUI>().text = "Category: " + GetCategoryNameByType(item.itemType);
        itemInfoPanel.transform.Find("Item Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item Sprites/" + item.itemImageName);
        itemInfoPanel.transform.Find("Item Description").GetComponent<TextMeshProUGUI>().text = item.itemDescription;

        if (inventory.PlayerHasItemEquipped(item))
        {
            itemInfoPanel.transform.Find("Equipped Label").gameObject.SetActive(true);
        }
        else
        {
            itemInfoPanel.transform.Find("Equipped Label").gameObject.SetActive(false);
        }
    }

    private void TurnOffInventoryItemBackgrounds()
    {
        foreach (Transform item in inventoryListUI)
        {
            item.gameObject.GetComponent<InventoryItemUI>().TurnOffBackgroundColor();
        }
    }

    private void TurnOffEquippedItemBackgrounds()
    {
        foreach (Transform item in equippedListUI)
        {
            item.gameObject.GetComponent<EquippedItemUI>().TurnOffBackgroundColor();
        }
    }

    private void EquipItem()
    {
        playerScript.GetComponent<PlayerInventory>().EquipItem(currentlySelectedItem);
        itemInfoPanel.transform.Find("Equipped Label").gameObject.SetActive(true);
        PopulateEquippedItemsList();
        PopulateInventoryItemsList();
    }

    private string GetCategoryNameByType(ItemType itemType)
    {
        foreach (CraftingCategory category in AllCraftingCategories)
        {
            if (category.correspondingItemType == itemType)
                return category.categoryName;
        }
        return null;
    }
}
