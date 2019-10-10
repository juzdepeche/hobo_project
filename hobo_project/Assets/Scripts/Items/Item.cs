using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float lifeTime;
    public int value;

    void Start()
    {
        Invoke("DestroyItem", lifeTime);
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        PickupItem(other);
    }

    public virtual void PickupItem(Collision other)
    {
        if (other.collider.tag.Contains("Player") && other.collider.GetComponent<Inventory>())
        {
            other.gameObject.GetComponent<Inventory>().AddApple(value);
            AudioManager.Instance.Play("Pickup");
            DestroyItem();
        }
    }
}
