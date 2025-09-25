using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherInitTrigger : PassiveSkillTrigger
{
    public override void AddListener()
    {
        Trigger();
    }

    public override void RemoveListener()
    {
    }
}
