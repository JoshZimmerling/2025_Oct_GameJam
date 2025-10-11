using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject craftingScreen;
    [SerializeField] GameObject inventoryScreen;
    public PlayerInventory inventory;

    void Start()
    {
        inventory = gameObject.GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            if (!craftingScreen.activeSelf && !inventoryScreen.activeSelf)
            {
                craftingScreen.GetComponent<CraftablesUIHandler>().OpenCraftingMenu();
            }
            else
            {
                craftingScreen.GetComponent<CraftablesUIHandler>().CloseCraftingMenu();
            }
        }
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            if (!craftingScreen.activeSelf && !inventoryScreen.activeSelf)
            {
                inventoryScreen.GetComponent<InventoryUIHandler>().OpenInventoryMenu();
            }
            else
            {
                inventoryScreen.GetComponent<InventoryUIHandler>().CloseInventoryMenu();
            }
        }
    }

}
