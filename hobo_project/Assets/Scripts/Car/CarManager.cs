using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public GameObject myPrefab;

    public Transform carSpawner;
    public Transform carKiller;

    public float spawnInterval = 0.01f;

    private float nextSpawn = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("LaunchProjectile", 2.0f, 0.3f);
        InvokeRepeating("SpawnCar", 1.0f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnCar()
    {
        Instantiate(myPrefab, carSpawner.position, carSpawner.rotation);
    }
}
