using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendAdd_ContentView : View
{
    public FriendClass currentFriend;

    public Button addButton;

    public GameObject alreadyAdded;
    
    public override string Name
    {
        get
        {
            return Consts.FriendAdd_ContentView;
        }
    }

    private void Start()
    {
        addButton.onClick.AddListener(AddThisFriend);
        Init();
    }

    private void OnDestroy()
    {
        addButton.onClick.RemoveListener(AddThisFriend);
    }

    public void Init()
    {
        bool haveThisFriend = GetModel<GameModel>().FriendList.Contains(currentFriend);
        addButton.gameObject.SetActive(!haveThisFriend);
        alreadyAdded.SetActive(haveThisFriend);
    }
    
    public override void HandleEvent(object data = null)
    {
        
    }
    
    public void AddThisFriend()
    {
        if (Mvc.GetModel<GameModel>().FriendList.Count + 1 >  Mvc.GetModel<GameModel>().FriendLimit)
        {
            EventManager.Instance.TriggerEvent(EventName.ShowCommonTips,null,new ShowCommonTips()
            {
                tipsContent = "好友已满！"
            });
            return;
        }

        addButton.gameObject.SetActive(false);
        alreadyAdded.SetActive(true);
        List<FriendClass> friendList = GetModel<GameModel>().FriendList;
        friendList.Add(currentFriend);
        SendViewEvent(Consts.E_UpdateFriends, new UpdateFriends { friendList = friendList });
    }

}
