using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shank : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameController.Instance.NotifyPlayerState(other.gameObject.GetComponent<PlayerController>(), "player_shank_zone_" + gameObject.name, true, ShankPlayer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameController.Instance.NotifyPlayerState(other.gameObject.GetComponent<PlayerController>(), "player_shank_zone_" + gameObject.name, false, ShankPlayer);
        }
    }

    private IStateResponse ShankPlayer(GameObject player)
    {
        BaseResponse response = new BaseResponse();
        response.Success = true;

        var ctrl = GetComponentInParent<PlayerController>();

        ctrl.Shank();

        return response;
    }
}
