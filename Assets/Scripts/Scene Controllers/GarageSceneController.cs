using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GarageSceneController : MonoBehaviour
{
    [SerializeField] PolygonCollider2D doorToOutsideCollider;
    [SerializeField] PolygonCollider2D bedCollider;
    [SerializeField] InventoryUIHandler inventoryUI;
    [SerializeField] PolygonCollider2D inventoryCollider;
    [SerializeField] CraftablesUIHandler craftingUI;
    [SerializeField] PolygonCollider2D craftingCollider;

    private GameObject player;
    private Player playerScript;
    private PolygonCollider2D playerCollider;

    private static bool hasPlayerSlept = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        playerCollider = player.GetComponent<PolygonCollider2D>();

        player.transform.position = new Vector3(0,0,player.transform.position.z);

        inventoryUI.playerScript = playerScript;
        craftingUI.playerScript = playerScript;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && playerCollider.IsTouching(doorToOutsideCollider) && hasPlayerSlept)
        {
            SceneManager.LoadScene("OutsideScene");
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame && playerCollider.IsTouching(bedCollider) && !hasPlayerSlept)
        {
            //Reset player health
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame && playerCollider.IsTouching(inventoryCollider))
        {
            inventoryUI.OpenInventoryMenu();
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame && playerCollider.IsTouching(craftingCollider))
        {
            craftingUI.OpenCraftingMenu();
        }
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            inventoryUI.CloseInventoryMenu();
            craftingUI.CloseCraftingMenu();
        }
    }
}
