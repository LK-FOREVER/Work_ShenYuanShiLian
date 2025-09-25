using System.Collections;
using UnityEngine;
using TMPro;

public class TipBoard : MonoBehaviour
{
    public TMP_Text tip;
    public void InitAuto(string str)
    {
        StopCoroutine(Close());
        tip.text = str;
        StartCoroutine(Close());
    }

    public void Init(string str)
    {
        tip.text = str;
    }

    private IEnumerator Close()
    {
        yield return new WaitForSecondsRealtime(1f);
        gameObject.SetActive(false);
    }

    public void OnBtnClose()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        gameObject.SetActive(false);
    }
}
