using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerInventory inventory;

    void Start()
    {
        inventory = gameObject.GetComponent<PlayerInventory>();
    }

}
