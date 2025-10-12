using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    private Button myButton;
    private Image myImage;
    private Image backgroundColor;
    Constants.CraftableItem itemInfo;
    private InventoryUIHandler uiScript;

    public void SetupInventoryItem(Constants.CraftableItem item, InventoryUIHandler UIScript)
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(OnClicked);

        myImage = gameObject.transform.Find("Item Image").GetComponent<Image>();
        backgroundColor = gameObject.transform.Find("Background Color").GetComponent<Image>();
        backgroundColor.enabled = false;

        itemInfo = item;
        uiScript = UIScript;
        myImage.sprite = Resources.Load<Sprite>("Item Sprites/" + item.itemImageName);
    }

    public void TurnOffBackgroundColor()
    {
        backgroundColor.enabled = false;
    }

    public void OnClicked()
    {
        if (itemInfo == null)
            return;
        uiScript.ShowItemDetailsInPanel(itemInfo);
        backgroundColor.enabled = true;
    }
}