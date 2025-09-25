using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class DeadBoard : View
{
    public override string Name
    {
        get
        {
            return Consts.V_DeadBoard;
        }
    }

    public GameObject tip;
    private float countDown = 10;
    public TMP_Text countDownText;
    private int cost = 50;
    public TMP_Text costText;
    public GameObject failBoard;

    private void OnEnable()
    {
        countDown = 10;
        countDownText.text = countDown.ToString();
        costText.text = $"钻石：{cost}";
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        while (countDown > 0)
        {
            yield return new WaitForSecondsRealtime(1f);
            countDown--;
            countDownText.text = countDown.ToString();
            if (countDown == 0)
            {
                this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Fail });
                failBoard.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }

    public void OnBtnClose()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Fail });
        failBoard.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnBtnResurge()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        if (GetModel<GameModel>().Diamond < cost)
        {
            SendMsg("钻石不足。");
            return;
        }
        Time.timeScale = 1;
        StopCoroutine(CountDown());
        GetModel<GameModel>().Diamond -= cost;
        cost += 50;
        costText.text = $"钻石：{cost}";
        this.TriggerEvent(EventName.CharacterResurge);
        gameObject.SetActive(false);
    }

    private void SendMsg(string str)
    {
        tip.SetActive(true);
        tip.GetComponent<TipBoard>().InitAuto(str);
    }

    public override void HandleEvent(object data = null)
    {
    }
}
