using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Equip_Slot : MonoBehaviour
{
    public EquipType thisType;
    public Transform insParent;
    public TextMeshProUGUI strengthLevelText;
    public GameObject equipContentPrefab;
    public bool isSlotIns = false;
    [HideInInspector]
    public int strengthLevel;
    [HideInInspector]
    public int id = -1;
    

    public void Init(EquipInfoData curEquip,bool isOpenFromEquip)
    {
        for (int i = 0; i < insParent.childCount; i++)
        {
            MainObjectPool.Instance.ReturnToPool("Equip_Content",insParent.GetChild(i).gameObject);
        }
        ChangeLevel();
        Equip_Content equipContent = MainObjectPool.Instance.SpawnFromPool("Equip_Content",Vector3.zero, Quaternion.identity).GetComponent<Equip_Content>();
        equipContent.transform.SetParent(insParent);
        equipContent.transform.localPosition = Vector3.zero;
        equipContent.transform.localScale = Vector3.one;
        equipContent.isOpenFromEquip = isOpenFromEquip;
        equipContent.isSlotIns = isSlotIns;
        equipContent.InitContent(curEquip);
        id = curEquip.id;
    }

    public void ChangeLevel()
    {
        strengthLevelText.text = "+" + strengthLevel;
    }

    public void NoEquipOnWear()
    {
        for (int i = 0; i < insParent.childCount; i++)
        {
            Destroy(insParent.GetChild(i).gameObject);
        }
    }
}
