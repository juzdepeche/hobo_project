using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPossible : MonoBehaviour
{

    private Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        findCameraTransform();
    }

    // Update is called once per frame
    void Update()
    {
        fixRotation();
    }

    void findCameraTransform()
    {
        var camera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraTransform = camera.transform;
    }

    void fixRotation()
    {       
        gameObject.transform.rotation = cameraTransform.rotation;
    }

}
