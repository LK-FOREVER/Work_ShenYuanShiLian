using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshEquipButton : MonoBehaviour
{
    public EquipType thisType;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SendRefreshEvent);
    }

    private void OnDestroy()
    {
        GetComponent<Button>().onClick.RemoveListener(SendRefreshEvent);
    }

    private void SendRefreshEvent()
    {
        EventManager.Instance.TriggerEvent(EventName.RefreshEquip,null,new RefreshEquipArgs()
        {
           refreshType = thisType
        });
    }
}
