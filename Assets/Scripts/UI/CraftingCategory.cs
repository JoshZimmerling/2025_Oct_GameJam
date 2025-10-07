using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingCategory : MonoBehaviour
{
    private Button myButton;
    private TextMeshProUGUI myText;
    private Image myImage;
    Constants.UICraftingCategory category;
    private CraftablesUIHandler uiScript;

    private void Awake()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(OnTextClicked);

        myText = GetComponentInChildren<TextMeshProUGUI>();
        myImage = GetComponentInChildren<Image>();
    }

    public void SetupCraftingCategory(Constants.UICraftingCategory cat, CraftablesUIHandler UIScript)
    {
        category = cat;
        myText.text = cat.categoryName;
        myImage.sprite = cat.categoryImage;
        uiScript = UIScript;
    }

    public void OnTextClicked()
    {
        uiScript.SetupCraftablesForCategory(category.correspondingItemType);
        Debug.Log("Clicked on " + myText.text);
    }
}
