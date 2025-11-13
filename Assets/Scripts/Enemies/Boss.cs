using UnityEngine;

public class Boss : MonoBehaviour
{
    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public void OnDeath()
    {
        player.GetComponent<Player>().inventory.AddFish(Constants.FishType.STRING_FISH, 1);
    }
}
