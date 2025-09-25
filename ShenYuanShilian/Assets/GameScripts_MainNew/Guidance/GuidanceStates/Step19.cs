using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step19 : GuidanceStateBase
{
   public override void OnMethod()
   {
      Content.SetActive(false);
      NextStateButton.gameObject.SetActive(false);
      NextStateButton.onClick.RemoveListener(OnMethod);
   }
}
