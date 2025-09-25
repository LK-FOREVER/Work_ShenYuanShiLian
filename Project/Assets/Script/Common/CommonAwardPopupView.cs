using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommonAwardPopupView : View
{
    public override string Name
    {
        get
        {
            return Consts.CommonAwardPopupView;
        }
    }
    public override void HandleEvent(object data = null)
    {
        
    }
    [HideInInspector]
    public RewardType rewardType;
    [HideInInspector]
    public int rewardNum;
    [HideInInspector]
    public EquipInfoData rewardEquip;
    [HideInInspector]
    public int skillPieceNumber;
    
    private string rewardName;

    public GameObject rewardItem;
    public GameObject equipItem;
    public Transform rewardParent;

    private Treasure_Content cacheEquip;
    
    public void ShowAward(SetAward awardEvent)
    {
        for (int i = rewardParent.childCount - 1; i >= 0; i--)
        {
            Transform child = rewardParent.GetChild(i);
            if (child.gameObject.GetComponent<Treasure_Content>() != null)
            {
                MainObjectPool.Instance.ReturnToPool("Reward_Equip", rewardParent.GetChild(i).gameObject);
            }

            if (child.gameObject.GetComponent<AwardItem>() != null)
            {
                MainObjectPool.Instance.ReturnToPool("Reward_Item", rewardParent.GetChild(i).gameObject);
            }
        }

        this.TriggerEvent(EventName.PlayAnotherSound, new PlaySoundEventArgs() { index = Sound.GetReward });
        for (int i = 0; i < awardEvent.awardList.Count; i++)
        {
            rewardType = awardEvent.awardList[i].awardType;
            if (rewardType!=RewardType.None)
            {
                if (rewardType == RewardType.Equip)
                {
                    rewardEquip = awardEvent.awardList[i].awardEquip;
                    Treasure_Content item = MainObjectPool.Instance.SpawnFromPool("Reward_Equip", Vector3.zero, Quaternion.identity).GetComponent<Treasure_Content>();
                    item.transform.SetParent(rewardParent);
                    item.transform.localPosition = Vector3.zero;
                    item.transform.localScale = Vector3.one;
                    item.InitContent(rewardEquip);
                    cacheEquip = item;
                }
                else
                {
                    rewardNum = awardEvent.awardList[i].awardNum;
                    if (rewardNum == 0)
                    {
                        continue;
                    }
                    GetRewardName();
                    AwardItem item = MainObjectPool.Instance.SpawnFromPool("Reward_Item", Vector3.zero, Quaternion.identity).GetComponent<AwardItem>();
                    item.transform.SetParent(rewardParent);
                    item.transform.localPosition = Vector3.zero;
                    item.transform.localScale = Vector3.one;
                    item.Init( LoadImageByName(rewardType),rewardNum);
                }
                GetReward(rewardType);
            }
        }
    }
    
    // 通过图片名称加载图片并赋值
    public Sprite LoadImageByName(RewardType type)
    {
        Sprite loadedSprite = SpriteManager.Instance.GetRewardSprite(type);
        return loadedSprite;
    }

    private void GetReward(RewardType type)
    {
        switch (type)
        {
            case RewardType.Coin:
                int coinCurrent = GetModel<GameModel>().Coin;
                int coin = coinCurrent + rewardNum;
                SendViewEvent(Consts.E_ChangeCoin, new ChangeCoin() { coin = coin });
                break;
            
            case RewardType.Diamond:
                GetModel<GameModel>().Diamond += rewardNum;
                break;
            
            case RewardType.Stone:
                GetModel<GameModel>().StonesCount += rewardNum;
                break;
            
            case RewardType.Equip:
                var ownedEquipments = GetModel<GameModel>().OwnedEquipments;
                bool outEquip = Enum.TryParse(rewardEquip.type, out EquipType equipType);
                if (ownedEquipments[equipType].Any(e => e.id == rewardEquip.id))
                {
                    cacheEquip.PlayConvert(rewardEquip.convertStones);
                    GetModel<GameModel>().StonesCount += rewardEquip.convertStones;
                }
                else
                {
                    GetModel<GameModel>().OwnedEquipments[equipType].Add(rewardEquip);
                }
                break;
        }
    }

    private void GetRewardName()
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
            case RewardType.Exp:
                rewardName = "经验";
                break;
        }
    }


}
