using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardOptionBoard : View
{
    public override string Name
    {
        get
        {
            return Consts.V_CardOptionBoard;
        }
    }

    [SerializeField]
    private List<Sprite> soldierEquipmentSprites;
    [SerializeField]
    private List<Sprite> archerEquipmentSprites;
    [SerializeField]
    private List<Sprite> masterEquipmentSprites;
    [SerializeField]
    private List<Sprite> commonBuffSprites;
    [SerializeField]
    private List<Sprite> soldierBuffSprites;
    [SerializeField]
    private List<Sprite> archerBuffSprites;
    [SerializeField]
    private List<Sprite> masterBuffSprites;
    [SerializeField]
    private GameObject equipmentPrefab;
    [SerializeField]
    private GameObject buffPrefab;
    [SerializeField]
    private Transform slots;
    [SerializeField]
    private GameObject skipButton;
    [SerializeField]
    private GameObject tip;
    [SerializeField]
    private GameObject mask;

    private void Awake()
    {
        EventManager.Instance.AddListener(EventName.CreateCardOption, CreateCardOption);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveListener(EventName.CreateCardOption, CreateCardOption);
    }

    private void CreateCardOption(object sender, EventArgs e)
    {
        mask.SetActive(true);
        CardOptionEventArgs args = e as CardOptionEventArgs;
        if(args.cardType == CardType.Equipment)
        {
            CreateEquipmentOption(args.id);
        }
        else if(args.cardType == CardType.CommonBuff)
        {
            CreateBuffOption("Common", args.id);
        }
        else
        {
            if(GetModel<GameModel>().CurId == 0)
            {
                CreateBuffOption("Soldier", args.id);
            }
            else if(GetModel<GameModel>().CurId == 1)
            {
                CreateBuffOption("Archer", args.id);
            }
            else
            {
                CreateBuffOption("Master", args.id);
            }
        }
        skipButton.SetActive(true);
    }

    private void CreateEquipmentOption(int id)
    {
        GameObject equipment = Instantiate(equipmentPrefab);
        EquipmentInfo equipmentInfo = DataManager.Instance.equipmentInfoDic[id];
        switch(GetModel<GameModel>().CurId)
        {
            case 0:
                equipment.transform.Find("Icon").GetComponent<Image>().sprite = soldierEquipmentSprites[equipmentInfo.type];
                break;
            case 1:
                equipment.transform.Find("Icon").GetComponent<Image>().sprite = archerEquipmentSprites[equipmentInfo.type];
                break;
            case 2:
                equipment.transform.Find("Icon").GetComponent<Image>().sprite = masterEquipmentSprites[equipmentInfo.type];
                break;
            default: break;
        }

        equipment.transform.Find("Name").GetComponent<Text>().text = equipmentInfo.name;
        equipment.transform.Find("Desc").GetComponent<Text>().text = equipmentInfo.desc;
        int coin = UnityEngine.Random.Range(equipmentInfo.priceMin, equipmentInfo.priceMax + 1);
        equipment.transform.Find("PurchaseButton").Find("Text").GetComponent<Text>().text = $"X{coin}";
        equipment.transform.SetParent(transform.Find("Slots"));
        equipment.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            OnEquipmentBtn(id, coin);
        });
    }

    private void CreateBuffOption(string type, int id)
    {
        GameObject buff = Instantiate(buffPrefab);
        SkillInfo buffInfo = DataManager.Instance.skillInfoDic[type][id];
        switch(type)
        {
            case "Common":
                buff.transform.Find("Icon").GetComponent<Image>().sprite = commonBuffSprites[id];
                break;
            case "Soldier":
                buff.transform.Find("Icon").GetComponent<Image>().sprite = soldierBuffSprites[id];
                break;
            case "Archer":
                buff.transform.Find("Icon").GetComponent<Image>().sprite = archerBuffSprites[id];
                break;
            case "Master":
                buff.transform.Find("Icon").GetComponent<Image>().sprite = masterBuffSprites[id];
                break;
            default:break;
        }
        buff.transform.Find("Name").GetComponent<Text>().text = buffInfo.name;
        buff.transform.Find("Desc").GetComponent<Text>().text = buffInfo.desc;
        buff.transform.SetParent(transform.Find("Slots"));
        buff.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            OnBuffBtn(type, id);
        });
    }

    public void OnSkipBtn()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        skipButton.SetActive(false);
        mask.SetActive(false);
        foreach (Transform child in slots)
        {
            Destroy(child.gameObject);
        }
        this.TriggerEvent(EventName.PassTile);
    }

    public void OnEquipmentBtn(int id, int coin)
    {
        if (GetModel<GameModel>().Coin < coin)
        {
            SendMsg("½ð±Ò²»×ã¡£");
            return;
        }
        int cost = GetModel<GameModel>().Coin - coin;
        SendViewEvent(Consts.E_ChangeCoin, new ChangeCoin() { coin = cost });
        SendViewEvent(Consts.E_SetEquipment, new SetEquipment() { id = id });
        OnSkipBtn();
    }

    public void OnBuffBtn(string type, int id)
    {
        this.TriggerEvent(EventName.SetBuff, new SetBuffEventArgs() { type = type, id = id });
        OnSkipBtn();
    }

    private void SendMsg(string str)
    {
        tip.SetActive(true);
        tip.GetComponent<TipBoard>().InitAuto(str);
    }

    public override void HandleEvent(object data = null)
    {
    }
}
