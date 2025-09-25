using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpgradePanel : View
{
    public override string Name
    {
        get
        {
            return Consts.V_SkillUpgradePanel;
        }
    }

    [SerializeField]
    private List<Sprite> propertyIcon; //属性图标
    [SerializeField]
    private List<Sprite> upgradeIcon; //技能升级按钮状态图标
    [SerializeField]
    private Text propertyNameText; //属性名称
    [SerializeField]
    private Text currentLevelText; //当前等级
    [SerializeField]
    private Text nextLevelText; //下一级等级
    [SerializeField]
    private Image icon; //属性图标对象
    [SerializeField]
    private Text currentNum;//当前属性值
    [SerializeField]
    private Text nextNum;//下一级属性值
    [SerializeField]
    private Text skillPointTxt;//需要消耗的技能点数
    [SerializeField]
    private Button propertyUpgradeBtn; //属性升级按钮
    [SerializeField]
    private Button CloseBtn; //关闭按钮
    [SerializeField]
    private GameObject BigArrow;//大箭头
    [SerializeField]
    private GameObject SmallArrow;//小箭头

    private SkillPointType skillPointType;
    private int skillPointIndex;

    private Vector3 pos_1;//记录currentLevelText的位置
    private Vector3 pos_2;//记录currentNum的位置
    private Vector3 pos_3;//记录icon的位置
    private void Start()
    {
        propertyUpgradeBtn.onClick.AddListener(OnPropertyUpgradeBtnClick);
        CloseBtn.onClick.AddListener(OnCloseBtnClick);
    }

    public void Init(SkillPointType skillPointType, int index)
    {
        this.skillPointType = skillPointType;
        this.skillPointIndex = index;
        pos_1 = currentLevelText.gameObject.transform.localPosition;
        pos_2 = currentNum.gameObject.transform.localPosition;
        pos_3 = icon.gameObject.transform.localPosition;
        InitUI();
    }
    private void InitUI()
    {
        propertyUpgradeBtn.gameObject.transform.Find("Text").GetComponent<Text>().text = "升级";
        nextLevelText.gameObject.SetActive(true);
        nextNum.gameObject.SetActive(true);
        BigArrow.SetActive(true);
        SmallArrow.SetActive(true);
        skillPointTxt.gameObject.SetActive(true);
        propertyUpgradeBtn.interactable = true;

        if (skillPointType == SkillPointType.Atk)
        {
            propertyNameText.text = "攻击";
            propertyUpgradeBtn.gameObject.GetComponent<Image>().sprite = GetModel<GameModel>().SkillPoint > 0 && GetModel<GameModel>().AtkProLevel < 5 ? upgradeIcon[1] : upgradeIcon[0];
            if (propertyUpgradeBtn.gameObject.GetComponent<Image>().sprite == upgradeIcon[0])
            {
                propertyUpgradeBtn.interactable = false;
            }

            currentLevelText.text = "等级：" + GetModel<GameModel>().AtkProLevel.ToString();
            nextLevelText.text = "等级：" + (GetModel<GameModel>().AtkProLevel + 1).ToString();
            currentNum.text = (GetModel<GameModel>().AtkProLevel * 10).ToString();
            nextNum.text = ((GetModel<GameModel>().AtkProLevel + 1) * 10).ToString();
            if (GetModel<GameModel>().AtkProLevel == 5)
            {
                InitMaxLevelUI();
            }
        }
        else if (skillPointType == SkillPointType.Hp)
        {
            propertyNameText.text = "生命";
            propertyUpgradeBtn.gameObject.GetComponent<Image>().sprite = GetModel<GameModel>().SkillPoint > 0 && GetModel<GameModel>().HpProLevel < 5 ? upgradeIcon[1] : upgradeIcon[0];
            if (propertyUpgradeBtn.gameObject.GetComponent<Image>().sprite == upgradeIcon[0])
            {
                propertyUpgradeBtn.interactable = false;
            }
            currentLevelText.text = "等级：" + GetModel<GameModel>().HpProLevel.ToString();
            nextLevelText.text = "等级：" + (GetModel<GameModel>().HpProLevel + 1).ToString();
            currentNum.text = (GetModel<GameModel>().HpProLevel * 20).ToString();
            nextNum.text = ((GetModel<GameModel>().HpProLevel + 1) * 20).ToString();
            if (GetModel<GameModel>().HpProLevel == 5)
            {
                InitMaxLevelUI();
            }
        }
        else if (skillPointType == SkillPointType.Crit)
        {
            propertyNameText.text = "暴击";
            propertyUpgradeBtn.gameObject.GetComponent<Image>().sprite = GetModel<GameModel>().SkillPoint > 0 && GetModel<GameModel>().CritProLevel[skillPointIndex] < 2 ? upgradeIcon[1] : upgradeIcon[0];
            if (propertyUpgradeBtn.gameObject.GetComponent<Image>().sprite == upgradeIcon[0])
            {
                propertyUpgradeBtn.interactable = false;
            }
            currentLevelText.text = "等级：" + GetModel<GameModel>().CritProLevel[skillPointIndex].ToString();
            nextLevelText.text = "等级：" + (GetModel<GameModel>().CritProLevel[skillPointIndex] + 1).ToString();
            currentNum.text = (GetModel<GameModel>().CritProLevel[skillPointIndex] * 2).ToString() + "%";
            if (GetModel<GameModel>().CritProLevel[skillPointIndex] == 0)
            {
                currentNum.text = "0";
            }
            nextNum.text = ((GetModel<GameModel>().CritProLevel[skillPointIndex] + 1) * 2).ToString() + "%";
            if (GetModel<GameModel>().CritProLevel[skillPointIndex] == 2)
            {
                InitMaxLevelUI();
            }
        }
        else if (skillPointType == SkillPointType.AddDamage)
        {
            propertyNameText.text = "增伤";
            propertyUpgradeBtn.gameObject.GetComponent<Image>().sprite = GetModel<GameModel>().SkillPoint > 0 && GetModel<GameModel>().AddDamageProLevel[skillPointIndex] < 2 ? upgradeIcon[1] : upgradeIcon[0];
            if (propertyUpgradeBtn.gameObject.GetComponent<Image>().sprite == upgradeIcon[0])
            {
                propertyUpgradeBtn.interactable = false;
            }
            currentLevelText.text = "等级：" + GetModel<GameModel>().AddDamageProLevel[skillPointIndex].ToString();
            nextLevelText.text = "等级：" + (GetModel<GameModel>().AddDamageProLevel[skillPointIndex] + 1).ToString();
            currentNum.text = (GetModel<GameModel>().AddDamageProLevel[skillPointIndex] * 2).ToString() + "%";
            if (GetModel<GameModel>().AddDamageProLevel[skillPointIndex] == 0)
            {
                currentNum.text = "0";
            }
            nextNum.text = ((GetModel<GameModel>().AddDamageProLevel[skillPointIndex] + 1) * 2).ToString() + "%";
            if (GetModel<GameModel>().AddDamageProLevel[skillPointIndex] == 2)
            {
                InitMaxLevelUI();
            }
        }
        else if (skillPointType == SkillPointType.Dodge)
        {
            propertyNameText.text = "闪避";
            propertyUpgradeBtn.gameObject.GetComponent<Image>().sprite = GetModel<GameModel>().SkillPoint > 0 && GetModel<GameModel>().DodgeProLevel[skillPointIndex] < 2 ? upgradeIcon[1] : upgradeIcon[0];
            if (propertyUpgradeBtn.gameObject.GetComponent<Image>().sprite == upgradeIcon[0])
            {
                propertyUpgradeBtn.interactable = false;
            }
            currentLevelText.text = "等级：" + GetModel<GameModel>().DodgeProLevel[skillPointIndex].ToString();
            nextLevelText.text = "等级：" + (GetModel<GameModel>().DodgeProLevel[skillPointIndex] + 1).ToString();
            currentNum.text = (GetModel<GameModel>().DodgeProLevel[skillPointIndex] * 2).ToString() + "%";
            if (GetModel<GameModel>().DodgeProLevel[skillPointIndex] == 0)
            {
                currentNum.text = "0";
            }
            nextNum.text = ((GetModel<GameModel>().DodgeProLevel[skillPointIndex] + 1) * 2).ToString() + "%";
            if (GetModel<GameModel>().DodgeProLevel[skillPointIndex] == 2)
            {
                InitMaxLevelUI();
            }
        }
        else if (skillPointType == SkillPointType.DamageReduction)
        {
            propertyNameText.text = "减伤";
            propertyUpgradeBtn.gameObject.GetComponent<Image>().sprite = GetModel<GameModel>().SkillPoint > 0 && GetModel<GameModel>().DamageReducProLevel[skillPointIndex] < 2 ? upgradeIcon[1] : upgradeIcon[0];
            if (propertyUpgradeBtn.gameObject.GetComponent<Image>().sprite == upgradeIcon[0])
            {
                propertyUpgradeBtn.interactable = false;
            }
            currentLevelText.text = "等级：" + GetModel<GameModel>().DamageReducProLevel[skillPointIndex].ToString();
            nextLevelText.text = "等级：" + (GetModel<GameModel>().DamageReducProLevel[skillPointIndex] + 1).ToString();
            currentNum.text = (GetModel<GameModel>().DamageReducProLevel[skillPointIndex] * 2).ToString() + "%";
            if (GetModel<GameModel>().DamageReducProLevel[skillPointIndex] == 0)
            {
                currentNum.text = "0";
            }
            nextNum.text = ((GetModel<GameModel>().DamageReducProLevel[skillPointIndex] + 1) * 2).ToString() + "%";
            if (GetModel<GameModel>().DamageReducProLevel[skillPointIndex] == 2)
            {
                InitMaxLevelUI();
            }
        }
        icon.sprite = propertyIcon[(int)skillPointType];
        skillPointTxt.text = "需要消耗的技能点数：1";
    }
    private void OnPropertyUpgradeBtnClick()
    {
        if (skillPointType == SkillPointType.Atk)
        {
            GetModel<GameModel>().SkillPoint--;
            GetModel<GameModel>().AtkProLevel++;
            //玩家加10点攻击力
            GetModel<GameModel>().SkillTreeAtk += 10;

            GetModel<GameModel>().CritBranchLine[0] = true;
            GetModel<GameModel>().AddDamageBranchLine[0] = true;
        }
        else if (skillPointType == SkillPointType.Hp)
        {
            GetModel<GameModel>().SkillPoint--;
            GetModel<GameModel>().HpProLevel++;
            //玩家加20点生命值
            GetModel<GameModel>().SkillTreeHp += 20;

            GetModel<GameModel>().DodgeBranchLine[0] = true;
            GetModel<GameModel>().DamageReducBranchLine[0] = true;
        }
        else if (skillPointType == SkillPointType.Crit)
        {
            switch (skillPointIndex)
            {
                case 0:
                    if (GetModel<GameModel>().AtkProLevel <= 0) return;
                    if (GetModel<GameModel>().SkillPoint > 0 && GetModel<GameModel>().CritProLevel[0] < 2)
                    {
                        GetModel<GameModel>().SkillPoint--;
                        GetModel<GameModel>().CritProLevel[0]++;
                        //玩家加2%暴击率
                        GetModel<GameModel>().SkillTreeCritRate += 2;
                        GetModel<GameModel>().CritBranchLine[1] = true;
                    }
                    break;
                case 1:
                    if (GetModel<GameModel>().CritProLevel[0] <= 0) return;
                    if (GetModel<GameModel>().SkillPoint > 0 && GetModel<GameModel>().CritProLevel[1] < 2)
                    {
                        GetModel<GameModel>().SkillPoint--;
                        GetModel<GameModel>().CritProLevel[1]++;
                        //玩家加2%暴击率
                        GetModel<GameModel>().SkillTreeCritRate += 2;
                        GetModel<GameModel>().CritBranchLine[2] = true;
                    }
                    break;
                case 2:
                    if (GetModel<GameModel>().Skill1Level <= 0) return;
                    if (GetModel<GameModel>().SkillPoint > 0 && GetModel<GameModel>().CritProLevel[2] < 2)
                    {
                        GetModel<GameModel>().SkillPoint--;
                        GetModel<GameModel>().CritProLevel[2]++;
                        //玩家加2%暴击率
                        GetModel<GameModel>().SkillTreeCritRate += 2;
                        GetModel<GameModel>().CritBranchLine[4] = true;
                    }
                    break;
                case 3:
                    if (GetModel<GameModel>().CritProLevel[2] <= 0) return;
                    if (GetModel<GameModel>().SkillPoint > 0 && GetModel<GameModel>().CritProLevel[3] < 2)
                    {
                        GetModel<GameModel>().SkillPoint--;
                        GetModel<GameModel>().CritProLevel[3]++;
                        //玩家加2%暴击率
                        GetModel<GameModel>().SkillTreeCritRate += 2;
                        GetModel<GameModel>().CritBranchLine[5] = true;
                    }
                    break;
                default:
                    break;
            }
        }
        else if (skillPointType == SkillPointType.AddDamage)
        {
            switch (skillPointIndex)
            {
                case 0:
                    if (GetModel<GameModel>().AtkProLevel <= 0) return;
                    if (GetModel<GameModel>().SkillPoint > 0 && GetModel<GameModel>().AddDamageProLevel[0] < 2)
                    {
                        GetModel<GameModel>().SkillPoint--;
                        GetModel<GameModel>().AddDamageProLevel[0]++;
                        //玩家加2%伤害增益
                        GetModel<GameModel>().SkillTreeDamageAdd += 2;
                        GetModel<GameModel>().AddDamageBranchLine[1] = true;
                    }
                    break;
                case 1:
                    if (GetModel<GameModel>().AddDamageProLevel[0] <= 0) return;
                    if (GetModel<GameModel>().SkillPoint > 0 && GetModel<GameModel>().AddDamageProLevel[1] < 2)
                    {
                        GetModel<GameModel>().SkillPoint--;
                        GetModel<GameModel>().AddDamageProLevel[1]++;
                        //玩家加2%伤害增益
                        GetModel<GameModel>().SkillTreeDamageAdd += 2;
                        GetModel<GameModel>().AddDamageBranchLine[2] = true;
                    }
                    break;
                case 2:
                    if (GetModel<GameModel>().Skill1Level <= 0) return;
                    if (GetModel<GameModel>().SkillPoint > 0 && GetModel<GameModel>().AddDamageProLevel[2] < 2)
                    {
                        GetModel<GameModel>().SkillPoint--;
                        GetModel<GameModel>().AddDamageProLevel[2]++;
                        //玩家加2%伤害增益
                        GetModel<GameModel>().SkillTreeDamageAdd += 2;
                        GetModel<GameModel>().AddDamageBranchLine[4] = true;
                    }
                    break;
                case 3:
                    if (GetModel<GameModel>().AddDamageProLevel[2] <= 0) return;
                    if (GetModel<GameModel>().SkillPoint > 0 && GetModel<GameModel>().AddDamageProLevel[3] < 2)
                    {
                        GetModel<GameModel>().SkillPoint--;
                        GetModel<GameModel>().AddDamageProLevel[3]++;
                        //玩家加2%伤害增益
                        GetModel<GameModel>().SkillTreeDamageAdd += 2;
                        GetModel<GameModel>().AddDamageBranchLine[5] = true;
                    }
                    break;
                default:
                    break;
            }
        }
        else if (skillPointType == SkillPointType.Dodge)
        {
            switch (skillPointIndex)
            {
                case 0:
                    if (GetModel<GameModel>().HpProLevel <= 0) return;
                    if (GetModel<GameModel>().SkillPoint > 0 && GetModel<GameModel>().DodgeProLevel[0] < 2)
                    {
                        GetModel<GameModel>().SkillPoint--;
                        GetModel<GameModel>().DodgeProLevel[0]++;
                        //玩家加2%闪避率
                        GetModel<GameModel>().SkillTreeDodgeRate += 2;
                        GetModel<GameModel>().DodgeBranchLine[1] = true;
                    }
                    break;
                case 1:
                    if (GetModel<GameModel>().DodgeProLevel[0] <= 0) return;
                    if (GetModel<GameModel>().SkillPoint > 0 && GetModel<GameModel>().DodgeProLevel[1] < 2)
                    {
                        GetModel<GameModel>().SkillPoint--;
                        GetModel<GameModel>().DodgeProLevel[1]++;
                        //玩家加2%闪避率
                        GetModel<GameModel>().SkillTreeDodgeRate += 2;
                        GetModel<GameModel>().DodgeBranchLine[2] = true;
                    }
                    break;
                case 2:
                    if (GetModel<GameModel>().Skill3Level <= 0) return;
                    if (GetModel<GameModel>().SkillPoint > 0 && GetModel<GameModel>().DodgeProLevel[2] < 2)
                    {
                        GetModel<GameModel>().SkillPoint--;
                        GetModel<GameModel>().DodgeProLevel[2]++;
                        //玩家加2%闪避率
                        GetModel<GameModel>().SkillTreeDodgeRate += 2;
                        GetModel<GameModel>().DodgeBranchLine[4] = true;
                    }
                    break;
                case 3:
                    if (GetModel<GameModel>().DodgeProLevel[2] <= 0) return;
                    if (GetModel<GameModel>().SkillPoint > 0 && GetModel<GameModel>().DodgeProLevel[3] < 2)
                    {
                        GetModel<GameModel>().SkillPoint--;
                        GetModel<GameModel>().DodgeProLevel[3]++;
                        //玩家加2%闪避率
                        GetModel<GameModel>().SkillTreeDodgeRate += 2;
                        GetModel<GameModel>().DodgeBranchLine[5] = true;
                    }
                    break;
                default:
                    break;
            }
        }
        else if (skillPointType == SkillPointType.DamageReduction)
        {
            switch (skillPointIndex)
            {
                case 0:
                    if (GetModel<GameModel>().HpProLevel <= 0) return;
                    if (GetModel<GameModel>().SkillPoint > 0 && GetModel<GameModel>().DamageReducProLevel[0] < 2)
                    {
                        GetModel<GameModel>().SkillPoint--;
                        GetModel<GameModel>().DamageReducProLevel[0]++;
                        //玩家加2%伤害减免
                        GetModel<GameModel>().SkillTreeDamageReduc += 2;
                        GetModel<GameModel>().DamageReducBranchLine[1] = true;
                    }
                    break;
                case 1:
                    if (GetModel<GameModel>().DamageReducProLevel[0] <= 0) return;
                    if (GetModel<GameModel>().SkillPoint > 0 && GetModel<GameModel>().DamageReducProLevel[1] < 2)
                    {
                        GetModel<GameModel>().SkillPoint--;
                        GetModel<GameModel>().DamageReducProLevel[1]++;
                        //玩家加2%伤害减免
                        GetModel<GameModel>().SkillTreeDamageReduc += 2;
                        GetModel<GameModel>().DamageReducBranchLine[2] = true;
                    }
                    break;
                case 2:
                    if (GetModel<GameModel>().Skill3Level <= 0) return;
                    if (GetModel<GameModel>().SkillPoint > 0 && GetModel<GameModel>().DamageReducProLevel[2] < 2)
                    {
                        GetModel<GameModel>().SkillPoint--;
                        GetModel<GameModel>().DamageReducProLevel[2]++;
                        //玩家加2%伤害减免
                        GetModel<GameModel>().SkillTreeDamageReduc += 2;
                        GetModel<GameModel>().DamageReducBranchLine[4] = true;
                    }
                    break;
                case 3:
                    if (GetModel<GameModel>().DamageReducProLevel[2] <= 0) return;
                    if (GetModel<GameModel>().SkillPoint > 0 && GetModel<GameModel>().DamageReducProLevel[3] < 2)
                    {
                        GetModel<GameModel>().SkillPoint--;
                        GetModel<GameModel>().DamageReducProLevel[3]++;
                        //玩家加2%伤害减免
                        GetModel<GameModel>().SkillTreeDamageReduc += 2;
                        GetModel<GameModel>().DamageReducBranchLine[5] = true;
                    }
                    break;
                default:
                    break;
            }
        }
        InitUI();
        this.TriggerEvent(EventName.UpdateSkillTree);
    }

    // 满级时的UI调整
    private void InitMaxLevelUI()
    {
        propertyUpgradeBtn.gameObject.transform.Find("Text").GetComponent<Text>().text = "已满级";
        nextLevelText.gameObject.SetActive(false);
        nextNum.gameObject.SetActive(false);
        BigArrow.SetActive(false);
        SmallArrow.SetActive(false);
        skillPointTxt.gameObject.SetActive(false);
        //调整currentLevelText和currentNum以及icon的位置
        currentLevelText.gameObject.transform.localPosition = new Vector3(0, pos_1.y, pos_1.z);
        currentNum.gameObject.transform.localPosition = new Vector3(0, pos_2.y, pos_2.z);
        icon.gameObject.transform.localPosition = new Vector3(-110, pos_3.y, pos_3.z);
    }

    private void OnCloseBtnClick()
    {
        currentLevelText.gameObject.transform.localPosition = pos_1;
        currentNum.gameObject.transform.localPosition = pos_2;
        icon.gameObject.transform.localPosition = pos_3;
        gameObject.SetActive(false);
    }
    public override void HandleEvent(object data = null)
    {
    }
}
