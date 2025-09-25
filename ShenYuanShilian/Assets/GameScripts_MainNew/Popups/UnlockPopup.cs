using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockPopup : View
{
   public override string Name { get; }
   public override void HandleEvent(object data = null)
   {
      throw new System.NotImplementedException();
   }
   

   private int curIndex;
   
   public void Show(int characterIndex)
   {
      gameObject.SetActive(true);
      curIndex = characterIndex;
   }
   
   public void Unlock()
   {
      if (GetModel<GameModel>().Diamond >= 2000)
      {
         GetModel<GameModel>().UnlockCharacter[curIndex] = 1;
         GetModel<GameModel>().Diamond -= 2000;
         EventManager.Instance.TriggerEvent(EventName.RefreshCharacters);
      }
      else
      {
         EventManager.Instance.TriggerEvent(EventName.ShowCommonTips,null,new ShowCommonTips()
         {
            tipsContent = "钻石不足！"
         });
      }
   }
}
