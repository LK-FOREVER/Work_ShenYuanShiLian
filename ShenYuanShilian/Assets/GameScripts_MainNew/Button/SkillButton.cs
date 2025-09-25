using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
    public void ShowSkill(string skillName)
    {
        EventManager.Instance.TriggerEvent(EventName.ShowSkill,null,new ShowSkill()
        {
            skillName = skillName
        });
    }
}
