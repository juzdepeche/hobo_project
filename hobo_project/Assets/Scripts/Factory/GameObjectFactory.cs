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
        var apple = Instantiate(Apple, transform.position, transform.rotation);

        float scaler = UnityEngine.Random.Range(0.8f, 1f);
        apple.transform.localScale = new Vector3(scaler, scaler, scaler);

        var appleRb = apple.GetComponent<Rigidbody>();
        appleRb.velocity = new Vector3(appleRb.velocity.x + getOffSetForce(), appleRb.velocity.y + getOffSetForce(), appleRb.velocity.z + getOffSetForce());
    }

    private float getOffSetForce()
    {
        return UnityEngine.Random.Range(-1f, 1f);
    }
}
