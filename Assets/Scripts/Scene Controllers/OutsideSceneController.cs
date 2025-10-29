using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OutsideSceneController : MonoBehaviour
{
    [SerializeField] PolygonCollider2D doorToGarageCollider;
    [SerializeField] GameObject dayInfo;
    private Transform timeDial;
    private GameObject player;
    private PolygonCollider2D playerCollider;

    public static int dayCounter = 0;
    private float dayStartTime;
    private int fullDayTime = 180;
    public bool dayComplete = false;

    CameraScript cameraScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        playerCollider = player.transform.Find("Hitbox Collider").gameObject.GetComponent<PolygonCollider2D>();
        timeDial = dayInfo.transform.Find("Dial");

        dayCounter++;
        dayInfo.GetComponentInChildren<TextMeshPro>().text = "Day " + dayCounter;
        dayStartTime = Time.time;

        player.transform.position = new Vector3(-6.4f, -2f, player.transform.position.z);

        cameraScript = GameObject.Find("Camera").GetComponent<CameraScript>();
        cameraScript.UpdateCameraZoom(7);
    }

    // Update is called once per frame
    void Update()
    {
        dayInfo.transform.position = new Vector3(player.transform.position.x + (8f/5f * cameraScript.GetCameraZoom()), player.transform.position.y + (4f/5f * cameraScript.GetCameraZoom()), dayInfo.transform.position.z);
        if (!dayComplete)
        {
            TimeUpdate();
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame && playerCollider.IsTouching(doorToGarageCollider))
        {
            SceneManager.LoadScene("GarageScene");
        }
    }

    private void TimeUpdate()
    {
        float currentTime = Time.time - dayStartTime;
        float percentageCompleted = currentTime / (float)fullDayTime;

        //Set UI for currentTime
        timeDial.rotation = Quaternion.Euler(0, 0, 90 + (percentageCompleted * -180));
        float angle = Mathf.Deg2Rad * 180f * (1f - percentageCompleted);
        timeDial.localPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), -0.1f) * 0.5f - new Vector3(0, 0.15f, 0);

        if (percentageCompleted >= 1)
        {
            dayComplete = true;
            Fishing.CancelCurrentFishing();
        }
    }
}
