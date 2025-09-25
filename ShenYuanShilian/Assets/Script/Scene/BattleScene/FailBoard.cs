using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailBoard : MonoBehaviour
{
    public void OnBtnRestart()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        Time.timeScale = 1;
        Utils.FadeIn();
        GameManager.Instance.ChangeScene(Scene.Battle);
    }

    public void OnBtnBackMainScene()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        Time.timeScale = 1;
        Utils.FadeIn();
        GameManager.Instance.ChangeScene(Scene.Main);
    }
}
