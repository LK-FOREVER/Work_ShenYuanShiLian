using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonFreezeEffect : IEffect
{
    public void Execute(GameObject go, GameObject[] targets, List<int> param)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].GetComponent<Monster>().IsFrozen = true;
        }
    }

    public void EndSkill(GameObject go, GameObject[] targets, List<int> param)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].GetComponent<Monster>().IsFrozen = false;
        }
    }
}
