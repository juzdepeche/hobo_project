using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squidgy : MonoBehaviour
{
    private bool hasBeenSquid = false;

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
        if (!hasBeenSquid)
        {
            hasBeenSquid = true;
            bool playerGetsMoney = Random.Range(0, 3) == 0;

            if (playerGetsMoney)
            {
                var ctrl = player.GetComponent<PlayerController>();
                var inventory = player.GetComponent<Inventory>();
                if (ctrl)
                {
                    inventory.AddMoney(Random.Range(1, 6));
                    ctrl.SparkMoney();
                }
            }
        }

        return response;
    }
}
