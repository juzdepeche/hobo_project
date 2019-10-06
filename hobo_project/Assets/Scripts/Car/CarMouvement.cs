using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMouvement : MonoBehaviour
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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasToStop)
        {
            GetComponent<Collider>().isTrigger = true;
            transform.position += (transform.forward * speed) * Time.deltaTime;
        }
        else
        {
            GetComponent<Collider>().isTrigger = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !hasToStop)
        {
            GameController.Instance.NotifyPlayerState(other.gameObject.GetComponent<PlayerController>(), "player_death", true, KillPlayer);
        }
    }

    private IStateResponse KillPlayer(GameObject player)
    {
        BaseResponse response = new BaseResponse();
        player.GetComponent<PlayerController>().Die();
        GameController.Instance.NotifyPlayerState(player.GetComponent<PlayerController>(), "player_death", false, null);
        player.transform.position = GameController.Instance.DeadzonePoint.position;
        StartCoroutine(ReviveCharacter(player));
        response.Success = true;
        return response;
    }

    private IEnumerator ReviveCharacter(GameObject player)
    {
        yield return new WaitForSeconds(1f);
        player.transform.position = GameController.Instance.GetRandomSpawnPoint();
        player.SetActive(true);
    }
}
