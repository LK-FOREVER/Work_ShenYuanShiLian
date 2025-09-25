using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterInitTrigger : PassiveSkillTrigger
{ 
    public override void AddListener()
    {
        Trigger();
    }

    public override void RemoveListener()
    {
    }
}
