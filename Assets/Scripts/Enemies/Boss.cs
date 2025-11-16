using UnityEngine;

public class Boss : MonoBehaviour
{
    private GameObject player;
    [SerializeField] GameObject stringFish;

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
        Instantiate(stringFish, transform.position, Quaternion.identity);
        GameObject.Find("Scene Script Holder").GetComponent<OutsideSceneController>().BossKilled();
    }
}
