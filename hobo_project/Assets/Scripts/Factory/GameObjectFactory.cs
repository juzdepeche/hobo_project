using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectFactory : MonoBehaviour
{
    public static GameObjectFactory Instance;

    public GameObject Apple;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    public void SpawnApple(Transform transform)
    {
        Instantiate(Apple, transform.position, Quaternion.identity);
    }
}
