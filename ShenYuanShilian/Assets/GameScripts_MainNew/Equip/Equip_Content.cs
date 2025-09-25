using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equip_Content : MonoBehaviour
{
    public Image equip;
    public GameObject isWear;
    [HideInInspector]
    public bool isWearing;
    public bool isSlotIns;
    public Image quality;
    [HideInInspector]
    public bool isOpenFromEquip;
    public EquipInfoData thisEquip;

    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(ShowEquip);
    }

    private void OnDestroy()
    {
        this.GetComponent<Button>().onClick.RemoveListener(ShowEquip);
    }

    private void ShowEquip()
    {
        EventManager.Instance.TriggerEvent(EventName.ShowEquip,null,new ShowEquip()
        {
            equipName = thisEquip.name,
            isWearing = isWearing || isSlotIns,
            isOpenFromEquip = isOpenFromEquip,
        });
    }

    public void InitContent(EquipInfoData curEquip)
    {
        thisEquip = curEquip;
        equip.sprite = SpriteManager.Instance.GetEquipSprite(curEquip.type, curEquip.id);
        quality.sprite = SpriteManager.Instance.GetQualitySprite(curEquip.quality);
        isWear.SetActive(isWearing && isOpenFromEquip && !isSlotIns);
    }

    public void Wearing()
    {
        isWear.SetActive(true);
    }
    
    public void UnWearing()
    {
        isWear.SetActive(false);
    }
}
