using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
public class ChallengeBoard : View
{
    [SerializeField]
    private Toggle[] toggles;
    [SerializeField]
    private GameObject[] pages;
    [SerializeField]
    private Text damageTxt;
    [SerializeField]
    private GameObject reward_content;
    [SerializeField]
    private GameObject challengeRewardItemPrefab;

    private List<ChallengeRanklistInfo> challengeRanklistInfo = new();
    private List<ChallengeRewardInfo> challengeRewardInfo = new();
    public override string Name
    {
        get
        {
            return Consts.V_ChallengeBoard;
        }
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if (toggles == null || pages == null) return;
        if (toggles.Length != pages.Length) return;

        challengeRanklistInfo = DataManager.Instance.challengeRanklistInfoDic.Values.ToList();
        challengeRewardInfo = DataManager.Instance.challengeRewardInfoDic.Values.ToList();

        for (int i = 0; i < toggles.Length; i++)
        {
            int index = i;
            toggles[i].onValueChanged.AddListener((isOn) =>
            {
                if (isOn)
                {
                    ShowPage(index);
                }
            });
        }

        //初始化显示第一页
        ShowPage(0);
    }
    private void ShowPage(int index)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == index);
        }
        if (index == 0)
        {
            InitFirstPage();
        }
        else if (index == 1)
        {
            InitSecondPage();
        }
    }

    //挑战页面
    private void InitFirstPage()
    {
        damageTxt.text = $"最高造成的伤害：<color=#BA451A>{GetModel<GameModel>().LeaderChallengeDamage}</color>";
    }
    //奖励页面
    private void InitSecondPage()
    {
        // 清空reward_content下的所有元素
        foreach (Transform child in reward_content.transform)
        {
            Destroy(child.gameObject);
        }
        // 展示奖励
        for (int i = 0; i < challengeRewardInfo.Count; i++)
        {
            GameObject rewardItem = Instantiate(challengeRewardItemPrefab);
            rewardItem.GetComponent<ChallengeRewardItemController>().Init(challengeRewardInfo[i]);
            rewardItem.transform.SetParent(reward_content.transform, false);
        }
    }

    //开始挑战按钮
    public void OnStartChallenge()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        GetModel<GameModel>().LevelType = 1;
        // SendViewEvent(Consts.E_ChangeCurLevel, new ChangeCurLevel() { curLevel = 1});
        Utils.FadeIn();
        GameManager.Instance.ChangeScene(Scene.Battle);
    }
    public void OnBtnClose()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        gameObject.SetActive(false);
        SendViewEvent(Consts.E_SaveData);
    }
    public override void RegisterViewEvent()
    {
    }

    public override void HandleEvent(object data = null)
    {
    }
}