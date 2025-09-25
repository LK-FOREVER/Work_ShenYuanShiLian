using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuccessBoard : View
{
    public override string Name
    {
        get
        {
            return Consts.V_SuccessBoard;
        }
    }

    public void OnBtnRestart()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        Time.timeScale = 1;
        GameManager.Instance.ChangeScene(Scene.Battle);
    }

    public void OnBtnBackMainScene()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        Time.timeScale = 1;
        Utils.FadeIn();
        GameManager.Instance.ChangeScene(Scene.Main);
    }

    public void OnBtnPass()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        Time.timeScale = 1;
        int curLevel = GetModel<GameModel>().CurLevel;
        SendViewEvent(Consts.E_ChangeCurLevel, new ChangeCurLevel() { curLevel = curLevel + 1 });
        Utils.FadeIn();
        GameManager.Instance.ChangeScene(Scene.Battle);
    }

    public override void HandleEvent(object data = null)
    {
        
    }
}
