using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelManager : View
{
    private DateTime _firstLoginOfDay;
    private DateTime _firstLoginOfWeek;

    public DateTime FirstLoginOfDay
    {
        get => _firstLoginOfDay;
        set => _firstLoginOfDay = value;
    }

    public DateTime FirstLoginOfWeek
    {
        get => _firstLoginOfWeek;
        set => _firstLoginOfWeek = value;
    }

    public override string Name { get; }
    public override void HandleEvent(object data = null)
    {
        throw new NotImplementedException();
    }

    public CommonButton InitPanelButton; 
    private void Awake()
    {
        InitData();
        InitPanelButton.OnClick();
    }

    private void InitData()
    {
        // GetModel<GameModel>().Diamond = 20000;
        // GetModel<GameModel>().Coin = 20000;
        // GetModel<GameModel>().StonesCount = 2000;
        GameManager.Instance.CalculateOfflineReward();
        if (GetModel<GameModel>().NickName == null)
        {
            EventManager.Instance.TriggerEvent(EventName.ShowCreateAccount);
        }
        //如果首次登录时间是默认值，则是第一次登录
        if (GetModel<GameModel>().FirstLoginOfDay == default)
        {
            InitEquip();
        }
        
        //检查今日是否首次登录
        if (IsFirstLoginOfDay())
        {
            InitFriends();
            GetModel<GameModel>().DailyTaskProgress.Clear();
            GetModel<GameModel>().TodayQuickAFK = true;
            GetModel<GameModel>().FirstGetTreasure = true;
            for (int i = 0; i < DataManager.Instance.dailyTaskList.Count; i++)
            {
                GetModel<GameModel>().DailyTaskProgress.Add(i+1,new TaskState()
                {

                });
            }
            GlobalTaskCounter.Instance.AddDailyCount(DailyTask.Login);
        }
        //检查本周是否首次登录
        if (IsFirstLoginOfWeek())
        {
            GetModel<GameModel>().WeeklyTaskProgress.Clear();
            for (int i = 0; i < DataManager.Instance.weeklyTaskList.Count; i++)
            {
                GetModel<GameModel>().WeeklyTaskProgress.Add(i+1,new TaskState()
                {

                });
            }
        }

        if (IsFirstLoginOfDay())
        {
            GlobalTaskCounter.Instance.AddWeeklyCount(WeeklyTask.Login);
        }
        
        GetModel<GameModel>().FirstLoginOfDay = DateTime.Today;
    }
    

    private bool IsFirstLoginOfDay()
    {
        DateTime today = DateTime.Today;
        return GetModel<GameModel>().FirstLoginOfDay < today;
    }

    private bool IsFirstLoginOfWeek()
    {
        // 从数据库获取用户的上次登录时间（可能是null）
        DateTime? lastLoginTime = GetModel<GameModel>().FirstLoginOfDay;

        // 调用方法（周一作为周起始日）
        
        bool FirstLoginOfWeek = DateTimeExtensions.IsFirstLoginThisWeek(lastLoginTime, DayOfWeek.Monday);
        return FirstLoginOfWeek;
    }

    private void InitEquip()
    {
        GetModel<GameModel>().OwnedEquipments[EquipType.Weapon].Add(DataManager.Instance.equipmentDic[nameof(EquipType.Weapon)][0]);
        GetModel<GameModel>().OwnedEquipments[EquipType.Necklace].Add(DataManager.Instance.equipmentDic[nameof(EquipType.Necklace)][0]);
        GetModel<GameModel>().OwnedEquipments[EquipType.Cloak].Add(DataManager.Instance.equipmentDic[nameof(EquipType.Cloak)][0]);
        GetModel<GameModel>().OwnedEquipments[EquipType.Head].Add(DataManager.Instance.equipmentDic[nameof(EquipType.Head)][0]);
        GetModel<GameModel>().OwnedEquipments[EquipType.Armor].Add(DataManager.Instance.equipmentDic[nameof(EquipType.Armor)][0]);
        GetModel<GameModel>().OwnedEquipments[EquipType.Legs].Add(DataManager.Instance.equipmentDic[nameof(EquipType.Legs)][0]);
    }
    
    private void InitFriends()
    {
        if (GetModel<GameModel>().FriendList.Count!=0)
        {
            for (int i = 0; i <  GetModel<GameModel>().FriendList.Count; i++)
            {
                GetModel<GameModel>().FriendList[i].alreadyGetMoney = false;
                GetModel<GameModel>().FriendList[i].alreadySendMoney = false;
                GetModel<GameModel>().FriendList[i].isOnline = UnityEngine.Random.Range(0, 2) == 0;
            }
        }
    }
}
