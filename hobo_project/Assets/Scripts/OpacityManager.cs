using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacityManager : MonoBehaviour
{
    public GameObject building;

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
        if (other.tag == "Player")
        {            
            var mesh = building.GetComponent<MeshRenderer>();
            if (mesh)
                mesh.material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f); ;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            var mesh = building.GetComponent<MeshRenderer>();
            if (mesh)
                mesh.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}
