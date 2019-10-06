using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : MonoBehaviour
{
    public float lifeTime;
    public int value;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBread", lifeTime);
    }

    void DestroyBread()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag.Contains("Player") && other.collider.GetComponent<Inventory>())
        {
            other.gameObject.GetComponent<Inventory>().AddApple(5);
            DestroyBread();
        }
    }
}
