using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingCategoryUI : MonoBehaviour
{
    private Button myButton;
    private TextMeshProUGUI myText;
    private Image myImage;
    private Image backgroundColor;
    Constants.CraftingCategory category;
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

    public void SetupCraftingCategory(Constants.CraftingCategory cat, CraftablesUIHandler UIScript)
    {
        category = cat;
        myText.text = cat.categoryName + "s";
        myImage.sprite = Resources.Load<Sprite>(cat.categoryImageName);
        uiScript = UIScript;
    }

    public void TurnOffBackgroundColor()
    {
        backgroundColor.enabled = false;
    }

    private void OnTextClicked()
    {
        uiScript.ShowCategoryDetailsInPanel(category);
        uiScript.SetupCraftablesForCategory(category.correspondingItemType);
        backgroundColor.enabled = true;
    }
}
