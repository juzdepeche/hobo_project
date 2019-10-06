using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tent : MonoBehaviour
{
    public Material Skin1;
    public Material Skin2;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = Random.Range(0, 2) == 0 ? Skin1 : Skin2;
    }
}
