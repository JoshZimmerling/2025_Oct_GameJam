using UnityEngine;

public class BuffSpellEffect : SpellEffect
{
    public enum BuffType
    {
        HEAL,
        SPEED
    }

    [SerializeField] BuffType buffType;
    [SerializeField] float buffAmount;
    [SerializeField] float buffDuration;

    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");

        switch (buffType)
        {
            case BuffType.HEAL:
                player.GetComponent<Player>().ChangeHealth((int)buffAmount);
                break;
            case BuffType.SPEED:
                player.GetComponent<PlayerMovement>().ChangeSpeed(buffAmount, buffDuration);
                break;
        }
    }

    public override void Setup(Wand wand)
    {
        
    }

    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z -.1f);
    }
}
