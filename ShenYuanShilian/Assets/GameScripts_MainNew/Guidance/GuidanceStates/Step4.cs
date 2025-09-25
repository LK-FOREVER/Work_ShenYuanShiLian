using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step4 : GuidanceStateBase
{
    public MapView map;
    public override void OnMethod()
    {
        GuidanceStateManager.Instance.allStatesInScene.Clear();
        EventManager.Instance.TriggerEvent(EventName.GuidanceStepComplete,null,new ChangeStateArgs()
        {
            guidanceID = stepID,
            changeScene = true
        });
        map.OnBtnLevel(1);
    }
}
