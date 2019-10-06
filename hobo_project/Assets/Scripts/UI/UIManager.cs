using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIManager : MonoBehaviour
{
    public Sprite appleState1;
    public Sprite appleState2;
    public Sprite appleState3;
    public Sprite appleState4;
    public Sprite appleState5;

    public Sprite moneyState1;
    public Sprite moneyState2;
    public Sprite moneyState3;
    public Sprite moneyState4;
    public Sprite moneyState5;

    private bool gameOverVisible = false;

    public GameObject inGame;
    public GameObject gameOver;

    public void updateApple(int nbApples, int playerIndex)
    {
        var nbAppleText = gameObject.GetComponentsInChildren<Text>().FirstOrDefault(t => t.name == "nbApplesText_" + playerIndex);

        if (nbAppleText)
            nbAppleText.text = nbApples.ToString();

        var imageApple = gameObject.GetComponentsInChildren<Image>().FirstOrDefault(t => t.name == "applesImage_" + playerIndex)?.GetComponent<Image>();

        if(imageApple)
        {
            if (nbApples < 2)
            {
                imageApple.sprite = appleState1;
            }
            else if (nbApples < 4)
            { 
                imageApple.sprite = appleState2;             
            }
            else if (nbApples < 6)
            {
                imageApple.sprite = appleState3;
            }
            else if (nbApples < 8)
            {
                imageApple.sprite = appleState4;
            }
            else
            {
                imageApple.sprite = appleState5;
            }                    
        }
    }

    public void updateMoney(int nbMoney, int playerIndex)
    {
        var nbMoneyText = gameObject.GetComponentsInChildren<Text>().FirstOrDefault(t => t.name == "nbMoneyText_" + playerIndex);

        if (nbMoneyText)
            nbMoneyText.text = nbMoney.ToString();

        var imageMoney = gameObject.GetComponentsInChildren<Image>().FirstOrDefault(t => t.name == "moneyImage_" + playerIndex)?.GetComponent<Image>();

        if (imageMoney)
        {
            if (nbMoney < 5)
            {
                imageMoney.sprite = moneyState1;
            }
            else if (nbMoney < 10)
            {
                imageMoney.sprite = moneyState2;
            }
            else if (nbMoney < 15)
            {
                imageMoney.sprite = moneyState3;
            }
            else if (nbMoney < 20)
            {
                imageMoney.sprite = moneyState4;
            }
            else
            {
                imageMoney.sprite = moneyState5;
            }
        }
    }

    public void updateTimer(float time, float maxTime)
    {
        var timerText = gameObject.GetComponentsInChildren<Text>().FirstOrDefault(t => t.name == "timerText");
        if (timerText)
        {
            float remaningTime = (maxTime - time);

            if (remaningTime > 0.0f)
            {
                string minutes = "0" + ((int)remaningTime / 60).ToString();
                string seconds = (remaningTime % 60).ToString("f0"); // Only two decimals

                if (seconds.Length == 1)
                    seconds = "0" + seconds;

                timerText.text = minutes + " : " + seconds;
            }
        }
    }

    public void showGameOver(int moneyPlayer1, int moneyPlayer2)
    {
        inGame.SetActive(false);
        gameOver.SetActive(true);

        var finalMoneyPlayer1 = gameObject.GetComponentsInChildren<Text>().FirstOrDefault(t => t.name == "finalMoneyText_1");
        var finalMoneyPlayer2 = gameObject.GetComponentsInChildren<Text>().FirstOrDefault(t => t.name == "finalMoneyText_2");

        if (finalMoneyPlayer1)
            finalMoneyPlayer1.text = moneyPlayer1.ToString() + "$";

        if (finalMoneyPlayer2)
            finalMoneyPlayer2.text = moneyPlayer2.ToString() + "$";
    }
}
