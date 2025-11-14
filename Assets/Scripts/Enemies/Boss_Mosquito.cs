using UnityEngine;
using System.Collections.Generic;

public class Boss_Mosquito : MonoBehaviour
{
    [SerializeField] GameObject normalMosquito;
    [SerializeField] int numMosquitosToSpawn;
    [SerializeField] float frequencyToSpawn;
    private float spawningTimer;
    private List<GameObject> mosquitoList = new List<GameObject>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawningTimer = frequencyToSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        spawningTimer = Mathf.Max(0, spawningTimer - Time.deltaTime);
        if(spawningTimer <= 0)
        {
            spawningTimer = frequencyToSpawn;
            for (int i = 0; i < numMosquitosToSpawn; i++)
            {
                float randomXShift = Random.Range(3.0f, 5.0f) * ((Random.Range(0, 2) == 0) ? 1f : -1f);
                float randomYShift = Random.Range(3.0f, 5.0f) * ((Random.Range(0, 2) == 0) ? 1f : -1f);
                mosquitoList.Add(Instantiate(normalMosquito, new Vector3(transform.position.x + randomXShift, transform.position.y + randomYShift, transform.position.z), transform.rotation));
            }
        }
    }

    private void OnDestroy()
    {
        foreach (GameObject mosquito in mosquitoList)
        {
            Destroy(mosquito);
        }
    }
}
