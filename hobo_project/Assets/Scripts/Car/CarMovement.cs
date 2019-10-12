using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CarMovement : MonoBehaviour
{
    public int id = 0;
    public int spawnId = 0;
    public int lastParkingHit = 0;
    public int parkingId = 0;

    // Is change when the car pass trough the stop line.
    public bool needToStop = true;
    // The manager as the car to stop at the next oportunity.
    public bool askToStop = false;
    // When the oportunity as happened make the carstop.
    public bool hasToStop = false;


    private float speed = 10.0f;

    bool randomSquidgy = false;
    private CarSquidgy squidgy;
    
    // Start is called before the first frame update
    void Start()
    {
        squidgy = gameObject.GetComponentInChildren<CarSquidgy>();
        if (squidgy)
        {
            squidgy.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasToStop)
        {
            GetComponent<Collider>().isTrigger = true;
            transform.position += (transform.forward * speed) * Time.deltaTime;

            if (squidgy && randomSquidgy)
            {
                squidgy.gameObject.SetActive(false);
            }
        }
        else
        {
            GetComponent<Collider>().isTrigger = false;
            if (squidgy)
            {
                if (!randomSquidgy)
                {
                    randomSquidgy = true;
                    squidgy.doIwantToBeSquid();
                }
            }
        }
    }
}
