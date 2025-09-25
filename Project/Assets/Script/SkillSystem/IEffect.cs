using System.Collections.Generic;
using UnityEngine;

public interface IEffect
{
    void Execute(GameObject go, GameObject[] targets, List<int> param);
    void EndSkill(GameObject go, GameObject[] targets, List<int> param);
}
