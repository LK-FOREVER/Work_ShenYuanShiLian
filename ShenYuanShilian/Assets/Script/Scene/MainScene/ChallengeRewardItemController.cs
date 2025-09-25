using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;
public class ChallengeRewardItemController : MonoBehaviour
{
    ChallengeRewardInfo rewardInfo;
    public Text taskName;
    public Button awardButton;
    public Text goingTxt;
    public GameObject awardItemPrefab; // 普通奖励预制体（金币、钻石等）
    public GameObject awardEquipPrefab; // 装备奖励预制体
    public Transform insParent; // 奖励父容器
    private List<ChallengeReward> curRewards; // 当前任务的奖励列表
    public int curRewardQuality;
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
    public void Init(ChallengeRewardInfo rewardInfo)
    {
        this.rewardInfo = rewardInfo;
        curRewards = rewardInfo.reward;
        InitUI();
        // 清空旧奖励UI
        for (int i = 0; i < insParent.childCount; i++)
        {
            Destroy(insParent.GetChild(i).gameObject);
        }

        // 动态生成奖励UI
        foreach (var reward in curRewards)
        {
            if (Enum.TryParse(reward.rewardType, out RewardType rewardType))
            {
                if (rewardType == RewardType.Equip)
                {
                    // 装备奖励
                    curRewardQuality = reward.quality;
                    var item = Instantiate(awardEquipPrefab, insParent).GetComponent<Treasure_Content>();
                    item.ShowRandom(LoadImageByName(rewardType), curRewardQuality, reward.rewardNum);
                }
                else
                {
                    // 普通奖励（金币、钻石等）
                    var item = Instantiate(awardItemPrefab, insParent).GetComponent<AwardItem>();
                    item.Init(LoadImageByName(rewardType), reward.rewardNum);
                }
            }
        }
    }
    public void InitUI()
    {
        taskName.text = rewardInfo.desc;
        if (Mvc.GetModel<GameModel>().ChallengeRewardGet[rewardInfo.id - 1])
        {
            awardButton.gameObject.SetActive(false);
            goingTxt.gameObject.SetActive(true);
            goingTxt.text = "已领取";
        }
        else
        {
            if (Mvc.GetModel<GameModel>().LeaderChallengeDamage >= rewardInfo.target)
            {
                awardButton.gameObject.SetActive(true);
                goingTxt.gameObject.SetActive(false);
            }
            else
            {
                awardButton.gameObject.SetActive(false);
                goingTxt.gameObject.SetActive(true);
                goingTxt.text = "进行中";
            }
        }
    }

    private void GetReward()
    {
        // 构建奖励列表
        var awardList = new List<AwardInfo>();
        foreach (var reward in curRewards)
        {
            if (Enum.TryParse(reward.rewardType, out RewardType rewardType))
            {
                if (rewardType == RewardType.Equip)
                {
                    for (int i = 0; i < reward.rewardNum; i++)
                    {
                        var equip = GetRandomEquip(reward.quality);
                        awardList.Add(new AwardInfo
                        {
                            awardType = rewardType,
                            awardEquip = equip
                        });
                    }
                }
                else
                {
                    awardList.Add(new AwardInfo
                    {
                        awardType = rewardType,
                        awardNum = reward.rewardNum
                    });
                }
            }
        }

        // 触发奖励事件
        EventManager.Instance.TriggerEvent(EventName.ShowCommonAward, null, new SetAward
        {
            awardList = awardList
        });
        Mvc.GetModel<GameModel>().ChallengeRewardGet[rewardInfo.id - 1] = true;
        InitUI();
    }
    public Sprite LoadImageByName(RewardType type)
    {
        if (type == RewardType.Equip)
        {
            return SpriteManager.Instance.GetRandomEquipSprite(curRewardQuality);
        }
        else
        {
            return SpriteManager.Instance.GetRewardSprite(type);
        }
    }

    private string GetRewardName(RewardType rewardType)
    {
        switch (rewardType)
        {
            case RewardType.Coin: return "金币";
            case RewardType.Diamond: return "钻石";
            case RewardType.Stone: return "强化石";
            case RewardType.Equip: return "装备";
            default: return "";
        }
    }

    public EquipInfoData GetRandomEquip(int quality)
    {
        int randomEquip = UnityEngine.Random.Range(0, 6);
        List<EquipInfoData> selectedInfo = DataManager.Instance.equipmentDic[equipTypeDic[randomEquip]];
        List<EquipInfoData> qualityEquipList = selectedInfo.Where(a => a.quality == quality).ToList();
        int randomIndex = UnityEngine.Random.Range(0, qualityEquipList.Count);
        return qualityEquipList[randomIndex];
    }
}