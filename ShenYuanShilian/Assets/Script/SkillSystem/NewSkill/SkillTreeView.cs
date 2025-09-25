using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SkillTreeView : View
{
    public override string Name
    {
        get
        {
            return Consts.V_SkillTreeView;
        }
    }
    [SerializeField]
    private List<Sprite> atkSprite; //攻击属性图标
    [SerializeField]
    private List<Sprite> critSprite; //暴击属性图标
    [SerializeField]
    private List<Sprite> addDamageSprite; //增伤属性图标
    [SerializeField]
    private List<Sprite> skill_1_Sprite; //技能1-魔力膨胀图标
    [SerializeField]
    private List<Sprite> skill_2_Sprite; //技能2-全力一击图标
    [SerializeField]
    private List<Sprite> hpSprite; //生命属性图标
    [SerializeField]
    private List<Sprite> dodgeSprite; //闪避属性图标
    [SerializeField]
    private List<Sprite> damageReducSprite; //伤害减免属性图标
    [SerializeField]
    private List<Sprite> skill_3_Sprite; //技能3-荆棘之甲图标
    [SerializeField]
    private List<Sprite> skill_4_Sprite; //技能4-魔法力场图标
    [SerializeField]
    private List<Sprite> leftUpLine; //左上线
    [SerializeField]
    private List<Sprite> leftDownLine; //左下线
    [SerializeField]
    private List<Sprite> rightUpLine; //右上线
    [SerializeField]
    private List<Sprite> rightDownLine; //右下线
    [SerializeField]
    private List<Sprite> middleLine; //中间线


    [SerializeField]
    private GameObject[] atkHpObj;//攻击和生命属性点对象，2个元素
    [SerializeField]
    private GameObject[] critObj;//从下到上暴击属性点对象，4个元素
    [SerializeField]
    private GameObject[] addDamageObj;//从下到上增伤属性点对象，4个元素
    [SerializeField]
    private GameObject[] dodgeObj;//从下到上闪避属性点对象，4个元素
    [SerializeField]
    private GameObject[] damageReducObj;//从下到上伤害减免属性点对象，4个元素
    [SerializeField]
    private GameObject[] skillObj;//技能对象，4个元素
    [SerializeField]
    private GameObject[] critBranchLineObj;//从下到上暴击分支线，6个元素
    [SerializeField]
    private GameObject[] addDamageBranchLineObj;//从下到上增伤分支线，6个元素
    [SerializeField]
    private GameObject[] dodgeBranchLineObj;//从下到上闪避分支线，6个元素
    [SerializeField]
    private GameObject[] damageReducBranchLineObj;//从下到上伤害减免分支线，6个元素


    [SerializeField]
    private TextMeshProUGUI levelText; //等级
    [SerializeField]
    private TextMeshProUGUI skillPointText; //技能点

    [SerializeField]
    private GameObject propertyUpgradePanel; //属性升级面板
    [SerializeField]
    private GameObject skillUnlockPanel;//技能解锁面板

    protected void Awake()
    {
        EventManager.Instance.AddListener(EventName.UpdateSkillTree, InitSkillTree);
        Init();
    }
    private void OnDestroy()
    {
        EventManager.Instance.RemoveListener(EventName.UpdateSkillTree, InitSkillTree);
    }
    private void Init()
    {
        int curID = GetModel<GameModel>().CurId;
        levelText.text = $"等级：{GetModel<GameModel>().PlayerLevel[curID]}";
        skillPointText.text = $"技能点：{GetModel<GameModel>().SkillPoint}";

        InitAtkHp();
        InitCrit();
        InitAddDamage();
        InitDodge();
        InitDamageReduc();
        InitSkill();
        InitCritBranchLine();
        InitAddDamageBranchLine();
        InitDodgeBranchLine();
        InitDamageReducBranchLine();
    }

    private void InitAtkHp()
    {
        atkHpObj[0].GetComponent<Image>().sprite = GetModel<GameModel>().AtkProLevel > 0 ? atkSprite[1] : atkSprite[0];
        atkHpObj[0].transform.Find("textBg").Find("name").GetComponent<Text>().text = $"攻击（{GetModel<GameModel>().AtkProLevel}/5）";
        atkHpObj[1].GetComponent<Image>().sprite = GetModel<GameModel>().HpProLevel > 0 ? hpSprite[1] : hpSprite[0];
        atkHpObj[1].transform.Find("textBg").Find("name").GetComponent<Text>().text = $"生命（{GetModel<GameModel>().HpProLevel}/5）";
    }
    private void InitCrit()
    {
        for (int i = 0; i < critObj.Length; i++)
        {
            critObj[i].GetComponent<Image>().sprite = GetModel<GameModel>().CritProLevel[i] > 0 ? critSprite[1] : critSprite[0];
            critObj[i].transform.Find("textBg").Find("name").GetComponent<Text>().text = $"暴击（{GetModel<GameModel>().CritProLevel[i]}/2）";
        }
    }
    private void InitAddDamage()
    {
        for (int i = 0; i < addDamageObj.Length; i++)
        {
            addDamageObj[i].GetComponent<Image>().sprite = GetModel<GameModel>().AddDamageProLevel[i] > 0 ? addDamageSprite[1] : addDamageSprite[0];
            addDamageObj[i].transform.Find("textBg").Find("name").GetComponent<Text>().text = $"增伤（{GetModel<GameModel>().AddDamageProLevel[i]}/2）";
        }
    }
    private void InitDodge()
    {
        for (int i = 0; i < dodgeObj.Length; i++)
        {
            dodgeObj[i].GetComponent<Image>().sprite = GetModel<GameModel>().DodgeProLevel[i] > 0 ? dodgeSprite[1] : dodgeSprite[0];
            dodgeObj[i].transform.Find("textBg").Find("name").GetComponent<Text>().text = $"闪避（{GetModel<GameModel>().DodgeProLevel[i]}/2）";
        }
    }
    private void InitDamageReduc()
    {
        for (int i = 0; i < damageReducObj.Length; i++)
        {
            damageReducObj[i].GetComponent<Image>().sprite = GetModel<GameModel>().DamageReducProLevel[i] > 0 ? damageReducSprite[1] : damageReducSprite[0];
            damageReducObj[i].transform.Find("textBg").Find("name").GetComponent<Text>().text = $"减伤（{GetModel<GameModel>().DamageReducProLevel[i]}/2）";
        }
    }
    private void InitSkill()
    {
        skillObj[0].GetComponent<Image>().sprite = GetModel<GameModel>().Skill1Level > 0 ? skill_1_Sprite[1] : skill_1_Sprite[0];
        skillObj[1].GetComponent<Image>().sprite = GetModel<GameModel>().Skill2Level > 0 ? skill_2_Sprite[1] : skill_2_Sprite[0];
        skillObj[2].GetComponent<Image>().sprite = GetModel<GameModel>().Skill3Level > 0 ? skill_3_Sprite[1] : skill_3_Sprite[0];
        skillObj[3].GetComponent<Image>().sprite = GetModel<GameModel>().Skill4Level > 0 ? skill_4_Sprite[1] : skill_4_Sprite[0];
    }
    private void InitCritBranchLine()
    {
        critBranchLineObj[0].GetComponent<Image>().sprite = GetModel<GameModel>().CritBranchLine[0] ? leftDownLine[1] : leftDownLine[0];
        critBranchLineObj[1].GetComponent<Image>().sprite = GetModel<GameModel>().CritBranchLine[1] ? middleLine[1] : middleLine[0];
        critBranchLineObj[2].GetComponent<Image>().sprite = GetModel<GameModel>().CritBranchLine[2] ? leftUpLine[1] : leftUpLine[0];
        critBranchLineObj[3].GetComponent<Image>().sprite = GetModel<GameModel>().CritBranchLine[3] ? leftDownLine[1] : leftDownLine[0];
        critBranchLineObj[4].GetComponent<Image>().sprite = GetModel<GameModel>().CritBranchLine[4] ? middleLine[1] : middleLine[0];
        critBranchLineObj[5].GetComponent<Image>().sprite = GetModel<GameModel>().CritBranchLine[5] ? leftUpLine[1] : leftUpLine[0];
    }
    private void InitAddDamageBranchLine()
    {
        addDamageBranchLineObj[0].GetComponent<Image>().sprite = GetModel<GameModel>().AddDamageBranchLine[0] ? rightDownLine[1] : rightDownLine[0];
        addDamageBranchLineObj[1].GetComponent<Image>().sprite = GetModel<GameModel>().AddDamageBranchLine[1] ? middleLine[1] : middleLine[0];
        addDamageBranchLineObj[2].GetComponent<Image>().sprite = GetModel<GameModel>().AddDamageBranchLine[2] ? rightUpLine[1] : rightUpLine[0];
        addDamageBranchLineObj[3].GetComponent<Image>().sprite = GetModel<GameModel>().AddDamageBranchLine[3] ? rightDownLine[1] : rightDownLine[0];
        addDamageBranchLineObj[4].GetComponent<Image>().sprite = GetModel<GameModel>().AddDamageBranchLine[4] ? middleLine[1] : middleLine[0];
        addDamageBranchLineObj[5].GetComponent<Image>().sprite = GetModel<GameModel>().AddDamageBranchLine[5] ? rightUpLine[1] : rightUpLine[0];
    }
    private void InitDodgeBranchLine()
    {
        dodgeBranchLineObj[0].GetComponent<Image>().sprite = GetModel<GameModel>().DodgeBranchLine[0] ? leftDownLine[1] : leftDownLine[0];
        dodgeBranchLineObj[1].GetComponent<Image>().sprite = GetModel<GameModel>().DodgeBranchLine[1] ? middleLine[1] : middleLine[0];
        dodgeBranchLineObj[2].GetComponent<Image>().sprite = GetModel<GameModel>().DodgeBranchLine[2] ? leftUpLine[1] : leftUpLine[0];
        dodgeBranchLineObj[3].GetComponent<Image>().sprite = GetModel<GameModel>().DodgeBranchLine[3] ? leftDownLine[1] : leftDownLine[0];
        dodgeBranchLineObj[4].GetComponent<Image>().sprite = GetModel<GameModel>().DodgeBranchLine[4] ? middleLine[1] : middleLine[0];
        dodgeBranchLineObj[5].GetComponent<Image>().sprite = GetModel<GameModel>().DodgeBranchLine[5] ? leftUpLine[1] : leftUpLine[0];
    }
    private void InitDamageReducBranchLine()
    {
        damageReducBranchLineObj[0].GetComponent<Image>().sprite = GetModel<GameModel>().DamageReducBranchLine[0] ? rightDownLine[1] : rightDownLine[0];
        damageReducBranchLineObj[1].GetComponent<Image>().sprite = GetModel<GameModel>().DamageReducBranchLine[1] ? middleLine[1] : middleLine[0];
        damageReducBranchLineObj[2].GetComponent<Image>().sprite = GetModel<GameModel>().DamageReducBranchLine[2] ? rightUpLine[1] : rightUpLine[0];
        damageReducBranchLineObj[3].GetComponent<Image>().sprite = GetModel<GameModel>().DamageReducBranchLine[3] ? rightDownLine[1] : rightDownLine[0];
        damageReducBranchLineObj[4].GetComponent<Image>().sprite = GetModel<GameModel>().DamageReducBranchLine[4] ? middleLine[1] : middleLine[0];
        damageReducBranchLineObj[5].GetComponent<Image>().sprite = GetModel<GameModel>().DamageReducBranchLine[5] ? rightUpLine[1] : rightUpLine[0];
    }

    #region 按钮
    public void OnClickAtkHpPoint(int index)
    {
        if (index == 0)
        {
            // 攻击属性
            InitSkillUpgradePanel(SkillPointType.Atk, index);
        }
        else
        {
            // 生命属性
            InitSkillUpgradePanel(SkillPointType.Hp, index);
        }
    }

    // 暴击属性
    public void OnClickCritPoint(int index)
    {
        InitSkillUpgradePanel(SkillPointType.Crit, index);
    }

    // 增伤属性
    public void OnClickAddDamagePoint(int index)
    {
        InitSkillUpgradePanel(SkillPointType.AddDamage, index);
    }

    // 闪避属性
    public void OnClickDodgePoint(int index)
    {
        InitSkillUpgradePanel(SkillPointType.Dodge, index);
    }

    // 减伤属性
    public void OnClickReduceDamagePoint(int index)
    {
        InitSkillUpgradePanel(SkillPointType.DamageReduction, index);
    }

    // 技能
    public void OnClickSkillPoint(int index)
    {
        switch (index)
        {
            case 0:
                InitSkillUpgradePanel(SkillPointType.Skill_1, index);
                break;
            case 1:
                InitSkillUpgradePanel(SkillPointType.Skill_2, index);
                break;
            case 2:
                InitSkillUpgradePanel(SkillPointType.Skill_3, index);
                break;
            case 3:
                InitSkillUpgradePanel(SkillPointType.Skill_4, index);
                break;
            default:
                break;
        }
    }

    private void InitSkillTree(object sender, EventArgs e)
    {
        Init();
    }
    private void InitSkillUpgradePanel(SkillPointType skillPointType, int index)
    {
        if (skillPointType == SkillPointType.Atk || skillPointType == SkillPointType.Hp ||
        skillPointType == SkillPointType.Crit || skillPointType == SkillPointType.AddDamage ||
        skillPointType == SkillPointType.Dodge || skillPointType == SkillPointType.DamageReduction)
        {
            propertyUpgradePanel.SetActive(true);
            propertyUpgradePanel.GetComponent<SkillUpgradePanel>().Init(skillPointType, index);
        }
        else
        {
            skillUnlockPanel.SetActive(true);
            skillUnlockPanel.GetComponent<SkillUnlockPanel>().Init(skillPointType, index);
        }
    }
    #endregion
    public override void HandleEvent(object data = null)
    {
    }
}
