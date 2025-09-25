using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMissTrigger : PassiveSkillTrigger
{
    public override void AddListener()
    {
        EventManager.Instance.AddListener(EventName.Miss, Trigger);
    }

    public override void RemoveListener()
    {
        EventManager.Instance.RemoveListener(EventName.Miss, Trigger);
    }
}
