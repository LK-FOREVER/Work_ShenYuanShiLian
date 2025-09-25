using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendAdd_Controller : MonoBehaviour
{
    public GameObject friendPrefab;
    public GameObject instantiateParent;

    public Button allAddButton;
    public Button refreshButton;

    private void Start()
    {
        if (instantiateParent.transform.childCount == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                InsRandomFriendAdd();
            }
        }
        allAddButton.onClick.AddListener(AllAdd);
        refreshButton.onClick.AddListener(RefreshFriends);
    }

    private void OnDestroy()
    {
        refreshButton.onClick.RemoveListener(RefreshFriends);
        allAddButton.onClick.AddListener(AllAdd);
    }
    
    private void AllAdd()
    { 
        int addNum = Mvc.GetModel<GameModel>().FriendLimit - Mvc.GetModel<GameModel>().FriendList.Count;
        if (addNum < 10)
        {
            EventManager.Instance.TriggerEvent(EventName.ShowCommonTips,null,new ShowCommonTips()
            {
                tipsContent = "好友已满！"
            });
        }
        addNum = addNum > 10 ? 10 : addNum;
        for (int i = 0; i < addNum; i++)
        {
            instantiateParent.transform.GetChild(i).GetComponent<FriendContent_View>().friendAddContentView.AddThisFriend();
        }
    }

    public void RefreshFriends()
    {
        foreach (Transform child in instantiateParent.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < 10; i++)
        {
            InsRandomFriendAdd();
        }
    }

    private void InsRandomFriendAdd()
    {
        GameObject newFriend = Instantiate(friendPrefab, instantiateParent.transform);
        FriendClass friendClass = new FriendClass();
        int frontNameIndex = UnityEngine.Random.Range(0,DataManager.Instance.names[0].Name1.Count);
        string frontName = DataManager.Instance.names[0].Name1[frontNameIndex].name;
        int lastNameIndex = UnityEngine.Random.Range(0,DataManager.Instance.names[1].Name2.Count);
        string lastName = DataManager.Instance.names[1].Name2[lastNameIndex].name;
        string name = frontName + lastName;
        int headIconIndex = UnityEngine.Random.Range(0,3);
        friendClass.headIconIndex = headIconIndex;
        friendClass.name = name;
        friendClass.level = UnityEngine.Random.Range(1, 201);
        friendClass.isOnline = UnityEngine.Random.Range(0, 2) == 0;
        friendClass.challenge = UnityEngine.Random.Range(0,100);
        friendClass.levelText = $"{UnityEngine.Random.Range(1,5)+"-"+UnityEngine.Random.Range(1,21)}";
        Mvc.GetModel<GameModel>().FriendID++;
        friendClass.friendID = Mvc.GetModel<GameModel>().FriendID;
        newFriend.GetComponent<FriendContent_View>().CurrentFriend = friendClass;
        newFriend.GetComponent<FriendContent_View>().ActiveView(2);
    }
}
