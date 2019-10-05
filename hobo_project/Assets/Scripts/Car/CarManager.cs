using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public GameObject myPrefab;

    public Transform carSpawner;
    public Transform carKiller;

    public float spawnInterval = 15.0f;

    private float nextSpawn = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn += spawnInterval;
            SpawnCar();
        }
    }

    private void SpawnCar()
    {
        Instantiate(myPrefab, carSpawner.position, carSpawner.rotation);
    }
}
