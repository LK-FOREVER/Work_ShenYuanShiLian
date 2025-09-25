using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour
{
    public TextMeshProUGUI taskDesc;
    public TextMeshProUGUI taskTarget;
    public TextMeshProUGUI progressText;
    public Button awardButton;
    public GameObject alreadyGet;

    public GameObject awardItem;
    public GameObject awardEquip;
    public Transform insParent;
    [HideInInspector]
    public int currentProgress;
    [HideInInspector]
    public bool isFinish;

    [HideInInspector] public TaskInfoData curTaskInfo;
    private string rewardName;

    [HideInInspector]
    public RewardType curRewardType;
    [HideInInspector]
    public int curRewardNum;
    [HideInInspector]
    public int curRewardQuality;
    [HideInInspector]
    public int curTaskId;
    [HideInInspector]
    public EquipInfoData curRewardEquip;
    public bool isDailyTask;

    private Dictionary<int, string> equipTypeDic = new Dictionary<int, string>()
    {
        {0,"Weapon"},
        {1,"Necklace"},
        {2,"Cloak"},
        {3,"Head"},
        {4,"Armor"},
        {5,"Legs"},
    };


    private void Start()
    {
        awardButton.onClick.AddListener(GetReward);
    }
    
    private void OnDestroy()
    {
        awardButton.onClick.RemoveListener(GetReward);
    }


    public virtual void Init(TaskInfoData taskInfo, bool isDaily)
    {
        curTaskInfo = taskInfo;
        taskDesc.text = taskInfo.desc;
        curTaskId = taskInfo.id;
        isDailyTask = isDaily;
        RefreshInfo(taskInfo);
        bool outReward = Enum.TryParse(taskInfo.rewardType, out RewardType rewardType);
        curRewardType = rewardType;
        for (int i = insParent.childCount - 1; i >= 0; i--)
        {
            Transform child = insParent.GetChild(i);
            if (child.GetComponent<Treasure_Content>() != null)
            {
                MainObjectPool.Instance.ReturnToPool("Reward_Equip", child.gameObject);
                continue; // 避免重复检查
            }

            if (child.GetComponent<AwardItem>() != null)
            {
                MainObjectPool.Instance.ReturnToPool("Reward_Item", child.gameObject);
            }
        }
        GetRewardName(rewardType);
        if (rewardType == RewardType.Equip)
        {
            curRewardQuality = taskInfo.rewardNum;
            Treasure_Content item = MainObjectPool.Instance.SpawnFromPool("Reward_Equip", Vector3.zero, Quaternion.identity).GetComponent<Treasure_Content>();
            item.transform.SetParent(insParent);
            item.transform.localPosition = Vector3.zero;
            item.transform.localScale = Vector3.one;
            item.ShowRandom(LoadImageByName(rewardType), curRewardQuality, 1);
        }
        else
        {
            AwardItem item = MainObjectPool.Instance.SpawnFromPool("Reward_Item", Vector3.zero, Quaternion.identity).GetComponent<AwardItem>();
            item.transform.SetParent(insParent);
            item.transform.localPosition = Vector3.zero;
            item.transform.localScale = Vector3.one;
            curRewardNum = taskInfo.rewardNum;
            item.Init(LoadImageByName(rewardType), taskInfo.rewardNum);
        }
    }

    public void RefreshInfo(TaskInfoData taskInfo)
    {
        if ((Mvc.GetModel<GameModel>().DailyTaskProgress[curTaskId].isGetReward && isDailyTask)
            || (Mvc.GetModel<GameModel>().WeeklyTaskProgress[curTaskId].isGetReward && !isDailyTask))
        {
            awardButton.gameObject.SetActive(false);
            progressText.gameObject.SetActive(false);
            alreadyGet.SetActive(true);
        }
        else
        {
            alreadyGet.SetActive(false);
            progressText.gameObject.SetActive(true);
            taskTarget.text = $"({currentProgress}/{taskInfo.target})";
            isFinish = currentProgress >= taskInfo.target;
            progressText.text = isFinish ? "" : "进行中";
            awardButton.gameObject.SetActive(isFinish);
        }

    }

    private void GetReward()
    {
        RefreshRewardState();
        if (curRewardType != RewardType.Equip)
        {
            EventManager.Instance.TriggerEvent(EventName.ShowCommonAward, null, new SetAward()
            {
                awardList = new List<AwardInfo>()
                {
                    new AwardInfo()
                    {
                        awardType = curRewardType,
                        awardNum = curRewardNum
                    },
                }
            });
        }
        else
        {
            curRewardEquip = GetRandomEquip(curRewardQuality);
            EventManager.Instance.TriggerEvent(EventName.ShowCommonAward, null, new SetAward()
            {
                awardList = new List<AwardInfo>()
                {
                    new AwardInfo()
                    {
                        awardType = curRewardType,
                        awardEquip = curRewardEquip
                    },
                }
            });
        }

    }

    public void RefreshRewardState()
    {
        awardButton.gameObject.SetActive(false);
        progressText.gameObject.SetActive(false);
        alreadyGet.SetActive(true);
        if (isDailyTask)
        {
            Mvc.GetModel<GameModel>().gameData.dailyTaskProgress[curTaskId].isGetReward = true;
        }
        else
        {
            Mvc.GetModel<GameModel>().gameData.weeklyTaskProgress[curTaskId].isGetReward = true;
        }
    }


    // 通过图片名称加载图片并赋值
    public Sprite LoadImageByName(RewardType type)
    {
        Sprite loadedSprite = null;
        if (type == RewardType.Equip)
        {
            loadedSprite = SpriteManager.Instance.GetRandomEquipSprite(curRewardQuality);
        }
        else
        {
            loadedSprite = SpriteManager.Instance.GetRewardSprite(curRewardType);
        }

        return loadedSprite;
    }

    private void GetRewardName(RewardType rewardType)
    {
        switch (rewardType)
        {
            case RewardType.Coin:
                rewardName = "金币";
                break;
            case RewardType.Diamond:
                rewardName = "钻石";
                break;
            case RewardType.Stone:
                rewardName = "强化石";
                break;
            case RewardType.Equip:
                rewardName = "装备";
                break;
        }
    }

    public EquipInfoData GetRandomEquip(int quality)
    {
        int result = quality;
        int randomEquip = UnityEngine.Random.Range(0, 6);
        List<EquipInfoData> selectedInfo = DataManager.Instance.equipmentDic[equipTypeDic[randomEquip]];
        List<EquipInfoData> qualityEquipList = selectedInfo.Where(a => a.quality == result).ToList();
        int randomIndex = UnityEngine.Random.Range(0, qualityEquipList.Count);
        EquipInfoData finalEquip = qualityEquipList[randomIndex];
        return finalEquip;
    }
    
}
