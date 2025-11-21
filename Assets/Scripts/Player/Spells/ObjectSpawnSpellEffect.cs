using System.Collections;
using UnityEngine;

//This class is for projectile spells that do originate from the player (i.e. Xereth E)
public class ObjectSpawnSpellEffect : SpellEffect
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] int numToSpawn;
    [SerializeField] float distanceAwayToSpawn;

    GameObject player;

    private void Start()
    {
       
    }

    public override void Setup(Wand wand)
    {
        player = GameObject.Find("Player");
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        damage *= wand.damageModifier;
        gameObject.transform.localScale *= wand.sizeModifier;
        distanceAwayToSpawn *= wand.rangeModifier;

        //Spawn all of em in
        for (int i = 0; i < numToSpawn; i++)
        {
            GameObject spawnedObject = Instantiate(objectToSpawn, transform);
            float xPos = (i % 2 == 0) ? player.transform.position.x - distanceAwayToSpawn + (2 * distanceAwayToSpawn * ((float)i / (float)(numToSpawn - 1))) : player.transform.position.x - distanceAwayToSpawn + (2 * distanceAwayToSpawn * ((float)(i + 1) / (float)(numToSpawn - 1)));
            int yBasis = (i > numToSpawn/2) ? numToSpawn / 2 - (i + 1 - numToSpawn/2) : i;
            float yPos = (yBasis % 2 == 0) ? player.transform.position.y + (2 * distanceAwayToSpawn * ((float)yBasis / (float)(numToSpawn - 1))) : player.transform.position.y - (2 * distanceAwayToSpawn * ((float)(yBasis + 1) / (float)(numToSpawn - 1)));
            spawnedObject.transform.position = new Vector2(xPos, yPos);
            SpawnedObject objectScript = spawnedObject.GetComponent<SpawnedObject>();
            objectScript.Setup(damage, slowPercentage, slowDuration, isPassthrough, knockback);
        }
    }

    private void FixedUpdate()
    {
        
    }
}
