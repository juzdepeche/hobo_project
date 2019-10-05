using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StreetFlowManager : MonoBehaviour
{
    private float timePeriode = 0.0f;
    public float interval = 5.5f;

    private List<int[]> StreetMatchingId = new List<int[]>() { new int[] { 1, 2 }, new int[] { 3, 4 } };
    private bool street1Active = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timePeriode += Time.deltaTime;
        if (timePeriode >= interval)
        {
            timePeriode = 0.0f;
            StartCoroutine(ChangeStreetFlow());
        }        
    }

    private IEnumerator ChangeStreetFlow()
    {
        var carManagers = GameObject.FindObjectsOfType<CarManager>();
        var carManagersStreet1 = carManagers.Where(c => c.id == StreetMatchingId[0][0] || c.id == StreetMatchingId[0][1]);
        var carManagersStreet2 = carManagers.Where(c => c.id == StreetMatchingId[1][0] || c.id == StreetMatchingId[1][1]);

        ChangeFlow(false, carManagersStreet1.ToArray());
        ChangeFlow(false, carManagersStreet2.ToArray());
        yield return new WaitForSeconds(1f);

        ChangeFlow(street1Active, carManagersStreet1.ToArray());
        ChangeFlow(!street1Active, carManagersStreet2.ToArray());

        street1Active = !street1Active;
    }

    private void ChangeFlow(bool stop, CarManager[] carManagers)
    {
        foreach (var carManager in carManagers)
        {
            var script = carManager.GetComponent<CarManager>();
            if (script)
                script.Stop(stop);
        }
    }
}
