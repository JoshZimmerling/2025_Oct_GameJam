using Unity.Mathematics;
using UnityEditor.UIElements;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public SpellEffect effect;

    public float duration = 0.3f;
    public float cooldown = 1f;
    
    public void CastSpell(Transform transform, Vector2 target, Wand wand)
    {
        GameObject spellEffectObject;
        SpellEffect spellEffect;
        Vector2 direction = target - (Vector2) transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (effect is ClickSpellEffect)
        {
            spellEffect = Instantiate(effect, target, Quaternion.identity); 
        }
        else
        {
            spellEffect = Instantiate(effect, transform.position, transform.rotation);
            
        }
        spellEffectObject = spellEffect.gameObject;
        
        //Apply wand properties
        spellEffectObject.transform.localScale *= wand.sizeModifier;
        spellEffect.damage *= wand.damageModifier;
        //TODO apply range
        
        ProjectileSpellEffect pse = spellEffectObject.GetComponentInChildren<ProjectileSpellEffect>();

        if (pse != null)
        {
            pse.SetDirection(direction.normalized);
        }
        
        Destroy(spellEffectObject, duration);
    }
}