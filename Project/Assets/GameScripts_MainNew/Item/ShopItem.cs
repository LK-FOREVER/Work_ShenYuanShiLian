using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShopItem : MonoBehaviour
{
    public TextMeshProUGUI costText;

    public GameObject soldOutObj;

    public GameObject limitObj;
    public TextMeshProUGUI limitText;

    public GameObject costTypeObj;
    public Image costTypeIcon;
    public Sprite[] iconSprites;
    public TextMeshProUGUI costNum;

    public Image icon;
    public TextMeshProUGUI desc;

    private ShopItemInfo curShopItemInfo;

    public Button buyButton;
    private LimitType curLimitType;


    private void Start()
    {
        buyButton.onClick.AddListener(SendBuyEvent);
    }
    
    private void OnDestroy()
    {
        buyButton.onClick.RemoveListener(SendBuyEvent);
    }
    
    public void Init(ShopItemInfo shopItemInfo)
    {
        curShopItemInfo = shopItemInfo;

        //限购设置
        Enum.TryParse(shopItemInfo.limitType, out LimitType limitType);
        curLimitType = limitType;
        if (limitType != LimitType.Limitless)
        {
            limitText.text = string.Format($"限购{shopItemInfo.buyLimit}次");
        }

        //商品类型设置（根据花销模式）
        Enum.TryParse(shopItemInfo.costType, out CostType costType);
        if (costType == CostType.Free || costType == CostType.Cash)
        {
            costTypeObj.SetActive(false);
            costText.gameObject.SetActive(true);
            costText.text = costType == CostType.Free ? "免费" : shopItemInfo.costNum + "元";
        }
        else
        {
            costTypeObj.SetActive(true);
            costText.gameObject.SetActive(false);
            costTypeIcon.sprite = costType == CostType.Coin ? iconSprites[0] : iconSprites[1];
            costNum.text = shopItemInfo.costNum.ToString();
        }
        RefreshInfo();
        desc.text = shopItemInfo.buyDesc;
        icon.sprite = SpriteManager.Instance.GetShopSprite(shopItemInfo.buyDesc);
        icon.SetNativeSize();
    }

    public void RefreshInfo()
    {
        limitObj.SetActive(curLimitType != LimitType.Limitless);
        switch (curLimitType)
        {
            case LimitType.Daily:
            case LimitType.Weekly:
                int[] curLimit = curLimitType == LimitType.Daily
                    ? Mvc.GetModel<GameModel>().DailyBuyLimit
                    : Mvc.GetModel<GameModel>().WeeklyBuyLimit;
                if (curLimit[curShopItemInfo.id] >= curShopItemInfo.buyLimit)
                {
                    SetSoldOut();
                }
                break;
            case LimitType.Limitless:
                soldOutObj.SetActive(false);
                break;
        }
    }
    
    private void SetSoldOut()
    {
        soldOutObj.SetActive(true);
        costTypeObj.SetActive(false);
        costText.gameObject.SetActive(false);
    }
    
    
    private void SendBuyEvent()
    {
        EventManager.Instance.TriggerEvent(EventName.ShopEvent,null,new SetShopArg()
        {
            curShopItemInfo =  curShopItemInfo,
        });
    }
}
