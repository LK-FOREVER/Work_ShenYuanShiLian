using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TipsController : MonoBehaviour
{
    // public override string Name => "TipsController";
    // public override void HandleEvent(object data = null)
    // {

    // }

    // Start is called before the first frame update
    private void Start()
    {
        EventManager.Instance.AddListener(EventName.ShowCommonTips, ShowCommonTipsEvent);
        EventManager.Instance.AddListener(EventName.ShopEvent, ShowShopTips);
        EventManager.Instance.AddListener(EventName.Recharge, ShowRechargeTips);
    }



    private void OnDestroy()
    {
        EventManager.Instance.RemoveListener(EventName.ShowCommonTips, ShowCommonTipsEvent);
        EventManager.Instance.RemoveListener(EventName.ShopEvent, ShowShopTips);
        EventManager.Instance.RemoveListener(EventName.Recharge, ShowRechargeTips);
    }

    private void ShowShopTips(object sender, EventArgs e)
    {
        BuyTipView buyTips = transform.Find("BuyTip").gameObject.GetComponent<BuyTipView>();
        SetShopArg shopEvent = e as SetShopArg;
        buyTips.Show(shopEvent);
    }


    private void ShowCommonTipsEvent(object sender, EventArgs e)
    {
        GameObject commonTips = transform.Find("CommonTips").gameObject;
        commonTips.SetActive(true);
        ShowCommonTips tip = e as ShowCommonTips;
        commonTips.GetComponent<CommonTips>().Show(tip.tipsContent);
    }
    private void ShowRechargeTips(object sender, EventArgs e)
    {
        GameObject rechargeTips = transform.Find("RechargeTips").gameObject;
        rechargeTips.SetActive(true);
        int totalRecharge = GameManager.Instance.getTotalRecharge();
        int remanent = 400 - totalRecharge;
        if (remanent >= 0)
            rechargeTips.transform.Find("Content_2").GetComponent<Text>().text = $"本月已累计充值{totalRecharge}元，还可充值{remanent}元。";    
    }
}


