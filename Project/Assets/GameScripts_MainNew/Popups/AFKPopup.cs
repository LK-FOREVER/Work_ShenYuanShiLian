using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AFKPopup : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public StateButton QuickGetButton;
    public StateButton GetButton;
    public TextMeshProUGUI[] rewardNums;

    private DateTime lastActiveTime;
    private float accumulatedAFKTime;

    private void Start()
    {
        EventManager.Instance.AddListener(EventName.RefreshAFK,RefreshAFKEventHandler);
        QuickGetButton.onClick.AddListener(QuickAFK);
        GetButton.onClick.AddListener(GetAFKReward);
        
    }
    

    private void OnDestroy()
    {
        EventManager.Instance.RemoveListener(EventName.RefreshAFK,RefreshAFKEventHandler);
        QuickGetButton.onClick.RemoveListener(QuickAFK);
        GetButton.onClick.RemoveListener(GetAFKReward);
    }

    private void RefreshAFKEventHandler(object sender, EventArgs e)
    {
        RefreshText();
    }

    private void OnEnable()
    {
        RefreshText();
    }

    public void GetAFKReward()
    {
        GlobalTaskCounter.Instance.AddDailyCount(DailyTask.GetAFKReward);
        GlobalTaskCounter.Instance.AddWeeklyCount(WeeklyTask.GetAFKReward);
        EventManager.Instance.TriggerEvent(EventName.ShowCommonAward, null, new SetAward()
        {
            awardList = new List<AwardInfo>()
            {
                new AwardInfo() { awardType = Mvc.GetModel<GameModel>().CacheAFKCoin == 0 ? RewardType.None : RewardType.Coin, awardNum =  Mvc.GetModel<GameModel>().CacheAFKCoin },
                new AwardInfo() { awardType = Mvc.GetModel<GameModel>().CacheAFKStone == 0 ? RewardType.None : RewardType.Stone,  awardNum =  Mvc.GetModel<GameModel>().CacheAFKStone },
                new AwardInfo() { awardType = Mvc.GetModel<GameModel>().CacheAFKExp == 0 ? RewardType.None : RewardType.Exp,  awardNum =   Mvc.GetModel<GameModel>().CacheAFKExp },
                new AwardInfo() { awardType = Mvc.GetModel<GameModel>().CacheAFKDiamond == 0 ? RewardType.None : RewardType.Diamond,  awardNum =   Mvc.GetModel<GameModel>().CacheAFKDiamond },
            }
        });
        Mvc.GetModel<GameModel>().CacheAFKCoin = 0;
        Mvc.GetModel<GameModel>().CacheAFKStone = 0;
        Mvc.GetModel<GameModel>().CacheAFKExp = 0;
        Mvc.GetModel<GameModel>().CacheAFKDiamond = 0;
        Mvc.GetModel<GameModel>().LastAFKRewardTime = DateTime.Now;
        GameManager.Instance.AFKSecond = 1;
        RefreshText();
    }

    private void Update()
    {
        if (GameManager.Instance.AFKSecond > 43200)
        {
            timerText.text = "挂机时间：12:00:00";
        }
        else
        {
            timerText.text = "挂机时间："+TimeSpan.FromSeconds(GameManager.Instance.AFKSecond).ToString(@"hh\:mm\:ss");
        }
        
    }

    private void RefreshText()
    {
        rewardNums[0].text =  Mvc.GetModel<GameModel>().CacheAFKCoin .ToString();
        rewardNums[1].text =  Mvc.GetModel<GameModel>().CacheAFKStone.ToString();
        rewardNums[2].text =  Mvc.GetModel<GameModel>().CacheAFKExp.ToString();
        rewardNums[3].text =  Mvc.GetModel<GameModel>().CacheAFKDiamond.ToString();
        GetButton.Interactable = Mvc.GetModel<GameModel>().CacheAFKCoin != 0 ||
                            Mvc.GetModel<GameModel>().CacheAFKStone != 0 ||
                            Mvc.GetModel<GameModel>().CacheAFKExp != 0 ||
                            Mvc.GetModel<GameModel>().CacheAFKDiamond != 0;
        QuickGetButton.Interactable = Mvc.GetModel<GameModel>().TodayQuickAFK;
    }

    public void QuickAFK()
    {
        Mvc.GetModel<GameModel>().TodayQuickAFK = false;
        QuickGetButton.Interactable = false;
        GlobalTaskCounter.Instance.AddDailyCount(DailyTask.GetAFKReward);
        GlobalTaskCounter.Instance.AddWeeklyCount(WeeklyTask.GetAFKReward);
        EventManager.Instance.TriggerEvent(EventName.ShowCommonAward, null, new SetAward()
        {
            awardList = new List<AwardInfo>()
            {
                new AwardInfo() { awardType = RewardType.Coin,awardNum = DataManager.Instance.AFKInfoList.Find(info => info.chapter == (Mvc.GetModel<GameModel>().CurLevel - 1) / 20 + 1).coinPerMin * 120},
                new AwardInfo() { awardType = RewardType.Stone,awardNum = DataManager.Instance.AFKInfoList.Find(info => info.chapter == (Mvc.GetModel<GameModel>().CurLevel - 1) / 20 + 1).stonePer30Min * 120 / 30 },
                new AwardInfo() { awardType = RewardType.Exp,awardNum = DataManager.Instance.AFKInfoList.Find(info => info.chapter == (Mvc.GetModel<GameModel>().CurLevel - 1) / 20 + 1).expPer30Min * 120 / 30 },
                new AwardInfo() { awardType = RewardType.Diamond,awardNum = DataManager.Instance.AFKInfoList.Find(info => info.chapter == (Mvc.GetModel<GameModel>().CurLevel - 1) / 20 + 1).diamondPer60Min * 120 / 60 },
            }
        });
    }
}
