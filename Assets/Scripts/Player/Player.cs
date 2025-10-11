using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject craftingScreen;
    public PlayerInventory inventory;

    void Start()
    {
        inventory = gameObject.GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            if (!craftingScreen.activeSelf)
            {
                craftingScreen.GetComponent<CraftablesUIHandler>().OpenCraftingMenu();
            }
            else
            {
                craftingScreen.GetComponent<CraftablesUIHandler>().CloseCraftingMenu();
            }
        }
    }

}
