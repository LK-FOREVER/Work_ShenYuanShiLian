using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step6 : GuidanceStateBase
{
    public BattleSceneManager BattleSceneManager;
    public override void OnEnter()
    {
        base.OnEnter();
        Time.timeScale = 0;
    }

    public override void OnMethod()
    {
        Content.SetActive(false);
        NextStateButton.gameObject.SetActive(false);
        NextStateButton.onClick.RemoveListener(OnMethod);
        Time.timeScale = 1;
        BattleSceneManager.OnClickSkill(0);
    }
    
}

