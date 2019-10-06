using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacityManager : MonoBehaviour
{
    public GameObject building;

    public Shader shader;

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
                mesh.material.shader = shader;            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            var mesh = building.GetComponent<MeshRenderer>();
            if (mesh)
                mesh.material.shader = Shader.Find("Standard"); 
        }
    }
}
