using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public PlayerInventory inventory;
    [SerializeField] PlayerUIHandler uiHandler;
        
    public int health = 100;
    public int mana = 100;
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        inventory = gameObject.GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        
    }

    public void ChangeHealth(int amount)
    {
        health += amount;
        uiHandler.SetHealth(health);
    }

}
