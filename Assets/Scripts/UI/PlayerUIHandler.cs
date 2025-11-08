using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class PlayerUIHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;

    [SerializeField] GameObject player;
    
    public List<Image> spellImages;
    public List<GameObject> spellCdProgressBars;
    
    private Wand wand;
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        wand = player.GetComponentInChildren<Wand>();
        
        UpdateSpellIcons();
    }
    
    void Update()
    {
        SetSpellCd();
    }

    public void SetHealth(int health)
    {
        healthText.text = health.ToString();
    }

    public void SetSpellCd()
    {
        spellCdProgressBars[0].transform.localScale = wand.GetPrimaryActiveSpell().cooldownTimer == 0 ? new Vector3(1, 0, 1) : new Vector3(1, 1 - (wand.GetPrimaryActiveSpell().cooldownTimer / (wand.GetPrimaryActiveSpell().cooldown * wand.cooldownModifier)), 1);
        spellCdProgressBars[1].transform.localScale = wand.GetSecondaryActiveSpell().cooldownTimer == 0 ? new Vector3(1, 0, 1) : new Vector3(1, 1 - (wand.GetSecondaryActiveSpell().cooldownTimer / (wand.GetSecondaryActiveSpell().cooldown * wand.cooldownModifier)), 1);
        spellCdProgressBars[2].transform.localScale = wand.GetPassiveSpell().cooldownTimer == 0 ? new Vector3(1, 0, 1) : new Vector3(1, 1 - (wand.GetPassiveSpell().cooldownTimer / (wand.GetPassiveSpell().cooldown * wand.cooldownModifier)), 1);
    }

    public void UpdateSpellIcons()
    {
        spellImages[0].sprite = wand.GetPrimaryActiveSpell().effect.GetComponentInChildren<SpriteRenderer>().sprite;
        spellImages[1].sprite = wand.GetSecondaryActiveSpell().effect.GetComponentInChildren<SpriteRenderer>().sprite;
        spellImages[2].sprite = wand.GetPassiveSpell().effect.GetComponentInChildren<SpriteRenderer>().sprite;
    }
}
