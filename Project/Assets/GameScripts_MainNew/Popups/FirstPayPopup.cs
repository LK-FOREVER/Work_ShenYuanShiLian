using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FirstPayPopup : View
{
    
    public override string Name { get; }
    public override void HandleEvent(object data = null)
    {
        throw new System.NotImplementedException();
    }
    
    public void GetAward()
    {
        GetModel<GameModel>().FirstPay =true;
        gameObject.SetActive(false);
        EventManager.Instance.TriggerEvent(EventName.ShowCommonAward,null,new SetAward()
        {
            awardList = new List<AwardInfo>()
            {
                new AwardInfo()
                {
                    awardType = RewardType.Coin,
                    awardNum = 5000
                },
                new AwardInfo()
                {
                    awardType = RewardType.Stone,
                    awardNum = 300
                },
                new AwardInfo()
                {
                    awardType = RewardType.Equip,
                    awardEquip = DataManager.Instance.equipmentDic["Weapon"].Find(e => e.id == 6)
                }
            }
        });
    }


}
