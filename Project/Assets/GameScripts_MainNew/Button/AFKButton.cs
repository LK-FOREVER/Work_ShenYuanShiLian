using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AFKButton : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private void Update()
    {
        if (GameManager.Instance.AFKSecond > 43200)
        {
            timerText.text = "12:00:00";
        }
        else
        {
            timerText.text = TimeSpan.FromSeconds(GameManager.Instance.AFKSecond).ToString(@"hh\:mm\:ss");
        }
    }

}
