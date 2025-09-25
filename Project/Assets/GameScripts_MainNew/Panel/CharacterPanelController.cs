using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanelController : View
{
    public override string Name { get; }
    public override void HandleEvent(object data = null)
    {
        throw new NotImplementedException();
    }

    public Equip_Slot[] equips;

    public TextMeshProUGUI levelText;

    public Image expImage;
    public TextMeshProUGUI expText;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI attractText;
    public TextMeshProUGUI critText;
    public TextMeshProUGUI dodgeText;

    public GameObject[] lockCharacters;
    public UnlockPopup unlockPopup;
    public CommonButtonController battleButtons;
    public Button[] characterButtons;

    private void OnEnable()
    {
        Init();
        EventManager.Instance.AddListener(EventName.RefreshCharacters, RefreshCharacters);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveListener(EventName.RefreshCharacters, RefreshCharacters);
    }

    private void Init()
    {
        //如果武器有变动，则更新武器数据，若本来此部位没有武器，初始化生成武器
        foreach (var equip in equips)
        {
            int strengthLevel = GetModel<GameModel>().StrengthEquipLevel[equip.thisType];
            equip.strengthLevel = strengthLevel;
            equip.ChangeLevel();
            if (equip.insParent.childCount > 0)
            {
                if (GetModel<GameModel>().WornEquipments[equip.thisType] != null)
                {
                    if (GetModel<GameModel>().WornEquipments[equip.thisType].id != equip.id)
                    {
                        equip.isSlotIns = true;
                        equip.Init(GetModel<GameModel>().WornEquipments[equip.thisType], false);
                    }
                }
                else
                {
                    equip.NoEquipOnWear();
                }
            }
            else
            {
                if (GetModel<GameModel>().WornEquipments[equip.thisType] != null)
                {
                    if (GetModel<GameModel>().WornEquipments[equip.thisType] != null)
                    {
                        equip.isSlotIns = true;
                        equip.Init(GetModel<GameModel>().WornEquipments[equip.thisType], false);
                    }
                }
                else
                {
                    equip.NoEquipOnWear();
                }

            }
        }



        //展示属性
        int curID = GetModel<GameModel>().CurId;
        ChangeBattleCharacter(curID);
        int exp = GetModel<GameModel>().Exp;
        if (GetModel<GameModel>().PlayerLevel[curID] == DataManager.Instance.expUpgradeInfoDic[curID].Count)
        {
            expText.text = "满级";
            expImage.fillAmount = 1;
        }
        else
        {
            int nextLevelExp = DataManager.Instance.expUpgradeInfoDic[curID][GetModel<GameModel>().PlayerLevel[curID]].expCost;
            expText.text = exp + "/" + nextLevelExp;
            expImage.fillAmount = exp / nextLevelExp;
        }

        levelText.text = "等级:" + GetModel<GameModel>().PlayerLevel[curID];
        hpText.text = GetModel<GameModel>().TotalProperty.hp.ToString();
        attractText.text = GetModel<GameModel>().TotalProperty.atk.ToString();
        critText.text = (int)(GetModel<GameModel>().TotalProperty.critRate * 100) + "%";
        dodgeText.text = (int)(GetModel<GameModel>().TotalProperty.dodgeRate * 100) + "%";

        RefreshCharacter();
    }


    private void RefreshCharacters(object sender, EventArgs e)
    {
        RefreshCharacter();
    }
    public void RefreshCharacter()
    {
        if (GetModel<GameModel>().VIP.IndexOf(GetModel<GameModel>().Account) != -1 && GetModel<GameModel>().PlayerLevel[GetModel<GameModel>().CurId] < 120)
        {
            GetModel<GameModel>().UnlockCharacter[0] = 0;
        }
        else if (GetModel<GameModel>().VIP.IndexOf(GetModel<GameModel>().Account) != -1 && GetModel<GameModel>().PlayerLevel[GetModel<GameModel>().CurId] >= 120)
        {
            GetModel<GameModel>().UnlockCharacter[0] = 1;
        }
        else
        {
            GetModel<GameModel>().UnlockCharacter[0] = GetModel<GameModel>().PlayerLevel[GetModel<GameModel>().CurId] >= 30 ? 1 : 0;
        }
        GetModel<GameModel>().UnlockCharacter[1] = GetModel<GameModel>().PlayerLevel[GetModel<GameModel>().CurId] >= 120 ? 1 : 0;
        for (int i = 0; i < lockCharacters.Length; i++)
        {
            lockCharacters[i].SetActive(GetModel<GameModel>().UnlockCharacter[i] == 0);
            characterButtons[i].interactable = GetModel<GameModel>().UnlockCharacter[i] == 1;
        }
    }

    public void UnlockCharacters(int characterIndex)
    {
        if (GetModel<GameModel>().UnlockCharacter[characterIndex] == 0)
        {
            if (characterIndex == 0)
            {
                EventManager.Instance.TriggerEvent(EventName.ShowCommonTips, null, new ShowCommonTips()
                {
                    tipsContent = "等级到达30级时可以解锁此角色。"
                });
            }
            else
            {
                EventManager.Instance.TriggerEvent(EventName.ShowCommonTips, null, new ShowCommonTips()
                {
                    tipsContent = "等级到达120级时可以解锁此角色。"
                });
            }
        }
    }

    public void ChangeBattleCharacter(int id)
    {
        GetModel<GameModel>().CurId = id;
        for (int i = 0; i < battleButtons.buttons.Length; i++)
        {
            battleButtons.buttons[i].GetComponent<Button>().interactable = i != id;
            battleButtons.buttons[i].GetComponent<CommonButton>().ChangeState(i == id);
        }
    }




}
