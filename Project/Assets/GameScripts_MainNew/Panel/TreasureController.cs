using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TreasureController : View
{
    public override string Name { get; }
    public override void HandleEvent(object data = null)
    {
        throw new System.NotImplementedException();
    }
    
    private Dictionary<int, string> equipTypeDic = new Dictionary<int, string>()
    {
        {0,"Weapon"},
        {1,"Necklace"},
        {2,"Cloak"},
        {3,"Head"},
        {4,"Armor"},
        {5,"Legs"},
    };
    
    public Button onceButton;
    public Button fiveTimesButton;

    public GameObject treasureContent;
    public Transform getOneParent;
    public Transform[] getFiveParents;
    private List<Treasure_Content> insTreasureList = new List<Treasure_Content>();
    public Button closePanel;
    public GameObject treasurePopup;
    public TextMeshProUGUI totalGet;
    public SkeletonGraphic boxAni;
    public GameObject firstText;
    public Step19 guidanceStep19;

    
    private void Start()
    {
        onceButton.onClick.AddListener(OnOnceClick);   
        fiveTimesButton.onClick.AddListener(OnFiveTimesClick);   
    }

    private void OnEnable()
    {
        firstText.SetActive(GetModel<GameModel>().FirstGetTreasure);
        totalGet.text = "今日已开启"+GetModel<GameModel>().GetTreasureTimes+"/30";
    }

    private void OnDestroy()
    {
        onceButton.onClick.RemoveListener(OnOnceClick);   
        fiveTimesButton.onClick.RemoveListener(OnFiveTimesClick);   
    }

    public void OnOnceClick()
    {
        if (GetModel<GameModel>().FirstGetTreasure)
        {
            StartCoroutine(ShowIE(false));
            GetModel<GameModel>().FirstGetTreasure = false;
            firstText.SetActive(false);
        }
        else
        {
            if (GetModel<GameModel>().Diamond>=DataManager.Instance.onceDiamondCost)
            {
                GetModel<GameModel>().Diamond -= DataManager.Instance.onceDiamondCost;
                StartCoroutine(ShowIE(false));
            }
            else
            {
                EventManager.Instance.TriggerEvent(EventName.ShowCommonTips,null,new ShowCommonTips()
                {
                    tipsContent = "钻石不足！"
                });
            }
        }
    }
    
    public void OnFiveTimesClick()
    {
        if (GetModel<GameModel>().Diamond>=DataManager.Instance.onceDiamondCost*5)
        {
            GetModel<GameModel>().Diamond -= DataManager.Instance.onceDiamondCost * 5;
            StartCoroutine(ShowIE(true));
        }
        else
        {
            EventManager.Instance.TriggerEvent(EventName.ShowCommonTips,null,new ShowCommonTips()
            {
                tipsContent = "钻石不足！"
            });
        }

    }


    public EquipInfoData GetTreasure()
    {
        int randomValue = UnityEngine.Random.Range(1, 101);
        int result;
        if (randomValue <= 70)
        {
            result = 1;
        }
        else if (randomValue <= 90)
        {
            result = 2;
        }
        else
        {
            result = 3;
            GetModel<GameModel>().OrangeTreasureTarget = 0;
        }

        GetModel<GameModel>().OrangeTreasureTarget++;
        if (GetModel<GameModel>().OrangeTreasureTarget>=DataManager.Instance.orangeTarget)
        {
            result = 3;
            GetModel<GameModel>().OrangeTreasureTarget = 0;
        }
        int randomEquip = UnityEngine.Random.Range(0, 6);
        List<EquipInfoData> selectedInfo = DataManager.Instance.equipmentDic[equipTypeDic[randomEquip]];
        List<EquipInfoData> qualityEquipList = selectedInfo.Where(a => a.quality == result).ToList();
        int randomIndex = UnityEngine.Random.Range(0, qualityEquipList.Count);
        EquipInfoData finalEquip = qualityEquipList[randomIndex];
        return finalEquip;
    }

    IEnumerator ShowIE(bool timesGet)
    {
        //检测是否达到每日上限
        EventSystem eventSystem = EventSystem.current;
        int dayLimit = GetModel<GameModel>().GetTreasureTimes;
        if (timesGet)
        {
            if (dayLimit+5>DataManager.Instance.getTreasureDayLimit)
            {
                ShowLimitTips();
                yield break;
            }
        }
        else
        {
            if (dayLimit+1>DataManager.Instance.getTreasureDayLimit)
            {
                ShowLimitTips();
                yield break;
            }
        }
        eventSystem.enabled = false;
        this.TriggerEvent(EventName.PlayAnotherSound, new PlaySoundEventArgs() { index = Sound.Treasure });
        boxAni.AnimationState.SetAnimation(0, "action_1", false);
        yield return new WaitForSeconds(1.8f);
        treasurePopup.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        if (timesGet)
        {
            for (int i = 0; i < 5; i++)
            {
                GlobalTaskCounter.Instance.AddDailyCount(DailyTask.GetTreasure);
                GlobalTaskCounter.Instance.AddWeeklyCount(WeeklyTask.GetTreasure);
                GetModel<GameModel>().GetTreasureTimes++;
                InsTreasure(getFiveParents[i]);
                yield return new WaitForSeconds(0.2f);
            }
        }
        else
        {
            GlobalTaskCounter.Instance.AddDailyCount(DailyTask.GetTreasure);
            GlobalTaskCounter.Instance.AddWeeklyCount(WeeklyTask.GetTreasure);
            GetModel<GameModel>().GetTreasureTimes++;
            InsTreasure(getOneParent);
        }
        totalGet.text = "今日已开启"+GetModel<GameModel>().GetTreasureTimes+"/30";
        yield return new WaitForSeconds(0.5f);
        foreach (var treasure in insTreasureList)
        {
            if (Enum.TryParse(treasure.thisEquip.type, out EquipType equipType))
            {
                if (GetModel<GameModel>().OwnedEquipments[equipType].Any(e => e.id == treasure.thisEquip.id))
                {
                    treasure.PlayConvert(treasure.thisEquip.convertStones);
                    GetModel<GameModel>().StonesCount += treasure.thisEquip.convertStones;
                }
                else
                {
                    treasure.PlayNew();
                    GetModel<GameModel>().OwnedEquipments[equipType].Add(treasure.thisEquip);
                }
            }
        }
        yield return new WaitForSeconds(1);
        eventSystem.enabled = true;
        closePanel.onClick.AddListener(() =>
        {
            foreach (var treasure in insTreasureList)
            {
                MainObjectPool.Instance.ReturnToPool("Treasure_Content",treasure.gameObject);
            }
            insTreasureList.Clear();
            treasurePopup.SetActive(false);
            boxAni.AnimationState.SetAnimation(0, "idle", true);
            if (!GetModel<GameModel>().FinishGuidance)
            {
                guidanceStep19.OnComplete();
            }
        });
    }

    private void InsTreasure(Transform parent)
    {
        Treasure_Content treasure =MainObjectPool.Instance.SpawnFromPool("Treasure_Content", Vector3.zero, Quaternion.identity).GetComponent<Treasure_Content>();
        treasure.transform.SetParent(parent);
        treasure.transform.localPosition = Vector3.zero;
        treasure.transform.localScale = Vector3.one;
        EquipInfoData randomEquip = GetTreasure();
        treasure.InitContent(randomEquip);
        insTreasureList.Add(treasure);
    }

    private void ShowLimitTips()
    {
        EventManager.Instance.TriggerEvent(EventName.ShowCommonTips,null,new ShowCommonTips()
        {
            tipsContent = "不能超过每日抽取上限！"
        });
    }


}
