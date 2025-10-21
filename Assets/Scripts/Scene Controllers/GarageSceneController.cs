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

    private static bool hasPlayerSlept;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        playerCollider = player.GetComponent<PolygonCollider2D>();

        player.transform.position = new Vector3(0,0,player.transform.position.z);
        hasPlayerSlept = false;

        inventoryUI.playerScript = playerScript;
        craftingUI.playerScript = playerScript;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (playerCollider.IsTouching(doorToOutsideCollider))
            {
                if (hasPlayerSlept)
                {
                    SceneManager.LoadScene("OutsideScene");
                }
                else
                {
                    playerScript.DisplayText("zzzzzzzzz", 2f);
                }
            }
            else if (playerCollider.IsTouching(bedCollider)){
                if (hasPlayerSlept)
                {
                    playerScript.DisplayText("Not really feeling a nap rn", 2f);
                }
                else
                {
                    hasPlayerSlept = true;
                    playerScript.SetFullHealth();
                }
            }
            else if (playerCollider.IsTouching(inventoryCollider))
            {
                inventoryUI.OpenInventoryMenu();
            }
            else if (playerCollider.IsTouching(craftingCollider))
            {
                craftingUI.OpenCraftingMenu();
            }
        }
        else if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            inventoryUI.CloseInventoryMenu();
            craftingUI.CloseCraftingMenu();
        }
    }
}
