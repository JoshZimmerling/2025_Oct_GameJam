using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public PlayerInventory inventory;
    [SerializeField] PlayerUIHandler uiHandler;
        
    public int maxHealth = 100;
    public int currentHealth;
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        inventory = gameObject.GetComponent<PlayerInventory>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if(currentHealth < maxHealth)
        {
            currentHealth = maxHealth;
        }
        uiHandler.SetHealth(currentHealth);
    }

    public void SetFullHealth()
    {
        currentHealth = maxHealth;
    }

}
