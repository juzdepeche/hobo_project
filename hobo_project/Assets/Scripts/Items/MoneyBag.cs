using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBag : MonoBehaviour
{
    public float lifeTime;
    public int value;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyMoneyBag", lifeTime);
    }

    void DestroyMoneyBag()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag.Contains("Player") && other.collider.GetComponent<Inventory>() && !other.collider.GetComponent<PlayerController>().isShanked)
        {
            other.gameObject.GetComponent<Inventory>().AddMoney(value);
            other.gameObject.GetComponent<PlayerController>().SparkMoney();
            DestroyMoneyBag();
        }
    }
}
