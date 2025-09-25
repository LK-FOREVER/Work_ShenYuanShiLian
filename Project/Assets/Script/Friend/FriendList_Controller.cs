using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendList_Controller : View
{
    public GameObject friendPrefab;
    public GameObject InstantiateParent;

    public Button getSendAllButton;
    public Button deleteFriend;

    public TextMeshProUGUI friendCount;

    private List<FriendClass> cacheList = new List<FriendClass>();
    
    private FriendClass cachedFriend;

    public override string Name
    {
        get
        {
            return Consts.FriendList_View;
        }
    }

    private void OnEnable()
    {
        RefreshFriendList();
    }

    private void Start()
    {
        getSendAllButton.onClick.AddListener(GetAllMoney);
        EventManager.Instance.AddListener(EventName.SelectFriend,SelectFriendEvent);
    }

    private void SelectFriendEvent(object sender, EventArgs e)
    {
        SelectFriend friendEvent = e as SelectFriend;
        for (int i = 0; i < InstantiateParent.transform.childCount; i++)
        {
            FriendContent_View searchFriend = InstantiateParent.transform.GetChild(i).GetComponent<FriendContent_View>();
            searchFriend.friendListContentView.chooseObj.SetActive(false);
            FriendClass friend = searchFriend.currentFriend;
            if (friend.friendID == friendEvent.curFriend.friendID)
            {
                searchFriend.friendListContentView.chooseObj.SetActive(true);
                cachedFriend = friend;
            }
        }
    }

    public void DeleteFriend()
    {
        var friendToRemove =  GetModel<GameModel>().FriendList.FirstOrDefault(f => f.friendID == cachedFriend.friendID);
        if (friendToRemove != null)
        {
            GetModel<GameModel>().FriendList.Remove(friendToRemove);
        }
        RefreshFriendList();
    }

    private void OnDestroy()
    {
        getSendAllButton.onClick.RemoveListener(GetAllMoney);
    }

    public override void RegisterViewEvent()
    {
        base.RegisterViewEvent();
        attractEventList.Add(Consts.E_UpdateFriends);
    }
    
    public override void HandleEvent(object data = null)
    {
        RefreshFriendList();
    }

    public void RefreshFriendList()
    {
        foreach (Transform child in InstantiateParent.transform)
        {
            child.gameObject.GetComponent<FriendContent_View>().ActiveView(0);
        }
        friendCount.text = GetModel<GameModel>().FriendList.Count + "/" + GetModel<GameModel>().FriendLimit;
        if (InstantiateParent.transform.childCount == GetModel<GameModel>().FriendList.Count)
        {
            return;
        }
        foreach (Transform child in InstantiateParent.transform)
        {
            Destroy(child.gameObject);
        }
        // Sort friends: online and lower ID first, then offline and lower ID
        var sortedFriends = GetModel<GameModel>().FriendList.OrderBy(f => f.isOnline ? 0 : 1).ThenBy(f => f.friendID).ToList();
        foreach (var friend in sortedFriends)
        {
            GameObject friendObj = Instantiate(friendPrefab, InstantiateParent.transform, true);
            friendObj.transform.localPosition = new Vector3(0, 0, 0);
            friendObj.transform.localScale = new Vector3(1, 1, 1);
            FriendContent_View friendContentView = friendObj.GetComponent<FriendContent_View>();
            friendObj.GetComponent<FriendContent_View>().CurrentFriend = friend;
            friendObj.GetComponent<FriendContent_View>().friendListContentView.ButtonActive(friendContentView.friendListContentView.getMoneyButton,!friend.alreadyGetMoney);
            friendObj.GetComponent<FriendContent_View>().friendListContentView.ButtonActive(friendContentView.friendListContentView.sendMoneyButton,!friend.alreadySendMoney);
        }
    }

    private void GetAllMoney()
    {
        int totalCoin = 0;
        foreach (Transform child in InstantiateParent.transform)
        {
            if (!child.gameObject.GetComponent<FriendContent_View>().currentFriend.alreadyGetMoney)
            {
                totalCoin += 100;
                child.gameObject.GetComponent<FriendContent_View>().friendListContentView.GetMoney();
            }
            if (!child.gameObject.GetComponent<FriendContent_View>().currentFriend.alreadySendMoney)
            {
                child.gameObject.GetComponent<FriendContent_View>().friendListContentView.SendMoney(true);
            }
        }

        if (totalCoin == 0)
        {
            return;
        }
        EventManager.Instance.TriggerEvent(EventName.ShowCommonAward,null,new SetAward()
        {
            awardList = new List<AwardInfo>()
            {
                new AwardInfo()
                {
                    awardType = RewardType.Coin,
                    awardNum = totalCoin
                },
            }
        });
    }
}
