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

    [SerializeField] Spawnzone treeSpawnZone;
    [SerializeField] Spawnzone waterSpawnZone;
    private float minTimeBetweenSpawns;
    private float maxTimeBetweenSpawns;
    private float spawnTimer;

    CameraScript cameraScript;

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

        minTimeBetweenSpawns = 4f - (dayCounter * .1f) < 2f ? 2f : 4f - (dayCounter * .1f);
        maxTimeBetweenSpawns = 7.5f - (dayCounter * .1f) < 4f ? 4f : 7.5f - (dayCounter * .1f);
        ResetSpawnTimer();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && playerCollider.IsTouching(doorToGarageCollider))
        {
            SceneManager.LoadScene("GarageScene");
        }
        dayInfo.transform.position = new Vector3(player.transform.position.x + (8f / 5f * cameraScript.GetCameraZoom()), player.transform.position.y + (4f / 5f * cameraScript.GetCameraZoom()), dayInfo.transform.position.z);
    }

    private void FixedUpdate()
    {
        if (!dayComplete)
        {
            TimeUIUpdate();
        }

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            if(Random.Range(0f,1f) <= 0.5f)
            {
                treeSpawnZone.spawnEnemy();
            }
            else
            {
                waterSpawnZone.spawnEnemy();
            }
            ResetSpawnTimer();
        }
    }

    private void TimeUIUpdate()
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

    private void ResetSpawnTimer()
    {
        float rand1 = Random.Range(minTimeBetweenSpawns/2f, minTimeBetweenSpawns / 2f + (maxTimeBetweenSpawns - minTimeBetweenSpawns) / 2f);
        float rand2 = Random.Range(minTimeBetweenSpawns/2f, minTimeBetweenSpawns / 2f + (maxTimeBetweenSpawns - minTimeBetweenSpawns) / 2f);
        spawnTimer = rand1+rand2;
    }
}
