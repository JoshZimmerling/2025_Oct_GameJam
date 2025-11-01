using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public PlayerInventory inventory;
    [SerializeField] PlayerUIHandler uiHandler;
    [SerializeField] GameObject textbox;
    private Coroutine currentTextBoxCoroutine;

    public int maxHealth = 100;
    public int currentHealth;

    public GameObject healthbar;

    private float healthBarStartingXScale;
    private float healthBarStartingXPos;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        inventory = gameObject.GetComponent<PlayerInventory>();
        textbox.SetActive(false);

        currentHealth = maxHealth;
        healthBarStartingXPos = healthbar.transform.localPosition.x;
        healthBarStartingXScale = healthbar.transform.localScale.x;
    }

    private void Update()
    {
        
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        StartCoroutine(Flash(amount < 0 ? Color.red : Color.green));
        ScaleHealthBar();
        uiHandler.SetHealth(currentHealth);
    }

    public void SetFullHealth()
    {
        ChangeHealth(maxHealth);
    }

    public void DisplayText(string text, float duration)
    {
        ClearText();
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

    private void ScaleHealthBar()
    {
        float healthPercentage = (float)currentHealth / (float)maxHealth;
        float newXScale = healthBarStartingXScale * healthPercentage;

        healthbar.transform.localScale = new Vector3(newXScale, healthbar.transform.localScale.y, healthbar.transform.localScale.z);
        healthbar.transform.localPosition = new Vector3(healthBarStartingXPos - (healthBarStartingXScale - newXScale) * 1 / 2, healthbar.transform.localPosition.y, healthbar.transform.localPosition.z);
    }

    private IEnumerator Flash(Color color)
    {
        transform.Find("PlayerSprite").GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.1f);
        transform.Find("PlayerSprite").GetComponent<SpriteRenderer>().color = Color.white;
    }
}
