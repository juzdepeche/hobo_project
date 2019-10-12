using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSquidgy : MonoBehaviour
{
    private bool wantToBeSquid = false;
    private bool hasBeenSquid = false;

    private GameObject interactionPossible;

    private void Start()
    {
        
    }

    public void doIwantToBeSquid()
    {
        wantToBeSquid = Random.Range(0, 4) == 0;
        gameObject.SetActive(wantToBeSquid);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameController.Instance.NotifyPlayerState(other.gameObject.GetComponent<PlayerController>(), "Squidgy_" + gameObject.name, true, Squid);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameController.Instance.NotifyPlayerState(other.gameObject.GetComponent<PlayerController>(), "Squidgy_" + gameObject.name, false, Squid);
        }
    }

    private IStateResponse Squid(GameObject player)
    {
        BaseResponse response = new BaseResponse();
        response.Success = true;
        if (!hasBeenSquid && wantToBeSquid)
        {
            hasBeenSquid = true;

            var ctrl = player.GetComponent<PlayerController>();
            var inventory = player.GetComponent<Inventory>();
            if (ctrl)
            {
                inventory.AddMoney(Random.Range(5, 10));
                ctrl.SparkMoney();
            }

            wantToBeSquid = false;
            gameObject.SetActive(wantToBeSquid);
        }

        return response;
    }
}
