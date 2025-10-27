using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float cameraZoom;
    private Transform transform;
    [SerializeField] Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        transform = gameObject.GetComponent<Transform>();

        cameraZoom = 5;
        UpdateCameraZoom(cameraZoom);
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -10);
    }

    public void UpdateCameraZoom(float zoom)
    {
        cameraZoom = zoom;
        gameObject.GetComponent<Camera>().orthographicSize = cameraZoom;
    }

    public float GetCameraZoom()
    {
        return cameraZoom;
    }
}
