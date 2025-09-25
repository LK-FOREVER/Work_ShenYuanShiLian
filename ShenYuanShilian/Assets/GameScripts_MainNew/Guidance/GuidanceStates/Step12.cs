using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step12 : GuidanceStateBase
{
    public override void OnComplete()
    {
        Mvc.GetModel<GameModel>().WornEquipments[EquipType.Weapon] = Mvc.GetModel<GameModel>().OwnedEquipments[EquipType.Weapon][0];
        EventManager.Instance.TriggerEvent(EventName.RefreshEquip,null,new RefreshEquipArgs()
        {
            refreshType = EquipType.Weapon
        });
        base.OnComplete();
    }
}
