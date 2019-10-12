using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRotator : MonoBehaviour
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
            var car = other.GetComponent<CarMovement>();
            var managerOfThisKiller = this.gameObject.GetComponentInParent<CarManager>();

            if (car && managerOfThisKiller && managerOfThisKiller.id == car.spawnId)
            {
                //Destroy(other.gameObject);
                // Rotate the cube by converting the angles into a quaternion.
                Quaternion target = Quaternion.Euler(90, car.transform.rotation.y, car.transform.rotation.z);

                // Dampen towards the target rotation
                car.transform.Rotate(90, 0, 0);
            }
        }
    }
}
