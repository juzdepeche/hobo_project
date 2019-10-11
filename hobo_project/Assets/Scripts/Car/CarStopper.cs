using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStopper : MonoBehaviour
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
            var car = other.gameObject.GetComponent<CarMovement>();
            if (car)
            {
                car.needToStop = false;
            }
        }
    }
}
