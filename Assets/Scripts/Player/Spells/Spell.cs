using Unity.VisualScripting;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public SpellEffect effect;

    [SerializeField] float spellDuration = 0.3f;
    private float deleteSpellTime;
    public float cooldown = 1f;
    public float cooldownTimer = 0;

    private GameObject player;
    
    public void CastSpell(Transform transform, Vector2 target, Wand wand)
    {
        GameObject spellEffectObject;
        SpellEffect spellEffect;
        Vector2 direction = target - (Vector2) transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        deleteSpellTime = spellDuration;

        player = GameObject.Find("Player");

        switch (effect) {
            case ProjectileSpellEffect:
                spellEffect = Instantiate(effect, transform.position, transform.rotation);
                spellEffect.gameObject.transform.localScale *= wand.sizeModifier;
                deleteSpellTime = spellDuration * wand.rangeModifier;
                break;
            case LaserSpellEffect:
                spellEffect = Instantiate(effect, transform.position, transform.rotation);
                spellEffect.gameObject.transform.localScale = new Vector2(spellEffect.gameObject.transform.localScale.x * wand.rangeModifier, spellEffect.gameObject.transform.localScale.y);
                spellEffect.gameObject.transform.localScale *= wand.sizeModifier;
                break;
            case ClickSpellEffect:
                if(Vector2.Distance(player.transform.position, target) > (((ClickSpellEffect)effect).GetMaxRange() * wand.rangeModifier))
                {
                    target = (Vector2)player.transform.position + direction.normalized * (((ClickSpellEffect)effect).GetMaxRange() * wand.rangeModifier);
                }
                spellEffect = Instantiate(effect, target, Quaternion.identity);
                spellEffect.gameObject.transform.localScale *= wand.sizeModifier;
                break;
            case BuffSpellEffect:
                spellEffect = Instantiate(effect, player.transform.position, Quaternion.identity);
                break;
            default:
                spellEffect = Instantiate(effect, transform.position, transform.rotation);
                spellEffect.gameObject.transform.localScale = new Vector2(spellEffect.gameObject.transform.localScale.x * wand.rangeModifier, spellEffect.gameObject.transform.localScale.y);
                spellEffect.gameObject.transform.localScale *= wand.sizeModifier;
                break;
        }
        spellEffectObject = spellEffect.gameObject;
        
        //Apply wand properties
        spellEffect.damage *= wand.damageModifier;
        
        ProjectileSpellEffect pse = spellEffectObject.GetComponentInChildren<ProjectileSpellEffect>();

        if (pse != null)
        {
            pse.SetDirection(direction.normalized);
        }
        
        Destroy(spellEffectObject, deleteSpellTime);
    }
}