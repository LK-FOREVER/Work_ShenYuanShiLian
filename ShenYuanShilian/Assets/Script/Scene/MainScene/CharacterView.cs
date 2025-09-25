using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//原来是三个角色的信息界面，现改为装备升级界面
public class CharacterView : View
{
    public override string Name
    {
        get
        {
            return Consts.V_CharacterView;
        }
    }

    [SerializeField]
    private GameObject prop;
    [SerializeField]
    private GameObject propPrefab;
    [SerializeField]
    private List<GameObject> toggles;
    [SerializeField]
    private List<Sprite> icons;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TMP_Text profession;
    [SerializeField]
    private Text desc;
    [SerializeField]
    private GameObject purchaseTip;

    private int curId;

    private GameObject helmet;
    private GameObject corselet;

    private GameObject cuish;


    private void Awake()
    {
        ChangeCharacterInfo(GetModel<GameModel>().CurId);
        // SetToggleStatus();

        helmet = Instantiate(propPrefab);
        helmet.transform.SetParent(prop.transform);
        corselet = Instantiate(propPrefab);
        corselet.transform.SetParent(prop.transform);
        cuish = Instantiate(propPrefab);
        cuish.transform.SetParent(prop.transform);

        SetProp(GetModel<GameModel>().CurId);
    }

    private void SetProp(int id)
    {
        helmet.GetComponent<PropertyView>().init(id, Property.Helmet);
        corselet.GetComponent<PropertyView>().init(id, Property.Corselet);
        cuish.GetComponent<PropertyView>().init(id, Property.Cuish);
    }
    private void SetToggleStatus()
    {
        for (int i = 0; i < GetModel<GameModel>().Unlock.Length; i++)
        {
            if (!GetModel<GameModel>().Unlock[i])
            {
                toggles[i].transform.Find("Lock").gameObject.SetActive(true);
                toggles[i].transform.Find("Prohibit").gameObject.SetActive(true);
            }
            else
            {
                toggles[i].transform.Find("Lock").gameObject.SetActive(false);
                toggles[i].transform.Find("Prohibit").gameObject.SetActive(false);
            }
        }
    }

    public void OnSoldierToggleTrigger(bool isOn)
    {
        if (!GetModel<GameModel>().Unlock[(int)Character.Soldier])
        {
            toggles[curId].GetComponent<Toggle>().isOn = true;
            ShowPurchaseTip((int)Character.Soldier);
            return;
        }
        if (!isOn) return;
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        ChangeCharacterInfo((int)Character.Soldier);
    }

    public void OnArcherToggleTrigger(bool isOn)
    {
        if (!GetModel<GameModel>().Unlock[(int)Character.Archer])
        {
            toggles[curId].GetComponent<Toggle>().isOn = true;
            ShowPurchaseTip((int)Character.Archer);
            return;
        }
        if (!isOn) return;
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        ChangeCharacterInfo((int)Character.Archer);
    }

    public void OnMasterToggleTrigger(bool isOn)
    {
        if (!GetModel<GameModel>().Unlock[(int)Character.Master])
        {
            toggles[curId].GetComponent<Toggle>().isOn = true;
            ShowPurchaseTip((int)Character.Master);
            return;
        }
        if (!isOn) return;
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        ChangeCharacterInfo((int)Character.Master);
    }

    private void ShowPurchaseTip(int id)
    {
        purchaseTip.SetActive(true);
        purchaseTip.GetComponent<PurchaseTipView>().init(id);
    }

    private void ChangeCharacterInfo(int id)
    {
        curId = id;
        // icon.sprite = icons[id];
        // profession.text = DataManager.Instance.characterInfoDic[id].name;
        // desc.text = DataManager.Instance.characterInfoDic[id].desc;
    }

    public void OnChooseBtn()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        SendViewEvent(Consts.E_ChooseCharacter, new ChooseCharacter() { id = curId });
    }

    public override void RegisterViewEvent()
    {
        base.RegisterViewEvent();
        attractEventList.Add(Consts.E_ChangeCharacterModel);
    }

    public override void HandleEvent(object data = null)
    {
        SetToggleStatus();
    }
}
