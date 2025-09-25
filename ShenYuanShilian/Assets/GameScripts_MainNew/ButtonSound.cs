using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    void Start()
    {
        if ( this.GetComponent<Button>()!=null)
        {
            this.GetComponent<Button>().onClick.AddListener(PlayOnClick);
        }
    }

    private void OnDestroy()
    {
        if ( this.GetComponent<Button>()!=null)
        {
            this.GetComponent<Button>().onClick.RemoveListener(PlayOnClick);
        }
    }

    private void PlayOnClick()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
    }
    
}
