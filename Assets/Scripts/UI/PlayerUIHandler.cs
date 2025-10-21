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
        for (int i = 0; i < wand.spells.Count; i++)
        {
            if (wand.GetSpellTimer(i)== 0)
            {
                spellCdProgressBars[i].transform.localScale = new Vector3(1, 0, 1);
            }
            else
            {
                spellCdProgressBars[i].transform.localScale = new Vector3(1, 1 - ( wand.GetSpellTimer(i) / wand.GetSpellCooldown(i) ), 1);
            }
        }
    }

    public void UpdateSpellIcons()
    {
        for (int i = 0; i < wand.spells.Count; i++)
        {
            spellImages[i].sprite = wand.spells[i].effect.GetComponentInChildren<SpriteRenderer>().sprite;
        }
    }
}
