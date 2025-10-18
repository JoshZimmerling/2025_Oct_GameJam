using TMPro;
using UnityEngine;

public class PlayerUIHandler : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI manaText;
    
    [SerializeField] TextMeshProUGUI spellCd;
    [SerializeField] GameObject player;
    private Wand wand;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        wand = player.GetComponentInChildren<Wand>();
    }

    // Update is called once per frame
    void Update()
    {
        SetSpellCD(wand.cooldownCounter);
    }

    public void SetHealth(int health)
    {
        healthText.text = health.ToString();
    }

    public void SetMana(int mana)
    {
        manaText.text = mana.ToString();
    }

    public void SetSpellCD(float spellCD)
    {
        spellCd.text = spellCD.ToString();
    }
}
