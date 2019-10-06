using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public int appleNumber = 0;
    public int money = 0;

    public void AddApple(int appleNumber)
    {
        this.appleNumber += appleNumber;

        var audio = gameObject.GetComponentsInParent<AudioSource>().FirstOrDefault(a => a.clip.name.Contains("pickup"));        
        if (audio)
            audio.Play();
            
        GameController.Instance.NotifyPlayerState(gameObject.GetComponent<PlayerController>(), "update_apple_ui", true, updateAppleText);
    }

    public void AddMoney(int moneyAMount)
    {
        money += moneyAMount;

        var audio = gameObject.GetComponentsInParent<AudioSource>().FirstOrDefault(a => a.clip.name.Contains("sell"));
        if (audio)
            audio.Play();

        GameController.Instance.NotifyPlayerState(gameObject.GetComponent<PlayerController>(), "update_money_ui", true, updateMoneyText);
    }

    public void SellApples(int applePrice)
    {
        int appleNumberTemp = appleNumber;
        appleNumber = 0;
        int moneyMade = appleNumberTemp * applePrice;
        money += moneyMade;

        PlayerController ctrl = gameObject.GetComponent<PlayerController>();
        if (ctrl && moneyMade > 0)
        {
            ctrl.SparkMoney();
        }

        GameController.Instance.NotifyPlayerState(gameObject.GetComponent<PlayerController>(), "update_money_ui", true, updateMoneyText);
    }

    private IStateResponse updateMoneyText(GameObject player)
    {
        BaseResponse baseResponse = new BaseResponse();
               
        var canvas = GetUICanvas();
        var uiManager = canvas?.GetComponent<UIManager>();
        if (uiManager)
            uiManager.updateMoney(money, player.GetComponent<PlayerController>().GetPlayerIndex());       

        baseResponse.Success = true;
        return baseResponse;
    }

    private IStateResponse updateAppleText(GameObject player)
    {
        BaseResponse baseResponse = new BaseResponse();

        var canvas = GetUICanvas();
        var uiManager = canvas?.GetComponent<UIManager>();

        if (uiManager)
            uiManager.updateApple(appleNumber, player.GetComponent<PlayerController>().GetPlayerIndex());

        baseResponse.Success = true;
        return baseResponse;
    }

    private Canvas GetUICanvas()
    {
        var canvas = GameObject.FindObjectOfType<Canvas>();

        if (!canvas)
            return null;

        return canvas;
    }
}
