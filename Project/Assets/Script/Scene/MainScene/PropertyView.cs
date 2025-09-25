using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PropertyView : View
{
    public override string Name
    {
        get
        {
            return Consts.V_PropertyView;
        }
    }

    [SerializeField]
    private Sprite hpIcon;
    [SerializeField]
    private Sprite atkIcon;
    [SerializeField]
    private Sprite helmetIcon;
    [SerializeField]
    private Sprite corseletIcon;
    [SerializeField]
    private Sprite cuishIcon;
    [SerializeField]
    private List<Sprite> propertyIcon;
    [SerializeField]
    private Sprite upgradeButtonMaxSprite;//满级时的升级按钮图片
    [SerializeField]
    private Image upgradeButtonIcon;//升级按钮的图片
    [SerializeField]
    private Sprite normalSlot;
    [SerializeField]
    private Sprite hpSlot;
    [SerializeField]
    private Sprite atkSlot;
    [SerializeField]
    private Sprite equipmentSlot;
    [SerializeField]
    private List<Sprite> propertySlot;

    [SerializeField]
    private Image icon;
    [SerializeField]
    private List<Image> slots;
    [SerializeField]
    private TMP_Text curNum;
    [SerializeField]
    private TMP_Text nextNum;
    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private GameObject coinIcon;
    [SerializeField]
    private TMP_Text coinNum;

    private int id;
    private Property type;
    private int cost;

    public void init(int id, Property type)
    {
        this.id = id;
        this.type = type;

        switch (type)
        {
            case Property.Hp:
                icon.sprite = hpIcon;
                for (int i = 0; i < slots.Count; i++)
                {
                    slots[i].sprite = GetModel<GameModel>().HpLevel[id] > i ? hpSlot : normalSlot;
                }
                curNum.text = $"生命值 {DataManager.Instance.upgradeInfoDic[id][GetModel<GameModel>().HpLevel[id]].hp}";
                if (GetModel<GameModel>().HpLevel[id] < slots.Count)
                {
                    arrow.SetActive(true);
                    coinIcon.SetActive(true);
                    cost = DataManager.Instance.upgradeInfoDic[id][GetModel<GameModel>().HpLevel[id] + 1].price;
                    coinNum.text = $"X{cost}";
                    nextNum.gameObject.SetActive(true);
                    nextNum.text = $"{DataManager.Instance.upgradeInfoDic[id][GetModel<GameModel>().HpLevel[id] + 1].hp}";
                }
                else
                {
                    arrow.SetActive(false);
                    coinIcon.SetActive(false);
                    coinNum.text = $"满级";
                    upgradeButtonIcon.sprite = upgradeButtonMaxSprite;
                    nextNum.gameObject.SetActive(false);
                    upgradeButtonIcon.GetComponent<Button>().interactable = false;
                }
                break;
            case Property.Atk:
                icon.sprite = atkIcon;
                for (int i = 0; i < slots.Count; i++)
                {
                    slots[i].sprite = GetModel<GameModel>().AtkLevel[id] > i ? atkSlot : normalSlot;
                }
                // curNum.text = $"攻击力 {DataManager.Instance.upgradeInfoDic[id][GetModel<GameModel>().AtkLevel[id]].atkMin}-{DataManager.Instance.upgradeInfoDic[id][GetModel<GameModel>().AtkLevel[id]].atkMax}";
                curNum.text = $"攻击力 {DataManager.Instance.upgradeInfoDic[id][GetModel<GameModel>().AtkLevel[id]].atk}";

                if (GetModel<GameModel>().AtkLevel[id] < slots.Count)
                {
                    arrow.SetActive(true);
                    coinIcon.SetActive(true);
                    cost = DataManager.Instance.upgradeInfoDic[id][GetModel<GameModel>().AtkLevel[id] + 1].price;
                    coinNum.text = $"X{cost}";
                    nextNum.gameObject.SetActive(true);
                    // nextNum.text = $"{DataManager.Instance.upgradeInfoDic[id][GetModel<GameModel>().AtkLevel[id] + 1].atkMin}-{DataManager.Instance.upgradeInfoDic[id][GetModel<GameModel>().AtkLevel[id] + 1].atkMax}";
                    nextNum.text = $"{DataManager.Instance.upgradeInfoDic[id][GetModel<GameModel>().AtkLevel[id] + 1].atk}";
                }
                else
                {
                    arrow.SetActive(false);
                    coinIcon.SetActive(false);
                    coinNum.text = $"满级";
                    upgradeButtonIcon.sprite = upgradeButtonMaxSprite;
                    nextNum.gameObject.SetActive(false);
                    upgradeButtonIcon.GetComponent<Button>().interactable = false;
                }
                break;
            case Property.Prop:
                // 原来是护盾值，体力值，魔力值，现仅表示暴击率
                icon.sprite = propertyIcon[id];
                for (int i = 0; i < slots.Count; i++)
                {
                    slots[i].sprite = GetModel<GameModel>().PropLevel[id] > i ? propertySlot[id] : normalSlot;
                }
                switch (id)
                {
                    case 0:
                        curNum.text = $"护盾值 {DataManager.Instance.upgradeInfoDic[id][GetModel<GameModel>().PropLevel[id]].mp}";
                        break;
                    case 1:
                        curNum.text = $"体力值 {DataManager.Instance.upgradeInfoDic[id][GetModel<GameModel>().PropLevel[id]].mp}";
                        break;
                    case 2:
                        if (GetModel<GameModel>().PropLevel[id] == 0)
                        {
                            curNum.text = $"暴击率 {DataManager.Instance.upgradeInfoDic[id][GetModel<GameModel>().PropLevel[id]].crit}";
                        }
                        else
                        {
                            curNum.text = $"暴击率 {DataManager.Instance.upgradeInfoDic[id][GetModel<GameModel>().PropLevel[id]].crit * 100}%";
                        }
                        break;
                    default:
                        break;
                }
                if (GetModel<GameModel>().PropLevel[id] < slots.Count)
                {
                    arrow.SetActive(true);
                    coinIcon.SetActive(true);
                    cost = DataManager.Instance.upgradeInfoDic[id][GetModel<GameModel>().PropLevel[id] + 1].price;
                    coinNum.text = $"X{cost}";
                    nextNum.gameObject.SetActive(true);
                    nextNum.text = $"{DataManager.Instance.upgradeInfoDic[id][GetModel<GameModel>().PropLevel[id] + 1].crit * 100}%";
                }
                else
                {
                    arrow.SetActive(false);
                    coinIcon.SetActive(false);
                    coinNum.text = $"满级";
                    upgradeButtonIcon.sprite = upgradeButtonMaxSprite;
                    nextNum.gameObject.SetActive(false);
                    upgradeButtonIcon.GetComponent<Button>().interactable = false;
                }
                break;
            case Property.Helmet:
                icon.sprite = helmetIcon;
                for (int i = 0; i < slots.Count; i++)
                {
                    slots[i].sprite = GetModel<GameModel>().HelmetLevel[id] > i ? equipmentSlot : normalSlot;
                }
                curNum.text = $"生命值 {DataManager.Instance.equipmentUpgradeInfoDic[0][GetModel<GameModel>().HelmetLevel[id]].value}";
                if (GetModel<GameModel>().HelmetLevel[id] < slots.Count)
                {
                    arrow.SetActive(true);
                    coinIcon.SetActive(true);
                    cost = DataManager.Instance.equipmentUpgradeInfoDic[0][GetModel<GameModel>().HelmetLevel[id] + 1].price;
                    coinNum.text = $"X{cost}";
                    nextNum.gameObject.SetActive(true);
                    nextNum.text = $"{DataManager.Instance.equipmentUpgradeInfoDic[0][GetModel<GameModel>().HelmetLevel[id] + 1].value}";
                }
                else
                {
                    arrow.SetActive(false);
                    coinIcon.SetActive(false);
                    coinNum.text = $"满级";
                    upgradeButtonIcon.sprite = upgradeButtonMaxSprite;
                    nextNum.gameObject.SetActive(false);
                    upgradeButtonIcon.GetComponent<Button>().interactable = false;
                }
                break;
            case Property.Corselet:
                icon.sprite = corseletIcon;
                for (int i = 0; i < slots.Count; i++)
                {
                    slots[i].sprite = GetModel<GameModel>().CorseletLevel[id] > i ? equipmentSlot : normalSlot;
                }
                if (GetModel<GameModel>().CorseletLevel[id] == 0)
                {
                    curNum.text = $"减伤 {DataManager.Instance.equipmentUpgradeInfoDic[1][GetModel<GameModel>().CorseletLevel[id]].value}";
                }
                else
                {
                    curNum.text = $"减伤 {DataManager.Instance.equipmentUpgradeInfoDic[1][GetModel<GameModel>().CorseletLevel[id]].value * 100}%";
                }
                if (GetModel<GameModel>().CorseletLevel[id] < slots.Count)
                {
                    arrow.SetActive(true);
                    coinIcon.SetActive(true);
                    cost = DataManager.Instance.equipmentUpgradeInfoDic[1][GetModel<GameModel>().CorseletLevel[id] + 1].price;
                    coinNum.text = $"X{cost}";
                    nextNum.gameObject.SetActive(true);
                    nextNum.text = $"{DataManager.Instance.equipmentUpgradeInfoDic[1][GetModel<GameModel>().CorseletLevel[id] + 1].value * 100}%";
                }
                else
                {
                    arrow.SetActive(false);
                    coinIcon.SetActive(false);
                    coinNum.text = $"满级";
                    upgradeButtonIcon.sprite = upgradeButtonMaxSprite;
                    nextNum.gameObject.SetActive(false);
                    upgradeButtonIcon.GetComponent<Button>().interactable = false;
                }
                break;
            case Property.Cuish:
                icon.sprite = cuishIcon;
                for (int i = 0; i < slots.Count; i++)
                {
                    slots[i].sprite = GetModel<GameModel>().CuishLevel[id] > i ? equipmentSlot : normalSlot;
                }
                if (GetModel<GameModel>().CuishLevel[id] == 0)
                {
                    curNum.text = $"闪避 {DataManager.Instance.equipmentUpgradeInfoDic[2][GetModel<GameModel>().CuishLevel[id]].value}";
                }
                else
                {
                    curNum.text = $"闪避 {DataManager.Instance.equipmentUpgradeInfoDic[2][GetModel<GameModel>().CuishLevel[id]].value * 100}%";
                }
                if (GetModel<GameModel>().CuishLevel[id] < slots.Count)
                {
                    arrow.SetActive(true);
                    coinIcon.SetActive(true);
                    cost = DataManager.Instance.equipmentUpgradeInfoDic[2][GetModel<GameModel>().CuishLevel[id] + 1].price;
                    coinNum.text = $"X{cost}";
                    nextNum.gameObject.SetActive(true);
                    nextNum.text = $"{DataManager.Instance.equipmentUpgradeInfoDic[2][GetModel<GameModel>().CuishLevel[id] + 1].value * 100}%";
                }
                else
                {
                    arrow.SetActive(false);
                    coinIcon.SetActive(false);
                    coinNum.text = $"满级";
                    upgradeButtonIcon.sprite = upgradeButtonMaxSprite;
                    nextNum.gameObject.SetActive(false);
                    upgradeButtonIcon.GetComponent<Button>().interactable = false;
                }
                break;
            default:
                break;
        }
    }

    public void OnUpgradeBtn()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        //if (!GetModel<GameModel>().Unlock[id] || cost > GetModel<GameModel>().Coin) return;
        if (!GetModel<GameModel>().Unlock[id])
        {
            SendMsg("角色未解锁，无法升级");
            return;
        }
        if (cost > GetModel<GameModel>().Coin)
        {
            SendMsg("金币不足。");
            return;
        }
        switch (type)
        {
            case Property.Hp:
                if (GetModel<GameModel>().HpLevel[id] >= slots.Count) return;
                break;
            case Property.Atk:
                if (GetModel<GameModel>().AtkLevel[id] >= slots.Count) return;
                break;
            case Property.Prop:
                if (GetModel<GameModel>().PropLevel[id] >= slots.Count) return;
                break;
            case Property.Helmet:
                if (GetModel<GameModel>().HelmetLevel[id] >= slots.Count) return;
                break;
            case Property.Corselet:
                if (GetModel<GameModel>().CorseletLevel[id] >= slots.Count) return;
                break;
            case Property.Cuish:
                if (GetModel<GameModel>().CuishLevel[id] >= slots.Count) return;
                break;
            default: break;
        }
        SendViewEvent(Consts.E_Upgrade, new Upgrade() { id = id, type = type, cost = cost });
        init(id, type);
    }

    private void SendMsg(string str)
    {
        GameObject tip = GameObject.Find("PopupCanvas").transform.Find("TipBoard").gameObject;
        tip.SetActive(true);
        tip.GetComponent<TipBoard>().InitAuto(str);
    }

    public override void HandleEvent(object data = null)
    {
    }
}
