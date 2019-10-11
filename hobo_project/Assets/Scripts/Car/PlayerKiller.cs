using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !GetComponentInParent<CarMovement>().hasToStop)
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            GameController.Instance.NotifyPlayerState(player, "player_death", true, KillPlayer);
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
        yield return new WaitForSeconds(2.5f);
        player.transform.position = GameController.Instance.GetRandomSpawnPoint();
        player.GetComponent<PlayerController>().ResetShank();
        player.SetActive(true);
    }
}
