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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateApple(int nbApples)
    {
        var nbAppleText = gameObject.GetComponentsInChildren<Text>().FirstOrDefault(t => t.name == "nbApplesText");

        if (nbAppleText)
            nbAppleText.text = nbApples.ToString();

        var imageApple = gameObject.GetComponentsInChildren<Image>().FirstOrDefault(t => t.name == "applesImage").GetComponent<Image>();

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

    public void updateMoney(Canvas canvas, int nbMoney)
    {
        var nbAppleText = gameObject.GetComponentsInChildren<Text>().FirstOrDefault(t => t.name == "nbMoneyText");

        if (nbAppleText)
            nbAppleText.text = nbMoney.ToString();
    }
}
