using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterRestoreMpEffect : MonoBehaviour
{
    public void Execute(GameObject go, GameObject[] targets, List<int> param)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].GetComponent<Master>().RestoreMp(param[0]);
        }
    }

    public void EndSkill(GameObject go, GameObject[] targets, List<int> param)
    {
    }
}
