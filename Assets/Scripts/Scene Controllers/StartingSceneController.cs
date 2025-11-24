using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartingSceneController : MonoBehaviour
{
    [SerializeField] PolygonCollider2D doorToGarageCollider;
    private GameObject player;
    private PolygonCollider2D playerCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        playerCollider = player.transform.Find("Hitbox Collider").gameObject.GetComponent<PolygonCollider2D>();

        player.transform.rotation = new Quaternion(0, 180, 0, 0);
        player.transform.Find("Textbox").localRotation = new Quaternion(0, 180, 0, 0);
        player.transform.Find("Healthbar").localRotation = new Quaternion(0, 180, 0, 0);
        player.GetComponent<Player>().DisplayText("Yea yea yea....", 5);
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame && playerCollider.IsTouching(doorToGarageCollider))
        {
            SceneManager.LoadScene("GarageScene");
        }
    }
}
