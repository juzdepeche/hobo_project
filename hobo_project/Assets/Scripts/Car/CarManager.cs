using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CarManager : MonoBehaviour
{
    public int id = 0;
    public int lastCarId = 1;

    public GameObject Car;
    public GameObject Pickup;
    public GameObject Bus;

    public Material[] CarMaterials;
    public Material[] PickupMaterials;
    public Material[] BusMaterials;

    public Transform carSpawner;
    public Transform carKiller;

    public bool isActive = false;

    private bool isSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnCar", 1.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Stop(bool isStopping)
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
                    if (car.lastParkingHit != parkingId)
                    {
                        car.parkingId = parkingId;
                        parkingId++;
                    }
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
            GameObject car = Instantiate(GetRandomVehicule(), carSpawner.position, carSpawner.rotation) as GameObject;
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

    private GameObject GetRandomVehicule()
    {
        var choosenVehiculeIndex = Random.Range(0, 3);

        switch (choosenVehiculeIndex)
        {
            //car
            case 0:
                Car.GetComponent<Renderer>().material = CarMaterials[Random.Range(0, CarMaterials.Length)];
                return Car;
            //pickup
            case 1:
                Pickup.GetComponent<Renderer>().material = PickupMaterials[Random.Range(0, PickupMaterials.Length)];
                return Pickup;
            //bus
            case 2:
                Bus.GetComponent<Renderer>().material = BusMaterials[Random.Range(0, BusMaterials.Length)];
                return Bus;
        }

        return null;
    }
}

