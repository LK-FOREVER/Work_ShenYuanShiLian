using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class MainSceneManager : View
{
    public override string Name
    {
        get
        {
            return Consts.V_MainScene;
        }
    }
    [SerializeField]
    private GameObject guidanceBoard;
    [SerializeField]
    private List<Toggle> toggles;
    [SerializeField]
    private GameObject upgradationPage;
    [SerializeField]
    private GameObject characterPage;
    [SerializeField]
    private GameObject mapPage;

    [SerializeField]
    private List<Sprite> propertyIcons;
    [SerializeField]
    private List<Sprite> propertySliderFills;
    [SerializeField]
    private Image propertyIcon;
    [SerializeField]
    private Image propertySliderFill;
    [SerializeField]
    private TMP_Text defNum;
    [SerializeField]
    private TMP_Text hpNum;
    [SerializeField]
    private TMP_Text coinNum;
    [SerializeField]
    private TMP_Text diamondNum;

    [SerializeField]
    private GameObject equipmentPrefab;
    [SerializeField]
    private List<GameObject> equipmentSlots;
    [SerializeField]
    private List<Sprite> soldierEquipmentSprites;
    [SerializeField]
    private List<Sprite> archerEquipmentSprites;
    [SerializeField]
    private List<Sprite> masterEquipmentSprites;

    [SerializeField]
    private GameObject setBoard;

    [SerializeField]
    private GameObject warnBoard;
    [SerializeField]
    private Button ShopBtn;
    [SerializeField]
    private GameObject ShopBoard;
    [SerializeField]
    private GameObject ChallengeBoard;
    [SerializeField]
    private GameObject rankListBoard;

    public ShopPanelController shopPanelController;
    public TaskPopup TaskPopup;

    private void Awake()
    {
        // SetCharacterInfo();
        UpdateCoin();
        UpdateDiamond();
        //UpdateEquipment();
        Utils.FadeOut();
    }

    public void OnUpgradationToggleTrigger(bool isOn)
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        upgradationPage.SetActive(isOn);
    }

    public void OnCharacterToggleTrigger(bool isOn)
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        characterPage.SetActive(isOn);
    }

    public void OnMapToggleTrigger(bool isOn)
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        mapPage.SetActive(isOn);
    }

    public void OnBtnSet()
    {
        setBoard.SetActive(true);
    }

    public void OnShowShop()
    {
        ShopBoard.SetActive(true);
    }

    public void OnShowChallenge()
    {
        ChallengeBoard.SetActive(true);
    }
    public void OnShowRankList()
    {
        rankListBoard.SetActive(true);
    }

    public void OnShowWarn()
    {
        warnBoard.SetActive(true);
        Time.timeScale = 0;
    }
    public void OnCloseWarn()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        warnBoard.SetActive(false);
        Time.timeScale = 1;
        GameManager.Instance.ChangeScene(Scene.Loading);
    }

    private void SetCharacterInfo()
    {
        propertyIcon.sprite = propertyIcons[GetModel<GameModel>().CurId];
        propertySliderFill.sprite = propertySliderFills[GetModel<GameModel>().CurId];
        defNum.text = DataManager.Instance.upgradeInfoDic[GetModel<GameModel>().CurId][GetModel<GameModel>().PropLevel[GetModel<GameModel>().CurId]].mp.ToString();
        hpNum.text = DataManager.Instance.upgradeInfoDic[GetModel<GameModel>().CurId][GetModel<GameModel>().HpLevel[GetModel<GameModel>().CurId]].hp.ToString();
    }

    private void UpdateEquipment()
    {
        if (SceneManager.GetActiveScene().name != "Main") return;
        int[] value = { -1, -1, -1 };
        switch (GetModel<GameModel>().CurId)
        {
            case 0:
                value = GetModel<GameModel>().SoldierEquipments;
                break;
            case 1:
                value = GetModel<GameModel>().ArcherEquipments;
                break;
            case 2:
                value = GetModel<GameModel>().MasterEquipments;
                break;
            default: break;
        }

        for (int i = 0; i < value.Length; i++)
        {

            if (equipmentSlots[i].transform.childCount > 0)
            {
                DestroyImmediate(equipmentSlots[i].transform.GetChild(0).gameObject);
            }

            if (value[i] == -1) continue;
            EquipmentInfo info = DataManager.Instance.equipmentInfoDic[value[i]];
            GameObject equipment = Instantiate(equipmentPrefab);
            switch (GetModel<GameModel>().CurId)
            {
                case 0:
                    equipment.transform.Find("Icon").GetComponent<Image>().sprite = soldierEquipmentSprites[info.type];
                    break;
                case 1:
                    equipment.transform.Find("Icon").GetComponent<Image>().sprite = archerEquipmentSprites[info.type];
                    break;
                case 2:
                    equipment.transform.Find("Icon").GetComponent<Image>().sprite = masterEquipmentSprites[info.type];
                    break;
                default: break;
            }

            equipment.transform.Find("Name").GetComponent<Text>().text = info.name;
            equipment.transform.Find("Desc").GetComponent<Text>().text = info.desc;
            equipment.transform.SetParent(equipmentSlots[info.type].transform, false);
        }
    }

    private void UpdateCoin()
    {
        coinNum.text = $"{GetModel<GameModel>().Coin}";
    }

    private void UpdateDiamond()
    {
        diamondNum.text = $"{GetModel<GameModel>().Diamond}";
    }

    public override void RegisterViewEvent()
    {
        base.RegisterViewEvent();
        attractEventList.Add(Consts.E_ChangeCharacterModel);
        attractEventList.Add(Consts.E_UpdateCoin);
        attractEventList.Add(Consts.E_UpdateDiamond);
    }

    public override void HandleEvent(object data = null)
    {
        // SetCharacterInfo();
        UpdateCoin();
        UpdateDiamond();
        //UpdateEquipment();
    }
}
