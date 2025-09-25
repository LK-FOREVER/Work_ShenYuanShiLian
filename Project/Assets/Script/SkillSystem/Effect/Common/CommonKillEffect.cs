using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonKillEffect : IEffect
{
    public void Execute(GameObject go, GameObject[] targets, List<int> param)
    {
        float randomValue = Random.Range(0, 100);

        if (randomValue <= param[0])
        {
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].GetComponent<Monster>().Kill();
            }
        }
    }

    public void EndSkill(GameObject go, GameObject[] targets, List<int> param)
    {
    }
}
