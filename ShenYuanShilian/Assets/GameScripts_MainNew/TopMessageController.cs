using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopMessageController : MonoBehaviour
{
    public TextMeshProUGUI nickName;
    public TextMeshProUGUI level;
    public Image headIconImage;
    public Sprite[] iconImages;

    private void Start()
    {
        Init();
        EventManager.Instance.AddListener(EventName.RefreshTopMessage,Init);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveListener(EventName.RefreshTopMessage,Init);
    }

    private void Init(object sender, EventArgs e)
    {
        Init();
    }

    public void Init()
    {
        nickName.text = Mvc.GetModel<GameModel>().NickName;
        level.text = "等级"+Mvc.GetModel<GameModel>().PlayerLevel[Mvc.GetModel<GameModel>().CurId];
        headIconImage.sprite = iconImages[Mvc.GetModel<GameModel>().CurId];
    }
}
