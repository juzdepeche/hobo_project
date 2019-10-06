using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    private float timePeriode = 0.0f;
    public float timeReloadGame = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timePeriode += Time.deltaTime;
        if (timePeriode >= timeReloadGame)
        {
            SceneManager.LoadScene(0);
        }
    }
}
