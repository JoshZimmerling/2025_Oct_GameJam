using UnityEditor.UIElements;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public SpellEffect effect;

    public float duration = 0.3f;

    public void CastSpell(Transform transform)
    {
        GameObject spell = Instantiate(effect, transform.position, transform.rotation).gameObject;

        Destroy(spell, duration);
    }

    public void CastSpell(Transform transform, Vector2 target)
    {
        Vector2 direction = target - (Vector2) transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        
        GameObject spell = Instantiate(effect, transform.position, transform.rotation).gameObject;
        
        ProjectileSpellEffect pse = spell.GetComponentInChildren<ProjectileSpellEffect>();

        if (pse != null)
        {
            pse.SetDirection(direction.normalized);
        }


        Destroy(spell, duration);
    }
}