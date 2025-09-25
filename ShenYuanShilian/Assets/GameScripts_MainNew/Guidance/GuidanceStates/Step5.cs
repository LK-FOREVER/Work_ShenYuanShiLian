using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step5 : GuidanceStateBase
{
   public override void OnEnter()
   {
      base.OnEnter();
      Time.timeScale = 0;
   }

   public override void OnComplete()
   {
      Time.timeScale = 1;
      StartCoroutine(WaitShow());
   }

   private IEnumerator WaitShow()
   {
      Content.SetActive(false);
      NextStateButton.gameObject.SetActive(false);
      NextStateButton.onClick.RemoveListener(OnMethod);
      yield return new WaitForSeconds(2f);
      EventManager.Instance.TriggerEvent(EventName.GuidanceStepComplete,null,new ChangeStateArgs()
      {
         guidanceID = stepID,
      });
   }
}
