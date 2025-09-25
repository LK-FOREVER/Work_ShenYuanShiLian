using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradationView : View
{
    public override string Name
    {
        get
        {
            return Consts.V_UpgradationView;
        }
    }

    [SerializeField]
    private GameObject prop;
    [SerializeField]
    private GameObject propPrefab;
    [SerializeField]
    private GameObject toggleGroup;
    [SerializeField]
    private List<GameObject> toggles;
    private GameObject hp;
    private GameObject atk;

    private GameObject property;

    private void Awake()
    {
        hp = Instantiate(propPrefab);
        hp.transform.SetParent(prop.transform);
        atk = Instantiate(propPrefab);
        atk.transform.SetParent(prop.transform);
        property = Instantiate(propPrefab);
        property.transform.SetParent(prop.transform);

        SetToggleStatus();
        SetProp(GetModel<GameModel>().CurId);
    }

    private void OnEnable()
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            toggles[i].GetComponent<Toggle>().isOn = i== GetModel<GameModel>().CurId;
        }
    }

    public void OnSoldierToggleTrigger(bool isOn)
    {
        if (!isOn) return;
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        SetProp((int)Character.Soldier);
    }

    public void OnArcherToggleTrigger(bool isOn)
    {
        if (!isOn) return;
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        SetProp((int)Character.Archer);
    }

    public void OnMasterToggleTrigger(bool isOn)
    {
        if (!isOn) return;
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        SetProp((int)Character.Master);
    }

    private void SetProp(int id)
    {
        hp.GetComponent<PropertyView>().init(id,Property.Hp);
        atk.GetComponent<PropertyView>().init(id, Property.Atk);
        property.GetComponent<PropertyView>().init(id, Property.Prop);
    }

    private void SetToggleStatus()
    {
        for (int i = 0; i < GetModel<GameModel>().Unlock.Length; i++)
        {
            toggles[i].GetComponent<Toggle>().interactable = GetModel<GameModel>().Unlock[i];
        }
        toggleGroup.SetActive(false);
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
