using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuyTipView : View
{
    public override string Name
    {
        get
        {
            return Consts.V_BuyTipView;
        }
    }
    
    private string moneyNum;
    [SerializeField] GameObject monthlyLimitPopup;
    [SerializeField] GameObject singleLimitPopup;

    public SetShopArg curShopArg;

    private string tipsText;
    private bool ageTips;
    
    private Dictionary<int, string> equipTypeDic = new Dictionary<int, string>()
    {
        {0,"Weapon"},
        {1,"Necklace"},
        {2,"Cloak"},
        {3,"Head"},
        {4,"Armor"},
        {5,"Legs"},
    };
    
    
    
    public void Show(SetShopArg shopArg)
    {
        gameObject.SetActive(true);
        curShopArg = shopArg;
        tipsText = "";
        ageTips = false;
    }

    private bool JudgeCanBuy()
    {
        bool canBuy = false;
        Enum.TryParse(curShopArg.curShopItemInfo.limitType, out LimitType limitType);
        int[] curLimit = limitType == LimitType.Daily
            ? Mvc.GetModel<GameModel>().DailyBuyLimit
            : Mvc.GetModel<GameModel>().WeeklyBuyLimit;
        Enum.TryParse(curShopArg.curShopItemInfo.costType, out CostType costType);
        if (costType!=CostType.Cash)
        {
            if ((curLimit[curShopArg.curShopItemInfo.id] + 1 <= curShopArg.curShopItemInfo.buyLimit))
            {
                if (costType!=CostType.Free)
                {
                    int costTypeNum = costType == CostType.Coin
                        ? Mvc.GetModel<GameModel>().Coin
                        : Mvc.GetModel<GameModel>().Diamond;
                    if (costTypeNum >= curShopArg.curShopItemInfo.costNum)
                    {
                        tipsText = "购买成功";
                        canBuy = true;
                    }
                    else
                    {
                        tipsText = costType == CostType.Coin ? "金币不足，购买失败" : "钻石不足，购买失败";
                    }
                
                }
                else
                {
                    tipsText = "购买成功";
                    canBuy = true;
                }
            }
            else
            {
                tipsText = "已超出限购次数";
                canBuy = false;
            }
        }
        else
        {
            tipsText = "购买成功";
            canBuy = true;
        }
        
        if (GetModel<GameModel>().Age == 2 && costType == CostType.Cash)
        {
            var (isMonthlyExceed, isSingleExceed) = GameManager.Instance.CheckRecharge(curShopArg.curShopItemInfo.costNum);
            if (isSingleExceed)
            {
                singleLimitPopup.SetActive(true);
                ageTips = true;
                canBuy = false ;
            }
            else if (isMonthlyExceed)
            {
                monthlyLimitPopup.SetActive(true);
                ageTips = true;
                canBuy = false ;
            }
        }
        return canBuy;
    }

    public void OnCancelBtn()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        gameObject.SetActive(false);
    }

    public void OnConfirmBtn()
    {
        Enum.TryParse(curShopArg.curShopItemInfo.limitType, out LimitType limitType);
        Enum.TryParse(curShopArg.curShopItemInfo.costType, out CostType costType);
        Enum.TryParse(curShopArg.curShopItemInfo.buyType, out RewardType buyType);
        if (JudgeCanBuy() || costType == CostType.Cash)
        {
            switch (limitType)
            {
                case LimitType.Daily:
                    Mvc.GetModel<GameModel>().DailyBuyLimit[curShopArg.curShopItemInfo.id]++;
                    break;
                case LimitType.Weekly:
                    Mvc.GetModel<GameModel>().WeeklyBuyLimit[curShopArg.curShopItemInfo.id]++;
                    break;
            }
            switch (costType)
            {
                case CostType.Coin:
                    Mvc.GetModel<GameModel>().Coin -= curShopArg.curShopItemInfo.costNum;
                    break;
                case CostType.Diamond:
                    Mvc.GetModel<GameModel>().Diamond -= curShopArg.curShopItemInfo.costNum;
                    break;
                case CostType.Cash:
                    GameManager.Instance.ApplyRecharge(curShopArg.curShopItemInfo.costNum);
                    break;
            }

            switch (buyType)
            {
                case RewardType.Equip:
                    EquipInfoData randomEquip = GetPurpleTreasure();
                    EventManager.Instance.TriggerEvent(EventName.ShowCommonAward,null,new SetAward()
                    {
                        awardList = new List<AwardInfo>()
                        {
                            new AwardInfo()
                            {
                                awardType = RewardType.Equip,
                                awardEquip = randomEquip
                            },
                        }
                    });
                    break;
                default:
                    EventManager.Instance.TriggerEvent(EventName.ShowCommonAward,null,new SetAward()
                    {
                        awardList = new List<AwardInfo>()
                        {
                            new AwardInfo()
                            {
                                awardType = buyType,
                                awardNum = curShopArg.curShopItemInfo.buyNum
                            },
                        }
                    });
                    break;
            }
        }
        else
        {
            if (!ageTips)
            {
                EventManager.Instance.TriggerEvent(EventName.ShowCommonTips,null,new ShowCommonTips()
                {
                    tipsContent = tipsText
                });
            }
        }
       

        EventManager.Instance.TriggerEvent(EventName.RefreshShop);
        
        gameObject.SetActive(false);
    }
    
    public EquipInfoData GetPurpleTreasure()
    {
        int result = 2;
        int randomEquip = UnityEngine.Random.Range(0, 6);
        List<EquipInfoData> selectedInfo = DataManager.Instance.equipmentDic[equipTypeDic[randomEquip]];
        List<EquipInfoData> qualityEquipList = selectedInfo.Where(a => a.quality == result).ToList();
        int randomIndex = UnityEngine.Random.Range(0, qualityEquipList.Count);
        EquipInfoData finalEquip = qualityEquipList[randomIndex];
        return finalEquip;
    }
    
    public override void HandleEvent(object data = null)
    {

    }
}
