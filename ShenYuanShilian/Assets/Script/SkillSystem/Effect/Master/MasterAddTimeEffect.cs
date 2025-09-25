using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterAddTimeEffect : IEffect
{
    public void Execute(GameObject go, GameObject[] targets, List<int> param)
    {
        this.TriggerEvent(EventName.AddBuffTime, new AddBuffTimeEventArgs() { time = param[0] });
    }

    public void EndSkill(GameObject go, GameObject[] targets, List<int> param)
    {
    }
}
