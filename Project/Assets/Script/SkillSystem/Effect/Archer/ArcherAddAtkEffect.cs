using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAddAtkEffect : IEffect
{
    public void Execute(GameObject go, GameObject[] targets, List<int> param)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].GetComponent<Archer>().AddAtk(param[0]);
        }
    }

    public void EndSkill(GameObject go, GameObject[] targets, List<int> param)
    {
    }
}
