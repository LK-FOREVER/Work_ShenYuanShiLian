using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step20 : GuidanceStateBase
{
    public override void OnComplete()
    {
        Mvc.GetModel<GameModel>().FinishGuidance = true;
        base.OnComplete();
    }
}
