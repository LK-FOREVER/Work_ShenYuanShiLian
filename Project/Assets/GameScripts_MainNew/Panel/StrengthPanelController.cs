using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StrengthPanelController : View
{
    
    public override string Name { get; }
    public override void HandleEvent(object data = null)
    {
        throw new NotImplementedException();
    }
    
    
    public TextMeshProUGUI curLevelText;
    public TextMeshProUGUI nextLevelText;
    public TextMeshProUGUI curPropertyText;
    public TextMeshProUGUI nextPropertyText;
    public Image curPropertyIcon;
    public Sprite[] propertySprites;
    public TextMeshProUGUI goldCostText;
    public TextMeshProUGUI stoneCostText;
    public Button upgradeButton;
    public Button backButton;
    public TextMeshProUGUI currentHaveStones;
    public GameObject strengthPanel;

    public GameObject[] normalLevelMessage;
    public GameObject[] maxLevelMessage;
    public TextMeshProUGUI maxLevelText;
    public TextMeshProUGUI maxPropertyText;
    public Image maxPropertyIcon;
    
    [HideInInspector]
    public EquipType thisType = EquipType.Weapon;

    private void Start()
    {
        EventManager.Instance.AddListener(EventName.RefreshStrength,RefreshStrength);
        upgradeButton.onClick.AddListener(UpgradeSlot);
        backButton.onClick.AddListener(BackToSelect);
    }

    private void OnEnable()
    {
        Init();
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveListener(EventName.RefreshStrength,RefreshStrength);
        upgradeButton.onClick.RemoveListener(UpgradeSlot);
        backButton.onClick.RemoveListener(BackToSelect);
    }

    public void Init()
    {
        currentHaveStones.text = GetModel<GameModel>().StonesCount.ToString();
        int maxLevel = DataManager.Instance.equipmentLevelDic.Count - 1;
        bool isMaxLevel = GetModel<GameModel>().StrengthEquipLevel[thisType] == maxLevel;
        foreach (var max in maxLevelMessage)
        {
            max.SetActive(isMaxLevel);
        }
        foreach (var normal in normalLevelMessage)
        {
            normal.SetActive(!isMaxLevel);
        }
        int curLevel = 0;
        int nextLevel = 0;
        if (isMaxLevel)
        {
            maxLevelText.text = "等级："+maxLevel;
        }
        else
        {
            curLevel = GetModel<GameModel>().StrengthEquipLevel[thisType];
            nextLevel = GetModel<GameModel>().StrengthEquipLevel[thisType] + 1;
            curLevelText.text ="等级:"+curLevel;
            nextLevelText.text = "等级:"+nextLevel;
        }
        switch (thisType)
        {
            case EquipType.Weapon:
                curPropertyText.text = DataManager.Instance.equipmentLevelDic[curLevel].weaponAttack.ToString();
                nextPropertyText.text = DataManager.Instance.equipmentLevelDic[nextLevel].weaponAttack.ToString();
                maxPropertyText.text = DataManager.Instance.equipmentLevelDic[maxLevel].weaponAttack.ToString();
                curPropertyIcon.sprite = propertySprites[0];
                maxPropertyIcon.sprite = propertySprites[0];
                break;
            case EquipType.Necklace:
                curPropertyText.text = DataManager.Instance.equipmentLevelDic[curLevel].necklaceCritRate +"%";
                nextPropertyText.text = DataManager.Instance.equipmentLevelDic[nextLevel].necklaceCritRate +"%";
                maxPropertyText.text = DataManager.Instance.equipmentLevelDic[maxLevel].necklaceCritRate +"%";
                curPropertyIcon.sprite = propertySprites[1];
                maxPropertyIcon.sprite = propertySprites[1];
                break;
            case EquipType.Cloak:
                curPropertyText.text = DataManager.Instance.equipmentLevelDic[curLevel].cloakDamageBonus +"%";
                nextPropertyText.text = DataManager.Instance.equipmentLevelDic[nextLevel].cloakDamageBonus +"%";
                maxPropertyText.text = DataManager.Instance.equipmentLevelDic[maxLevel].cloakDamageBonus +"%";
                curPropertyIcon.sprite = propertySprites[2];
                maxPropertyIcon.sprite = propertySprites[2];
                break;
            case EquipType.Head:
                curPropertyText.text = DataManager.Instance.equipmentLevelDic[curLevel].headHP.ToString();
                nextPropertyText.text = DataManager.Instance.equipmentLevelDic[nextLevel].headHP.ToString();
                maxPropertyText.text = DataManager.Instance.equipmentLevelDic[maxLevel].headHP.ToString();
                curPropertyIcon.sprite = propertySprites[3];
                maxPropertyIcon.sprite = propertySprites[3];
                break;
            case EquipType.Armor:
                curPropertyText.text = DataManager.Instance.equipmentLevelDic[curLevel].armorDamageReduction +"%";
                nextPropertyText.text = DataManager.Instance.equipmentLevelDic[nextLevel].armorDamageReduction +"%";
                maxPropertyText.text = DataManager.Instance.equipmentLevelDic[maxLevel].armorDamageReduction +"%";
                curPropertyIcon.sprite = propertySprites[4];
                maxPropertyIcon.sprite = propertySprites[4];
                break;
            case EquipType.Legs:
                curPropertyText.text = DataManager.Instance.equipmentLevelDic[curLevel].legsDodgeRate +"%";
                nextPropertyText.text = DataManager.Instance.equipmentLevelDic[nextLevel].legsDodgeRate +"%";
                maxPropertyText.text = DataManager.Instance.equipmentLevelDic[maxLevel].legsDodgeRate +"%";
                curPropertyIcon.sprite = propertySprites[5];
                maxPropertyIcon.sprite = propertySprites[5];
                break;
        }

        if (!isMaxLevel)
        {
            goldCostText.text = DataManager.Instance.equipmentLevelDic[nextLevel].goldCost.ToString();
            stoneCostText.text = DataManager.Instance.equipmentLevelDic[nextLevel].stoneCost.ToString();
        }
    }
    
    private void UpgradeSlot()
    {
        int nextLevel = GetModel<GameModel>().StrengthEquipLevel[thisType] + 1;
        int goldCost = DataManager.Instance.equipmentLevelDic[nextLevel].goldCost;
        int stoneCost = DataManager.Instance.equipmentLevelDic[nextLevel].stoneCost;
        if (GetModel<GameModel>().Coin >= goldCost && GetModel<GameModel>().StonesCount >= stoneCost)
        {
            GetModel<GameModel>().Coin-= goldCost;
            GetModel<GameModel>().StonesCount-= stoneCost;
            GetModel<GameModel>().StrengthEquipLevel[thisType]++;
            Init();
            this.TriggerEvent(EventName.PlayAnotherSound, new PlaySoundEventArgs() { index = Sound.Strength });
            EventManager.Instance.TriggerEvent(EventName.ShowCommonTips,null,new ShowCommonTips()
            {
                tipsContent = "强化成功！"
            });
        }
        else
        {
            EventManager.Instance.TriggerEvent(EventName.ShowCommonTips,null,new ShowCommonTips()
            {
                tipsContent = "材料不足，强化失败！"
            });
        }
     
    }
    
    private void BackToSelect()
    {
        strengthPanel.SetActive(false);
        EventManager.Instance.TriggerEvent(EventName.RefreshSlot);
    }
    
    private void RefreshStrength(object sender, EventArgs e)
    {
        RefreshStrengthArgs args = e as RefreshStrengthArgs;
        thisType = args.refreshType;
        Init();
    }
    


}
