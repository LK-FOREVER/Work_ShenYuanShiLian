using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EquipPanelController : View
{
    public override string Name { get; }
    public override void HandleEvent(object data = null)
    {
        throw new NotImplementedException();
    }
    
    public Equip_Slot[] equips;

    public Transform insParent;

    public GameObject equipContent;

    public void Start()
    {
        EventManager.Instance.AddListener(EventName.RefreshEquip,RefreshEquipCallBack);
        EventManager.Instance.AddListener(EventName.RefreshSlot,RefreshStrength);
    }
    
    private void OnDestroy()
    {
        EventManager.Instance.RemoveListener(EventName.RefreshEquip,RefreshEquipCallBack);
        EventManager.Instance.RemoveListener(EventName.RefreshSlot,RefreshStrength);
    }

    private void RefreshEquipCallBack(object sender, EventArgs e)
    {
        RefreshEquipArgs args = e as RefreshEquipArgs;
        RefreshOwnedEquip(args.refreshType);
        Init();
    }

    private void OnEnable()
    {
        Init();
        RefreshOwnedEquip(EquipType.Weapon);
    }
    
    private void RefreshStrength(object sender, EventArgs e)
    {
        Init();
    }

    
    private void Init()
    {
        //如果武器有变动，则更新武器数据，若本来此部位没有武器，初始化生成武器
        foreach (var equip in equips)
        {
            int strengthLevel = GetModel<GameModel>().StrengthEquipLevel[equip.thisType];
            equip.strengthLevel = strengthLevel;
            equip.ChangeLevel();
            if (equip.insParent.childCount>0)
            {
                if (GetModel<GameModel>().WornEquipments[equip.thisType]!=null)
                {
                    if (GetModel<GameModel>().WornEquipments[equip.thisType].id != equip.id)
                    {
                        equip.isSlotIns = true;
                        equip.Init(GetModel<GameModel>().WornEquipments[equip.thisType],true);
                    }
                }
                else
                {
                    equip.NoEquipOnWear();
                }
            }
            else
            {
                if (GetModel<GameModel>().WornEquipments[equip.thisType]!=null)
                {
                    if (GetModel<GameModel>().WornEquipments[equip.thisType]!=null)
                    {
                        equip.isSlotIns = true;
                        equip.Init(GetModel<GameModel>().WornEquipments[equip.thisType],true);
                    }
                }
                else
                {
                    equip.NoEquipOnWear();
                }
              
            }
        }
    }

    public void RefreshOwnedEquip(EquipType refreshType)
    {
        
        for (int i = insParent.childCount - 1; i >= 0; i--)
        {
            Transform child = insParent.GetChild(i);
            MainObjectPool.Instance.ReturnToPool("Equip_Content",insParent.GetChild(i).gameObject);
        }
        
        List<EquipInfoData> equipList = GetModel<GameModel>().OwnedEquipments[refreshType].OrderByDescending(equip => equip.quality).ToList();
        foreach (var equip in equipList)
        {
            Equip_Content equipObj = MainObjectPool.Instance.SpawnFromPool("Equip_Content",Vector3.zero, Quaternion.identity).GetComponent<Equip_Content>();
            equipObj.transform.SetParent(insParent);
            equipObj.transform.localPosition = Vector3.zero;
            equipObj.transform.localScale = Vector3.one;
            equipObj.thisEquip = equip;
            equipObj.isOpenFromEquip = true;
            if (GetModel<GameModel>().WornEquipments[refreshType]!=null)
            {
                if (GetModel<GameModel>().WornEquipments[refreshType].name == equipObj.thisEquip.name)
                {
                    equipObj.isWearing = true;
                }
            }
            else
            {
                equipObj.isWearing = false;
            }
            equipObj.InitContent(equip);
          
        }
    }

    
 

}
