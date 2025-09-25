using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step7 : GuidanceStateBase
{
    public override void OnMethod()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        Time.timeScale = 1;
        Utils.FadeIn();
        GameManager.Instance.ChangeScene(Scene.Main);
        base.OnMethod();
    }
    
    public override void OnComplete()
    {
        Content.SetActive(false);
        NextStateButton.gameObject.SetActive(false);
        NextStateButton.onClick.RemoveListener(OnMethod);
        EventManager.Instance.TriggerEvent(EventName.GuidanceStepComplete,null,new ChangeStateArgs()
        {
            guidanceID = stepID,
            changeScene = true
        });
    }
}
