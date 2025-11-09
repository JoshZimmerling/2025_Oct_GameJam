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
        GameObject spellEffectObject = null;
        SpellEffect spellEffect = null;
        Vector2 direction = target - (Vector2) transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        deleteSpellTime = spellDuration;

        player = GameObject.Find("Player");

        switch (effect) {
            case ProjectileSpellEffect:
                spellEffect = Instantiate(effect, transform.position, transform.rotation);
                ((ProjectileSpellEffect)spellEffect).SetDirection(direction.normalized);
                deleteSpellTime = spellDuration * wand.rangeModifier;
                break;
            case LaserSpellEffect:
                spellEffect = Instantiate(effect, transform.position, transform.rotation);
                break;
            case ClickSpellEffect:
                if(Vector2.Distance(player.transform.position, target) > (((ClickSpellEffect)effect).GetMaxRange() * wand.rangeModifier))
                    target = (Vector2)player.transform.position + direction.normalized * (((ClickSpellEffect)effect).GetMaxRange() * wand.rangeModifier);
                spellEffect = Instantiate(effect, target, Quaternion.identity);
                break;
            case BuffSpellEffect:
                spellEffect = Instantiate(effect, player.transform.position, Quaternion.identity);
                break;
        }
        spellEffect.Setup(wand);
        spellEffectObject = spellEffect.gameObject;
        
        Destroy(spellEffectObject, deleteSpellTime);
    }
}