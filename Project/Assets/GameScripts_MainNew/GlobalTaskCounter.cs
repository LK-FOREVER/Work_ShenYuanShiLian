using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DailyTask
{
    /// <summary>
    /// 完成1次战斗
    /// </summary>
    Battle = 1,
    /// <summary>
    /// 赠送好友金币1次
    /// </summary>
    SendFriendMoney,
    /// <summary>
    /// 领取1次挂机奖励
    /// </summary>
    GetAFKReward,
    /// <summary>
    /// 消灭50名敌人
    /// </summary>
    KillEnemy,
    /// <summary>
    /// 分解1次装备
    /// </summary>
    BreakEquip,
    /// <summary>
    /// 开启1次宝箱
    /// </summary>
    GetTreasure,
    /// <summary>
    /// 每日登录
    /// </summary>
    Login,
    /// <summary>
    /// 在线30分钟
    /// </summary>
    OnlineTime,
}

public enum WeeklyTask
{
    /// <summary>
    /// 完成10次战斗
    /// </summary>
    Battle = 1,
    /// <summary>
    /// 赠送好友金币5次
    /// </summary>
    SendFriendMoney,
    /// <summary>
    /// 领取5次挂机奖励
    /// </summary>
    GetAFKReward,
    /// <summary>
    /// 消灭200名敌人
    /// </summary>
    KillEnemy,
    /// <summary>
    /// 分解10次装备
    /// </summary>
    BreakEquip,
    /// <summary>
    /// 开启10次宝箱
    /// </summary>
    GetTreasure,
    /// <summary>
    /// 每日登录5次
    /// </summary>
    Login,
    /// <summary>
    /// 进行1次领袖挑战
    /// </summary>
    BossChallenge,
}

public class GlobalTaskCounter : SingletonBase<GlobalTaskCounter>
{
    public void AddDailyCount(DailyTask taskType)
    {
        Mvc.GetModel<GameModel>().DailyTaskProgress[(int)taskType].progress++;
    }
    
    public void AddWeeklyCount(WeeklyTask taskType)
    {
        Mvc.GetModel<GameModel>().WeeklyTaskProgress[(int)taskType].progress++;
    }
}
