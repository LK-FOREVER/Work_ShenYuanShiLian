using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPopup : View
{
    
    public override string Name { get; }
    public override void HandleEvent(object data = null)
    {
        throw new NotImplementedException();
    }
    
    public GameObject taskPrefab;
    public Transform dailyTaskParent;
    public Transform weeklyTaskParent;
    
    private bool firstOpen = true;
    
    private void OnEnable()
    {
        if (!firstOpen)
        {
            RefreshDailyTask();
            RefreshWeeklyTask();
        }
        else
        {
            Init();
        }
    }

    public void Init()
    {
        firstOpen = false;
        List<TaskInfoData> dailyTasks = DataManager.Instance.dailyTaskList;
        for (int i = 0; i < dailyTasks.Count; i++)
        {
            TaskItem taskItem = MainObjectPool.Instance.SpawnFromPool("Task_Content", Vector3.zero, Quaternion.identity).GetComponent<TaskItem>();
            taskItem.transform.SetParent(dailyTaskParent);
            taskItem.transform.localPosition = Vector3.zero;
            taskItem.transform.localScale = Vector3.one;
            taskItem.currentProgress = GetModel<GameModel>().DailyTaskProgress[i + 1].progress;
            taskItem.Init(dailyTasks[i],true);
        }
        
        List<TaskInfoData> weeklyTasks = DataManager.Instance.weeklyTaskList;
        for (int i = 0; i < weeklyTasks.Count; i++)
        {
            TaskItem taskItem = MainObjectPool.Instance.SpawnFromPool("Task_Content", Vector3.zero, Quaternion.identity).GetComponent<TaskItem>();
            taskItem.transform.SetParent(weeklyTaskParent);
            taskItem.transform.localPosition = Vector3.zero;
            taskItem.transform.localScale = Vector3.one;
            taskItem.currentProgress = GetModel<GameModel>().WeeklyTaskProgress[i + 1].progress;
            taskItem.Init(weeklyTasks[i],false);
        }
     
    }

    private void RefreshDailyTask()
    {
        List<TaskInfoData> dailyTasks = DataManager.Instance.dailyTaskList;
        for (int i = 0; i < dailyTaskParent.transform.childCount; i++)
        {
            TaskItem taskItem = dailyTaskParent.transform.GetChild(i).GetComponent<TaskItem>();
            taskItem.currentProgress = GetModel<GameModel>().DailyTaskProgress[i + 1].progress;
            taskItem.RefreshInfo(dailyTasks[i]);
        }
    }
    
    private void RefreshWeeklyTask()
    {
        List<TaskInfoData> weeklyTasks = DataManager.Instance.weeklyTaskList;
        for (int i = 0; i < weeklyTaskParent.transform.childCount; i++)
        {
            TaskItem taskItem = weeklyTaskParent.transform.GetChild(i).GetComponent<TaskItem>();
            taskItem.currentProgress = GetModel<GameModel>().WeeklyTaskProgress[i + 1].progress;
            taskItem.RefreshInfo(weeklyTasks[i]);
        }
    }

    public void GetAll()
    {
        int totalCoin = 0;
        int totalDiamond = 0;
        int totalStone = 0;
        List<AwardInfo> rewardList = new List<AwardInfo>() ;
        List<EquipInfoData> allRewardEquip = new List<EquipInfoData>();
        List<TaskItem> taskItems = new List<TaskItem>();
        for (int i = 0; i < dailyTaskParent.transform.childCount; i++)
        {
            TaskItem taskItem = dailyTaskParent.transform.GetChild(i).GetComponent<TaskItem>();
            taskItems.Add(taskItem);
        }
        for (int i = 0; i < weeklyTaskParent.transform.childCount; i++)
        {
            TaskItem taskItem = weeklyTaskParent.transform.GetChild(i).GetComponent<TaskItem>();
            taskItems.Add(taskItem);
        }
        bool hasReward = false;
        foreach (var taskItem in taskItems)
        {
            bool isGetReward = (taskItem.isDailyTask &&
                                Mvc.GetModel<GameModel>().gameData.dailyTaskProgress[taskItem.curTaskId].isGetReward) ||
                               (!taskItem.isDailyTask && Mvc.GetModel<GameModel>().gameData
                                   .weeklyTaskProgress[taskItem.curTaskId].isGetReward);
            if (taskItem.isFinish && !isGetReward)
            {
                Enum.TryParse(taskItem.curTaskInfo.rewardType, out RewardType rewardType);
                switch (rewardType)
                {
                    case RewardType.Coin:
                        totalCoin += taskItem.curTaskInfo.rewardNum;
                        break;
                    case RewardType.Diamond:
                        totalDiamond += taskItem.curTaskInfo.rewardNum;
                        break;
                    case RewardType.Stone:
                        totalStone += taskItem.curTaskInfo.rewardNum;
                        break;
                    case RewardType.Equip:
                        EquipInfoData insEquip = taskItem.GetRandomEquip(taskItem.curRewardQuality);
                        allRewardEquip.Add(insEquip);
                        break;
                }
                taskItem.RefreshRewardState();
                hasReward = true;
            }
        }
        if (!hasReward)
        {
            return;
        }
        rewardList.Add(new AwardInfo(){awardType = RewardType.Coin, awardNum = totalCoin});
        rewardList.Add(new AwardInfo(){awardType = RewardType.Diamond, awardNum = totalDiamond});
        rewardList.Add(new AwardInfo(){awardType = RewardType.Stone, awardNum = totalStone});
        foreach (var rewardEquip in allRewardEquip)
        {
            rewardList.Add(new AwardInfo(){awardType = RewardType.Equip, awardEquip = rewardEquip});
        }
        
        EventManager.Instance.TriggerEvent(EventName.ShowCommonAward,null,new SetAward()
        {
            awardList = rewardList
        });
    }
}
