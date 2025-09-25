using System;
using UnityEngine;

public class SoundManager : View
{
    public AudioSource musicSource;
    public AudioSource soundSource;
    public AudioSource anotherSoundSource;

    public AudioClip mainClip;
    public AudioClip battleClip;
    public AudioClip[] soundClip;

    public override string Name
    {
        get
        {
            return Consts.V_Music;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        EventManager.Instance.AddListener(EventName.ChangeScene, PlayMusic);
        EventManager.Instance.AddListener(EventName.PlaySound, PlaySound);
        EventManager.Instance.AddListener(EventName.PlayAnotherSound, PlayAnotherSound);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveListener(EventName.ChangeScene, PlayMusic);
        EventManager.Instance.RemoveListener(EventName.PlaySound, PlaySound);
        EventManager.Instance.RemoveListener(EventName.PlayAnotherSound, PlayAnotherSound);
    }

    private void PlayMusic(object sender, EventArgs e)
    {
        ChangeSceneEventArgs args = e as ChangeSceneEventArgs;
        musicSource.volume = GetModel<GameModel>().MusicVolume;
        if (args.index == Scene.Battle)
        {
            if (musicSource.clip != battleClip)
            {
                musicSource.Stop();
                musicSource.clip = battleClip;
                musicSource.loop = true;
                musicSource.volume = GetModel<GameModel>().MusicVolume;
                musicSource.Play();
            }
        }
        else
        {
            if (musicSource.clip != mainClip)
            {
                musicSource.Stop();
                musicSource.clip = mainClip;
                musicSource.loop = true;
                musicSource.volume = GetModel<GameModel>().MusicVolume;
                musicSource.Play();
            }
        }
    }

    private void PlaySound(object sender, EventArgs e)
    {
        soundSource.Stop();
        PlaySoundEventArgs args = e as PlaySoundEventArgs;
        soundSource.clip = soundClip[(int)args.index];
        soundSource.loop = false;
        soundSource.volume = GetModel<GameModel>().SoundVolume;
        soundSource.Play();
    }
    
    private void PlayAnotherSound(object sender, EventArgs e)
    {
        anotherSoundSource.Stop();
        PlaySoundEventArgs args = e as PlaySoundEventArgs;
        anotherSoundSource.clip = soundClip[(int)args.index];
        anotherSoundSource.loop = false;
        anotherSoundSource.volume = GetModel<GameModel>().SoundVolume;
        anotherSoundSource.Play();
    }

    public override void RegisterViewEvent()
    {
        base.RegisterViewEvent();
        attractEventList.Add(Consts.E_ChangeVolume);
    }

    public override void HandleEvent(object data = null)
    {
        musicSource.volume = GetModel<GameModel>().MusicVolume;
        soundSource.volume = GetModel<GameModel>().SoundVolume;
        anotherSoundSource.volume = GetModel<GameModel>().SoundVolume;
    }
}
