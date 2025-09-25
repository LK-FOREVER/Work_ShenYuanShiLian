using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonRestoreEffect : IEffect
{
    Sequence mySequence = null;
    public void Execute(GameObject go, GameObject[] targets, List<int> param)
    {
        mySequence = DOTween.Sequence();
        mySequence.AppendInterval(param[1]);
        mySequence.AppendCallback(() =>
        {
            this.TriggerEvent(EventName.RestoreHp, new RestoreHpEventArgs() { percent = param[0] });
        });
        mySequence.SetLoops(-1);
    }

    public void EndSkill(GameObject go, GameObject[] targets, List<int> param)
    {
        mySequence.Kill();
    }
}
