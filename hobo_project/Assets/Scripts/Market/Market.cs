using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    public int ApplePrice;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameController.Instance.NotifyPlayerState(other.gameObject.GetComponent<PlayerController>(), "apple_market", true, SellApples);
        }
    }

    private IStateResponse SellApples(GameObject player)
    {
        IStateResponse response = new MarketResponse();
        AudioManager.Instance.Play("Sell");
        var inventory = player.GetComponent<Inventory>();
        if(inventory)
            inventory.SellApples(ApplePrice);

        response.Success = true;
        return response;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameController.Instance.NotifyPlayerState(other.gameObject.GetComponent<PlayerController>(), "apple_market", false);
        }
    }
}
