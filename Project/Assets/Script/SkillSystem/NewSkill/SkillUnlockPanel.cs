using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUnlockPanel : View
{
    public override string Name
    {
        get
        {
            return Consts.V_SkillUnlockPanel;
        }
    }

    [SerializeField]
    private List<Sprite> skillIcon; //技能图标
    [SerializeField]
    private List<Sprite> unlockIcon; //技能解锁 按钮 状态图标
    [SerializeField]
    private Text skillUpgradeNameText; //技能名称
    [SerializeField]
    private Image icon; //技能图标对象
    [SerializeField]
    private Text skillDescText; //技能描述
    [SerializeField]
    private Text skillPointTxt;//需要消耗的技能点数
    [SerializeField]
    private Button skillUnlockBtn; //技能解锁按钮
    [SerializeField]
    private Button CloseBtn; //关闭按钮
    private SkillPointType skillPointType;
    private int index;
    private void Start()
    {
        skillUnlockBtn.onClick.AddListener(OnSkillUnlockBtnClick);
        CloseBtn.onClick.AddListener(OnCloseBtnClick);
    }

    public void Init(SkillPointType skillPointType, int index)
    {
        this.skillPointType = skillPointType;
        this.index = index;
        InitUI();
    }

    private void InitUI()
    {
        icon.sprite = skillIcon[index];
        skillPointTxt.gameObject.SetActive(true);
        skillPointTxt.text = "需要消耗的技能点数：2";
        skillUnlockBtn.gameObject.transform.Find("Text").GetComponent<Text>().text = "解锁";
        skillUnlockBtn.interactable = true;

        if (skillPointType == SkillPointType.Skill_1)
        {
            skillUpgradeNameText.text = "魔力膨胀";
            skillUnlockBtn.gameObject.GetComponent<Image>().sprite = GetModel<GameModel>().SkillPoint >= 2 && GetModel<GameModel>().Skill1Level == 0 ? unlockIcon[1] : unlockIcon[0];
            if (GetModel<GameModel>().Skill1Level == 1)
            {
                skillUnlockBtn.gameObject.GetComponent<Image>().sprite = unlockIcon[0];
                skillPointTxt.gameObject.SetActive(false);
                skillUnlockBtn.gameObject.transform.Find("Text").GetComponent<Text>().text = "已解锁";
            }
            if (skillUnlockBtn.gameObject.GetComponent<Image>().sprite == unlockIcon[0])
            {
                skillUnlockBtn.interactable = false;
            }
            skillDescText.text = DataManager.Instance.skillInfoDataDic["Common"][3].description;
        }
        else if (skillPointType == SkillPointType.Skill_2)
        {
            skillUpgradeNameText.text = "全力一击";
            skillUnlockBtn.gameObject.GetComponent<Image>().sprite = GetModel<GameModel>().SkillPoint >= 2 && GetModel<GameModel>().Skill2Level == 0 ? unlockIcon[1] : unlockIcon[0];
            if (GetModel<GameModel>().Skill2Level == 1)
            {
                skillUnlockBtn.gameObject.GetComponent<Image>().sprite = unlockIcon[0];
                skillPointTxt.gameObject.SetActive(false);
                skillUnlockBtn.gameObject.transform.Find("Text").GetComponent<Text>().text = "已解锁";
            }
            if (skillUnlockBtn.gameObject.GetComponent<Image>().sprite == unlockIcon[0])
            {
                skillUnlockBtn.interactable = false;
            }
            skillDescText.text = DataManager.Instance.skillInfoDataDic["Common"][4].description;
        }
        else if (skillPointType == SkillPointType.Skill_3)
        {
            skillUpgradeNameText.text = "荆棘之甲";
            skillUnlockBtn.gameObject.GetComponent<Image>().sprite = GetModel<GameModel>().SkillPoint >= 2 && GetModel<GameModel>().Skill3Level == 0 ? unlockIcon[1] : unlockIcon[0];
            if (GetModel<GameModel>().Skill3Level == 1)
            {
                skillUnlockBtn.gameObject.GetComponent<Image>().sprite = unlockIcon[0];
                skillPointTxt.gameObject.SetActive(false);
                skillUnlockBtn.gameObject.transform.Find("Text").GetComponent<Text>().text = "已解锁";
            }
            if (skillUnlockBtn.gameObject.GetComponent<Image>().sprite == unlockIcon[0])
            {
                skillUnlockBtn.interactable = false;
            }
            skillDescText.text = DataManager.Instance.skillInfoDataDic["Common"][1].description;
        }
        else if (skillPointType == SkillPointType.Skill_4)
        {
            skillUpgradeNameText.text = "魔法力场";
            skillUnlockBtn.gameObject.GetComponent<Image>().sprite = GetModel<GameModel>().SkillPoint >= 2 && GetModel<GameModel>().Skill4Level == 0 ? unlockIcon[1] : unlockIcon[0];
            if (GetModel<GameModel>().Skill4Level == 1)
            {
                skillUnlockBtn.gameObject.GetComponent<Image>().sprite = unlockIcon[0];
                skillPointTxt.gameObject.SetActive(false);
                skillUnlockBtn.gameObject.transform.Find("Text").GetComponent<Text>().text = "已解锁";
            }
            if (skillUnlockBtn.gameObject.GetComponent<Image>().sprite == unlockIcon[0])
            {
                skillUnlockBtn.interactable = false;
            }
            skillDescText.text = DataManager.Instance.skillInfoDataDic["Common"][2].description;
        }
    }

    private void OnSkillUnlockBtnClick()
    {
        if (skillPointType == SkillPointType.Skill_1)
        {
            if (GetModel<GameModel>().CritProLevel[1] == 0 && GetModel<GameModel>().AddDamageProLevel[1] == 0) return;
            GetModel<GameModel>().Skill1Level = 1;
            GetModel<GameModel>().SkillPoint -= 2;
            GetModel<GameModel>().CritBranchLine[3] = true;
            GetModel<GameModel>().AddDamageBranchLine[3] = true;
        }
        else if (skillPointType == SkillPointType.Skill_2)
        {
            if (GetModel<GameModel>().CritProLevel[3] == 0 && GetModel<GameModel>().AddDamageProLevel[3] == 0) return;
            GetModel<GameModel>().Skill2Level = 1;
            GetModel<GameModel>().SkillPoint -= 2;
        }
        else if (skillPointType == SkillPointType.Skill_3)
        {
            if (GetModel<GameModel>().DodgeProLevel[1] == 0 && GetModel<GameModel>().DamageReducProLevel[1] == 0) return;
            GetModel<GameModel>().Skill3Level = 1;
            GetModel<GameModel>().SkillPoint -= 2;
            GetModel<GameModel>().DodgeBranchLine[3] = true;
            GetModel<GameModel>().DamageReducBranchLine[3] = true;
        }
        else if (skillPointType == SkillPointType.Skill_4)
        {
            if (GetModel<GameModel>().DodgeProLevel[3] == 0 && GetModel<GameModel>().DamageReducProLevel[3] == 0) return;
            GetModel<GameModel>().Skill4Level = 1;
            GetModel<GameModel>().SkillPoint -= 2;
        }
        this.TriggerEvent(EventName.UpdateSkillTree);
        InitUI();
    }

    private void OnCloseBtnClick()
    {
        gameObject.SetActive(false);
    }

    public override void HandleEvent(object data = null)
    {

    }
}
