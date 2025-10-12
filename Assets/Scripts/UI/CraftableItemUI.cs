using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftableItemUI : MonoBehaviour
{
    private Button myButton;
    private TextMeshProUGUI myText;
    private Image myImage;
    private Image backgroundColor;
    Constants.CraftableItem itemInfo;
    private CraftablesUIHandler uiScript;

    private void Awake()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(OnTextClicked);

        myText = GetComponentInChildren<TextMeshProUGUI>();
        myImage = gameObject.transform.Find("Category Image").GetComponent<Image>();
        backgroundColor = gameObject.transform.Find("Background Color").GetComponent<Image>();
        backgroundColor.enabled = false;
    }

    public void SetupCraftableItem(Constants.CraftableItem item, CraftablesUIHandler UIScript)
    {
        itemInfo = item;
        myText.text = item.itemName;
        myImage.sprite = Resources.Load<Sprite>("Item Sprites/" + item.itemImageName);
        uiScript = UIScript;
    }

    public void TurnOffBackgroundColor()
    {
        backgroundColor.enabled = false;
    }

    public void OnTextClicked()
    {
        uiScript.ShowItemDetailsInPanel(itemInfo);
        backgroundColor.enabled = true;
    }
}
