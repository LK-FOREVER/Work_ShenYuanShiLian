using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendApply_Controller : MonoBehaviour
{
    public GameObject friendPrefab;
    public GameObject InstantiateParent;
    public Button allApplyButton;
    public Button allRefusedButton;
    void Awake()
    {
        EventManager.Instance.AddListener(EventName.InsFriendAdd,InsRandomFriendAdd);
        allApplyButton.onClick.AddListener(AllApply);
        allRefusedButton.onClick.AddListener(AllRefused);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveListener(EventName.InsFriendAdd,InsRandomFriendAdd);
        allApplyButton.onClick.AddListener(AllApply);
        allRefusedButton.onClick.AddListener(AllRefused);
    }

    private void AllApply()
    {
        for (int i = 0; i < InstantiateParent.transform.childCount; i++)
        {
            InstantiateParent.transform.GetChild(i).GetComponent<FriendContent_View>().friendApplyContentView.AddThisFriend();
        }
    }
    
    private void AllRefused()
    {
        for (int i = 0; i < InstantiateParent.transform.childCount; i++)
        {
            InstantiateParent.transform.GetChild(i).GetComponent<FriendContent_View>().friendApplyContentView.RefuseThisFriend();
        }
    }

    private void InsRandomFriendAdd(object sender, EventArgs e)
    {
        GameObject newFriend = Instantiate(friendPrefab, InstantiateParent.transform);
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
        newFriend.GetComponent<FriendContent_View>().ActiveView(1);
    }
}
