using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public float Attraction = 3f;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, other.transform.position, Attraction * Time.deltaTime);
        }
    }
}
