using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public PlayerInventory inventory;
    [SerializeField] PlayerUIHandler uiHandler;
    [SerializeField] GameObject textbox;
    private Coroutine currentTextBoxCoroutine;

    public int maxHealth = 100;
    public int currentHealth;
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        inventory = gameObject.GetComponent<PlayerInventory>();
        textbox.SetActive(false);
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

    public void DisplayText(string text, float duration)
    {
        if (currentTextBoxCoroutine != null)
        {
            StopCoroutine(currentTextBoxCoroutine);
        }
        currentTextBoxCoroutine = StartCoroutine(TextDisplayLogic(text, duration));
    }

    public void ClearText()
    {
        if (currentTextBoxCoroutine != null)
        {
            StopCoroutine(currentTextBoxCoroutine);
            textbox.SetActive(false);
        }
    }

    private IEnumerator TextDisplayLogic(string text, float duration)
    {
        textbox.SetActive(true);
        textbox.GetComponentInChildren<TextMeshPro>().text = text;
        yield return new WaitForSeconds(duration);
        textbox.SetActive(false);
    }

}
