using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendList_ContentView : View
{
    public GameObject onlineObj;
    public GameObject offlineObj;

    public Button getMoneyButton;
    public Button sendMoneyButton;
    
    public FriendClass currentFriend;

    public GameObject chooseObj;


    public override string Name
    {
        get
        {
            return Consts.V_FriendPopupView;
        }
    }
    public override void HandleEvent(object data = null)
    {
        
    }

    private void Start()
    {
        getMoneyButton.onClick.AddListener(GetMoney);
        getMoneyButton.onClick.AddListener(GetAward);
        sendMoneyButton.onClick.AddListener(SendMoney);
        if (!currentFriend.alreadyGetMoney)
        {
            ButtonActive(getMoneyButton, true);
        }
        
        if (!currentFriend.alreadySendMoney)
        {
            ButtonActive(sendMoneyButton, true);
        }
        
        onlineObj.SetActive(currentFriend.isOnline);
        offlineObj.SetActive(!currentFriend.isOnline);
    }

    private void OnDestroy()
    {
        getMoneyButton.onClick.RemoveListener(GetMoney);
        getMoneyButton.onClick.RemoveListener(GetAward);
        sendMoneyButton.onClick.RemoveListener(SendMoney);
    }
    
    public void GetMoney()
    {

        Mvc.GetModel<GameModel>().FriendList.Find(f => f.friendID == currentFriend.friendID)
            .alreadyGetMoney = true;
        ButtonActive(getMoneyButton, false);
    }

    private void GetAward()
    {
        EventManager.Instance.TriggerEvent(EventName.ShowCommonAward,null,new SetAward()
        {
            awardList = new List<AwardInfo>()
            {
                new AwardInfo()
                {
                    awardType = RewardType.Coin,
                    awardNum = 100
                },
            }
        });
    }
    
    private void SendMoney()
    {
        SendMoney(false);
    }

    public void SendMoney(bool isAll)
    {
        Mvc.GetModel<GameModel>().FriendList.Find(f => f.friendID == currentFriend.friendID)
            .alreadySendMoney = true;
        ButtonActive(sendMoneyButton, false);
        GlobalTaskCounter.Instance.AddDailyCount(DailyTask.SendFriendMoney);
        GlobalTaskCounter.Instance.AddWeeklyCount(WeeklyTask.SendFriendMoney);
        if (!isAll)
        {
            EventManager.Instance.TriggerEvent(EventName.ShowCommonTips,null,new ShowCommonTips()
            {
                tipsContent = "赠送成功！"
            });
        }
    }

    public void ButtonActive(Button button, bool active)
    {
        if (active)
        {
            button.interactable = true;
            button.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        else
        {
            button.interactable = false;
            button.GetComponent<Image>().color = new Color(128, 128, 128, 255);
        }
    }

    public void Choose()
    {
        chooseObj.SetActive(true);
        EventManager.Instance.TriggerEvent(EventName.SelectFriend,null,new SelectFriend()
        {
            curFriend = currentFriend
        });
    }


}
