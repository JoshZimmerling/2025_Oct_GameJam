using UnityEngine;
using static BuffSpellEffect;

//This class is for point and click spells that don't originate from the player (i.e. Xereth W)
public class ClickSpellEffect : SpellEffect
{
    [SerializeField] float maxRange;

    public float GetMaxRange()
    {
        return maxRange;
    }
}
