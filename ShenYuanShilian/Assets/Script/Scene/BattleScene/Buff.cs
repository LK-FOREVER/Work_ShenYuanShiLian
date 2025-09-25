using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    SkillDeployer skillDeployer = new();
    public void Init(SkillDeployer _skillDeployer)
    {
        skillDeployer = _skillDeployer;
    }

    private void OnDestroy()
    {
        Destroy(skillDeployer);
    }
}
