using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OutsideSceneController : MonoBehaviour
{
    [SerializeField] PolygonCollider2D doorToGarageCollider;
    private GameObject player;
    private PolygonCollider2D playerCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        playerCollider = player.GetComponent<PolygonCollider2D>();

        player.transform.position = new Vector3(0, 0, player.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && playerCollider.IsTouching(doorToGarageCollider))
        {
            SceneManager.LoadScene("GarageScene");
        }
    }
}
