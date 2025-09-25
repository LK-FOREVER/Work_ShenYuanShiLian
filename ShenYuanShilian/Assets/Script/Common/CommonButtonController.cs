using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommonButtonController : MonoBehaviour
{
    public CommonButton[] buttons;

    private void Start()
    {
        EventManager.Instance.AddListener(EventName.CommonButtonClick,OnClick);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveListener(EventName.CommonButtonClick,OnClick);
    }

    private void OnClick(object sender, EventArgs e)
    {
        CommonClick clickEvent = e as CommonClick;
        CommonButton senderButton = clickEvent.CommonButton;
        if (buttons.Contains(senderButton))
        {
            foreach (var button in buttons)
            {
                if (button!=senderButton)
                {
                    button.Init();
                }
            }
        }
    }

    public void InitClick()
    {
        buttons[0].OnClick();
    }

}
