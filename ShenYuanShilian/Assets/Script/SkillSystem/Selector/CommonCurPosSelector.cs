using System;
using System.Collections.Generic;
using UnityEngine;

public class CommonCurPosSelector : ISelector
{
    public GameObject[] SelectTarget(GameObject go, int tag)
    {
        if (tag == 0) return new GameObject[] { go };

        List<GameObject> targets = new List<GameObject>();
        GameObject[] goArray = GameObject.FindGameObjectsWithTag(Convert.ToString((CommonBuffTargetTag)tag));
        targets.AddRange(goArray);
        targets = targets.FindAll(item => item.GetComponent<Entity>().id == go.GetComponent<CharacterBase>().curPos);
        return targets.ToArray();
    }
}
