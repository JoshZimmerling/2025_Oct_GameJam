using UnityEngine;

public class BuffSpellEffect : SpellEffect
{
    public enum BuffType
    {
        HEAL
    }

    [SerializeField] BuffType buffType;
    [SerializeField] float buffAmount;

    void Start()
    {
        switch (buffType)
        {
            case BuffType.HEAL:
                GameObject.Find("Player").GetComponent<Player>().ChangeHealth((int)buffAmount);
                break;
        }
    }
}
