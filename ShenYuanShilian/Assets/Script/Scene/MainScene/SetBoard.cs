using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetBoard : View
{
    public Slider musicSlider;
    public Slider soundSlider;
    public override string Name
    {
        get
        {
            return Consts.V_SettingBoard;
        }
    }

    private void OnEnable()
    {
        musicSlider.value = GetModel<GameModel>().MusicVolume;
        soundSlider.value = GetModel<GameModel>().SoundVolume;
    }

    public void OnMusicSliderChangeValue()
    {
        GetModel<GameModel>().MusicVolume = musicSlider.value;
    }

    public void OnSoundSliderChangeValue()
    {
        GetModel<GameModel>().SoundVolume = soundSlider.value;
    }

    public void OnBtnClose()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        gameObject.SetActive(false);
        SendViewEvent(Consts.E_SaveData);
    }

    public override void RegisterViewEvent()
    {
    }

    public override void HandleEvent(object data = null)
    {
    }

    public void Exit()
    {
        GameManager.Instance.ChangeScene(Scene.Loading);
    }
}
