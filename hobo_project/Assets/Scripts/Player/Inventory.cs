using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public int appleNumber = 0;
    public int money = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(money);
    }

    public void AddApple(int appleNumber)
    {
        this.appleNumber += appleNumber;

        GameController.Instance.NotifyPlayerState(gameObject.GetComponent<PlayerController>(), "update_apple_ui", true, updateAppleText);
    }

    public void SellApples(int applePrice)
    {
        int appleNumber = this.appleNumber;
        this.appleNumber = 0;
        this.money += appleNumber * applePrice;
        GameController.Instance.NotifyPlayerState(gameObject.GetComponent<PlayerController>(), "update_money_ui", true, updateMoneyText);
    }

    private IStateResponse updateMoneyText(GameObject player)
    {
        BaseResponse baseResponse = new BaseResponse();
               
        var canvas = GetUICanvas();
        var uiManager = canvas?.GetComponent<UIManager>();
        if (uiManager)
            uiManager.updateMoney(this.money, player.GetComponent<PlayerController>().GetPlayerIndex());       

        baseResponse.Success = true;
        return baseResponse;
    }

    private IStateResponse updateAppleText(GameObject player)
    {
        BaseResponse baseResponse = new BaseResponse();

        var canvas = GetUICanvas();
        var uiManager = canvas?.GetComponent<UIManager>();
        if (uiManager)
            uiManager.updateApple(this.appleNumber, player.GetComponent<PlayerController>().GetPlayerIndex());

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
