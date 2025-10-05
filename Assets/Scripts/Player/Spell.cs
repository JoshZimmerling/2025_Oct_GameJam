using UnityEditor.UIElements;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public SpellEffect effect;

    public void castSpell()
    {
        Instantiate(effect, gameObject.transform.position, gameObject.transform.rotation);
    }
}

