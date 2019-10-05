﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CarManager : MonoBehaviour
{
    public int id = 0;
    public int lastCarId = 1;

    public GameObject myPrefab;

    public Transform carSpawner;
    public Transform carKiller;

    public bool isActive = false;

    private bool isSpawning = false;
    private bool isStopping = false;

    // Start is called before the first frame update
    void Start()
    {        
        InvokeRepeating("SpawnCar", 1.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown("f"))
            {
                isStopping = !isStopping;
                if (isStopping)
                {
                    isSpawning = false;
                    StopCars(true);
                }
                else
                {
                    isSpawning = true;
                    StopCars(false);
                }
            }
        }
    }

    private void StopCars(bool Stop)
    {
        var cars = FindObjectsOfType<CarMouvement>();
        if (cars.Any())
        {
            if (!Stop)
            {
                lastCarId = 0;
            }

            cars = cars.Where(c => c.spawnId == id && c.needToStop == true).OrderBy(c => c.id).ToArray();

            int parkingId = 1;
            foreach (var car in cars)
            {
                car.askToStop = Stop;
                if (Stop)
                {
                    car.parkingId = parkingId;
                    parkingId++;
                }
                else
                {
                    car.hasToStop = false;
                    car.parkingId = 0;
                }
            }
        }
    }

    private void SpawnCar()
    {
        if (isSpawning)
        {
            GameObject car = Instantiate(myPrefab, carSpawner.position, carSpawner.rotation) as GameObject;
            CarMouvement carMouvement = car.GetComponent<CarMouvement>();

            if (carMouvement)
            {
                carMouvement.id = lastCarId;
                carMouvement.spawnId = id;
                carMouvement.needToStop = true;

                lastCarId++;
            }
        }
    }
}

