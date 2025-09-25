using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseBoard : MonoBehaviour
{
    [SerializeField]
    private GameObject setBoard;

    public void OnBtnBack()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        Time.timeScale = 1;
        gameObject.SetActive(false);
        this.TriggerEvent(EventName.ChangePauseButtonSprites, new ChangePauseButtonSpritesEventArgs() { isPause = false });
    }

    public void OnBtnRestart()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        Time.timeScale = 1;
        Utils.FadeIn();
        GameManager.Instance.ChangeScene(Scene.Battle);
        this.TriggerEvent(EventName.ChangePauseButtonSprites, new ChangePauseButtonSpritesEventArgs() { isPause = false });
    }

    public void OnBtnBackMainScene()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        Time.timeScale = 1;
        Utils.FadeIn();
        GameManager.Instance.ChangeScene(Scene.Main);
    }

    public void OnBtnSet()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        setBoard.SetActive(true);
    }
}
