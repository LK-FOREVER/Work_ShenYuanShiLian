using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonPoisonEffect : IEffect
{
    Sequence mySequence;
    public void Execute(GameObject go, GameObject[] targets, List<int> param)
    {
        mySequence.Kill();
        mySequence = DOTween.Sequence();
        mySequence.AppendInterval(1);
        mySequence.AppendCallback(() =>
        {
            this.TriggerEvent(EventName.Poison, new PoisonEventArgs() { pos = go.GetComponent<CharacterBase>().curPos,percent = param[0] });
        });
        mySequence.SetLoops(-1);
    }

    public void EndSkill(GameObject go, GameObject[] targets, List<int> param)
    {
        mySequence.Kill();
    }
}
