using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAddAtkEffect : IEffect
{
    public void Execute(GameObject go, GameObject[] targets, List<int> param)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].GetComponent<CharacterBase>().ChangeExtraAtk(param[0]);
        }
    }

    public void EndSkill(GameObject go, GameObject[] targets, List<int> param)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].GetComponent<CharacterBase>().ChangeExtraAtk(-param[0]);
        }
    }
}
