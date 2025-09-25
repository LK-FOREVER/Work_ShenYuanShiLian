using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBoard : View
{
    public GameObject buyTip;
    public Button btn1;
    public Button btn2;
    public Button btn3;
    public Button btn4;

    public BuyTipView buyTipView;


    public void Start()
    {
        btn1.onClick.AddListener(() => OnBtnShowBuyTip("6"));
        btn2.onClick.AddListener(() => OnBtnShowBuyTip("12"));
        btn3.onClick.AddListener(() => OnBtnShowBuyTip("18"));
        btn4.onClick.AddListener(() => OnBtnShowBuyTip("30"));
    }
    public override string Name
    {
        get
        {
            return Consts.V_ShopBoard;
        }
    }
    public override void HandleEvent(object data = null)
    {
    }
    public void OnBtnClose()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        gameObject.SetActive(false);
        SendViewEvent(Consts.E_SaveData);
    }
    public void OnBtnShowBuyTip(string moneyNum)
    {
        buyTip.SetActive(true);
    }
}
