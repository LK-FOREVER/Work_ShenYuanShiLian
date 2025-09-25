using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendApply_ContentView : View
{
    
    public override string Name
    {
        get
        {
            return Consts.FriendApply_ContentView;
        }
    }
    
    public FriendClass currentFriend;
    
    public Button ApplyButton;
    public Button RefuseButton;

    private void Start()
    {
        Init();
    }

    private void OnDestroy()
    {
        ApplyButton.onClick.RemoveListener(AddThisFriend);
    }

    public void Init()
    {
        RegisterButton();
    }
    
    

    private void RegisterButton()
    {
        RefuseButton.onClick.AddListener(RefuseThisFriend);
        ApplyButton.onClick.AddListener(AddThisFriend);
    }
    
    public void AddThisFriend()
    {
        List<FriendClass> friendList = GetModel<GameModel>().FriendList;
        friendList.Add(currentFriend);
        SendViewEvent(Consts.E_UpdateFriends, new UpdateFriends { friendList = friendList });
        Destroy(this.gameObject.transform.parent.gameObject);
    }

    public void RefuseThisFriend()
    {
        Destroy(this.gameObject.transform.parent.gameObject);
    }


    public override void HandleEvent(object data = null)
    {
        
    }
}
