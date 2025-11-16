using UnityEngine;

public class ObjectDeleter : MonoBehaviour
{
    [SerializeField] float lengthOfWait;

    void Start()
    {
        Destroy(gameObject, lengthOfWait);
    }
}
