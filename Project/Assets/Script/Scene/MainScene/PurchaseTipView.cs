using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseTipView : View
{
    public override string Name
    {
        get
        {
            return Consts.V_PurchaseTipView;
        }
    }

    private int id;

    public void init(int id)
    {
        this.id = id;
    }

    public void OnCancelBtn()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        gameObject.SetActive(false);
    }

    public void OnConfirmBtn()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        SendViewEvent(Consts.E_UnlockCharacter, new UnlockCharacher() { id = id });
        gameObject.SetActive(false);
    }

    public override void HandleEvent(object data = null)
    {

    }
}
