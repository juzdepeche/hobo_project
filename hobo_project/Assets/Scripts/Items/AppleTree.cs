using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    public GameObject Apple;
    public Transform[] SpawnPoints;

    
    public float minIntervalSpawn;
    public float maxIntervalSpawn;

    private float time = 0;
    private float spawnInterval = 1;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= spawnInterval)
        {
            SpawnApple();
            SetSpawnInterval();
        }
    }

    private void SetSpawnInterval()
    {
        spawnInterval = UnityEngine.Random.Range(minIntervalSpawn, maxIntervalSpawn);
    }

    private void SpawnApple()
    {
        time = 0;
        int spawnPointIndex = UnityEngine.Random.Range(0, SpawnPoints.Length);
        Transform spawnPoint = SpawnPoints[spawnPointIndex];
        var apple = Instantiate(Apple, spawnPoint.position, spawnPoint.rotation);

        float scaler = UnityEngine.Random.Range(0.8f, 1f);
        apple.transform.localScale = new Vector3(scaler, scaler, scaler);

        var appleRb = apple.GetComponent<Rigidbody>();
        appleRb.velocity = new Vector3(appleRb.velocity.x + getOffSetForce(), appleRb.velocity.y, appleRb.velocity.z + getOffSetForce());
    }

    private float getOffSetForce()
    {
        return UnityEngine.Random.Range(-0.5f, 0.5f);
    }
}
