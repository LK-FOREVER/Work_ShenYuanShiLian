using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonDropCoinEffect : IEffect
{
    public void Execute(GameObject go, GameObject[] targets, List<int> param)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].GetComponent<Monster>().DropCoin(param[0]);
        }
    }

    public void EndSkill(GameObject go, GameObject[] targets, List<int> param)
    {
    }
}
