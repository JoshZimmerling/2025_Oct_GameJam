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
    private static bool firstTimeEnteringGarage = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        playerCollider = player.GetComponent<PolygonCollider2D>();

        if (firstTimeEnteringGarage)
        {
            player.transform.position = new Vector3(-6.4f, -2f, player.transform.position.z);
            hasPlayerSlept = true;
            firstTimeEnteringGarage = false;
        }
        else
        {
            player.transform.position = new Vector3(6.4f, 0, player.transform.position.z);
            hasPlayerSlept = false;
        }

        inventoryUI.playerScript = playerScript;
        craftingUI.playerScript = playerScript;

        GameObject.Find("Camera").GetComponent<CameraScript>().UpdateCameraZoom(5);
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
                    playerScript.DisplayText("I'm too sleepy", 2f);
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
                    playerScript.DisplayText("zzzzzzzzzz", 2f);
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
