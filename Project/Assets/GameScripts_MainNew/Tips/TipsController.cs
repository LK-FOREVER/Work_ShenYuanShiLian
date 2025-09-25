using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TipsController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        EventManager.Instance.AddListener(EventName.ShowCommonTips, ShowCommonTipsEvent);
        EventManager.Instance.AddListener(EventName.ShopEvent, ShowShopTips);
    }



    private void OnDestroy()
    {
        EventManager.Instance.RemoveListener(EventName.ShowCommonTips, ShowCommonTipsEvent);
        EventManager.Instance.RemoveListener(EventName.ShopEvent, ShowShopTips);
    }

    private void ShowShopTips(object sender, EventArgs e)
    {
        BuyTipView buyTips =  transform.Find("BuyTip").gameObject.GetComponent<BuyTipView>();
        SetShopArg shopEvent = e as SetShopArg;
        buyTips.Show(shopEvent);
    }


    private void ShowCommonTipsEvent(object sender, EventArgs e)
    {
        GameObject commonTips =  transform.Find("CommonTips").gameObject;
        commonTips.SetActive(true);
        ShowCommonTips tip = e as ShowCommonTips;
        commonTips.GetComponent<CommonTips>().Show(tip.tipsContent);
    }
}

    
