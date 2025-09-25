using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupsController : MonoBehaviour
{
    public GameObject awardPopup;
    public GameObject skillPopup;
    public GameObject equipPopup;
    public GameObject creatAccountPopup;
    public GameObject friendMessagePopup;
    
    // Start is called before the first frame update
    private void Awake()
    {
        EventManager.Instance.AddListener(EventName.ShowCommonAward,ShowAwardPopup);
        EventManager.Instance.AddListener(EventName.ShowSkill,ShowSkillPopup);
        EventManager.Instance.AddListener(EventName.ShowEquip,ShowEquipPopup);
        EventManager.Instance.AddListener(EventName.ShowCreateAccount,ShowCreateAccountPopup);
        EventManager.Instance.AddListener(EventName.SelectFriend,ShowFriendMessagePopup);
    }
    
    private void OnDestroy()
    {
        EventManager.Instance.RemoveListener(EventName.ShowCommonAward,ShowAwardPopup);
        EventManager.Instance.RemoveListener(EventName.ShowSkill,ShowSkillPopup);
        EventManager.Instance.RemoveListener(EventName.ShowEquip, ShowEquipPopup);
        EventManager.Instance.RemoveListener(EventName.ShowCreateAccount,ShowCreateAccountPopup);
        EventManager.Instance.RemoveListener(EventName.SelectFriend,ShowFriendMessagePopup);
    }

    private void ShowFriendMessagePopup(object sender, EventArgs e)
    {
        friendMessagePopup.SetActive(true);
        SelectFriend selectFriend = e as SelectFriend;
        friendMessagePopup.GetComponent<FriendMessagePopup>().Show(selectFriend.curFriend);
    }

    private void ShowEquipPopup(object sender, EventArgs e)
    {
        equipPopup.SetActive(true);
        ShowEquip equipEvent = e as ShowEquip;
        equipPopup.GetComponent<EquipPopup>().Show(equipEvent);
    }
    
    private void ShowSkillPopup(object sender, EventArgs e)
    {
        skillPopup.SetActive(true);
        ShowSkill skillEvent = e as ShowSkill;
        skillPopup.GetComponent<SkillPopup>().Show(skillEvent);
    }

    public void ShowAwardPopup(object sender, EventArgs e)
    {
        awardPopup.SetActive(true);
        SetAward awardEvent = e as SetAward;
        awardPopup.GetComponent<CommonAwardPopupView>().ShowAward(awardEvent);
    }
    
    private void ShowCreateAccountPopup(object sender, EventArgs e)
    {
        creatAccountPopup.SetActive(true);
    }
    
}
