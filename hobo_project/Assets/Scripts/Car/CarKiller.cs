using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarKiller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            Destroy(other.gameObject);
        }
    }
}
