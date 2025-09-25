using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipPopup : View
{
    
    public override string Name { get; }
    public override void HandleEvent(object data = null)
    {
        throw new NotImplementedException();
    }
    
    public TextMeshProUGUI equipName;
    public TextMeshProUGUI equipType;
    public TextMeshProUGUI equipProperty;
    public Image equipIcon;
    public Image equipQuality;
    public Image bg;
    public Sprite[] bgSprites;
    public GameObject wearingButtons;
    public GameObject unWearingButtons;

    
    public StrengthPanelController strengthPanel;

    private string curType = "Weapon";
    private EquipInfoData thisEquip;

    public Button strengthButton;
    public Button unWearingButton;
    public Button wearButton;
    public Button breakButton;


    private void Start()
    {
        strengthButton.onClick.AddListener(OpenStrengthPanel);
        unWearingButton.onClick.AddListener(UnwearEquip);
        wearButton.onClick.AddListener(WearEquip);
        breakButton.onClick.AddListener(BreakEquip);
    }

    private void OnDestroy()
    {
        strengthButton.onClick.RemoveListener(OpenStrengthPanel);
        unWearingButton.onClick.RemoveListener(UnwearEquip);
        wearButton.onClick.RemoveListener(WearEquip);
        breakButton.onClick.RemoveListener(BreakEquip);
    }
    
    public void Show(ShowEquip e)
    {
        foreach (var equip in DataManager.Instance.equipmentList)
        {
            if (equip.name == e.equipName)
            {
                thisEquip = equip;
                break;
            }
        }

        if (thisEquip != null)
        {
            equipName.text = thisEquip.name;
            equipType.text = thisEquip.cnType;
            curType = thisEquip.type;
            string percent = thisEquip.isPercent ? "%" : "";
            equipProperty.text = thisEquip.propertyType+"："+thisEquip.value+ percent;
            equipIcon.sprite = SpriteManager.Instance.GetEquipSprite(thisEquip.type, thisEquip.id);
            equipQuality.sprite = SpriteManager.Instance.GetQualitySprite(thisEquip.quality);
        }

        //区分从角色界面和装备界面打开的不同展示
        bg.sprite = e.isOpenFromEquip ? bgSprites[0] : bgSprites[1];

        if (e.isOpenFromEquip)
        {
            wearingButtons.SetActive(e.isWearing);
            unWearingButtons.SetActive(!e.isWearing);

        }
        else
        {
            wearingButtons.SetActive(false);
            unWearingButtons.SetActive(false);
        }
    }

    public void OpenStrengthPanel()
    {
        strengthPanel.thisType = DataManager.Instance.EquipTypeDic[curType];
        strengthPanel.gameObject.SetActive(true);
    }
    
    private void BreakEquip()
    {
        GlobalTaskCounter.Instance.AddDailyCount(DailyTask.BreakEquip);
        GlobalTaskCounter.Instance.AddWeeklyCount(WeeklyTask.BreakEquip);
        bool outEquip = Enum.TryParse(thisEquip.type, out EquipType equipType);
        GetModel<GameModel>().OwnedEquipments[equipType].RemoveAll(e => e.id == thisEquip.id);
        Dictionary<EquipType,List<EquipInfoData>> WornEquipmentsdebug = GetModel<GameModel>().OwnedEquipments;
        EventManager.Instance.TriggerEvent(EventName.ShowCommonAward,null,new SetAward()
        {
            awardList = new List<AwardInfo>()
            {
                new AwardInfo()
                {
                    awardType = RewardType.Stone,
                    awardNum = thisEquip.convertStones,
                }
            }
        });
        EventManager.Instance.TriggerEvent(EventName.RefreshEquip,null,new RefreshEquipArgs()
        {
            refreshType = equipType
        });
    }

    private void WearEquip()
    {
        bool outEquip = Enum.TryParse(thisEquip.type, out EquipType equipType);
        GetModel<GameModel>().WornEquipments[equipType] = thisEquip;
        EventManager.Instance.TriggerEvent(EventName.RefreshEquip,null,new RefreshEquipArgs()
        {
            refreshType = equipType
        });
        
    }

    private void UnwearEquip()
    {
        bool outEquip = Enum.TryParse(thisEquip.type, out EquipType equipType);
        GetModel<GameModel>().WornEquipments[equipType] = null;
        EventManager.Instance.TriggerEvent(EventName.RefreshEquip,null,new RefreshEquipArgs()
        {
            refreshType = equipType
        });
    }


}
