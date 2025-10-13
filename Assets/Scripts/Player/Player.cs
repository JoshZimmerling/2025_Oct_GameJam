using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject craftingScreen;
    [SerializeField] GameObject inventoryScreen;
    [SerializeField] GameObject playerUIScreen;
    
    public PlayerInventory inventory;
    public PlayerUIHandler uiHandler;
        
    public int health;
    public int mana;
    
    void Start()
    {
        inventory = gameObject.GetComponent<PlayerInventory>();
        uiHandler = playerUIScreen.GetComponent<PlayerUIHandler>();
        
        health = 100;
        mana = 100;
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

    public void ChangeHealth(int amount)
    {
        health += amount;
        uiHandler.SetHealth(health);
    }

    public void ChangeMana(int amount)
    {
        mana += amount;
        uiHandler.SetMana(mana);
    }

}
