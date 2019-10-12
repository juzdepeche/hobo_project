using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBag : Item
{
    public override void PickupItem(Collision other)
    {
        if (other.collider.tag.Contains("Player") && other.collider.GetComponent<Inventory>() && !other.collider.GetComponent<PlayerController>().isShanked)
        {
            other.gameObject.GetComponent<Inventory>().AddMoney(value);
            other.gameObject.GetComponent<PlayerController>().SparkMoney();
            DestroyItem();
        }
    }
}
