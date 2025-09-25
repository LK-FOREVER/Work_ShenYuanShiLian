using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
public class RankListBoard : View
{
    [SerializeField]
    private Toggle[] toggles;
    [SerializeField]
    private GameObject[] pages;
    [SerializeField]
    private GameObject normal_rank_content;
    [SerializeField]
    private GameObject challenge_rank_content;
    [SerializeField]
    private GameObject challengeRanklistItemPrefab;
    [SerializeField]
    private GameObject normalRanklistItemPrefab;

    private List<NormalRanklistInfo> normalRanklistInfo = new();

    private List<ChallengeRanklistInfo> challengeRanklistInfo = new();
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
        normalRanklistInfo = DataManager.Instance.normalRanklistInfoDic.Values.ToList();
        challengeRanklistInfo = DataManager.Instance.challengeRanklistInfoDic.Values.ToList();

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

    //冒险挑战排行榜
    private void InitFirstPage()
    {
        // 玩家排名
        int play_rank_num = 0;

        // 先清除本玩家信息
        for (int i = 0; i < normalRanklistInfo.Count; i++)
        {
            if (normalRanklistInfo[i].player_id == 0)
            {
                // 先将 所要删除玩家 位置之后的 其他玩家id减1
                for (int j = i + 1; j < normalRanklistInfo.Count; j++)
                {
                    normalRanklistInfo[j].id--;
                }
                // 删除该玩家信息
                normalRanklistInfo.RemoveAt(i);
                break;
            }
        }

        for (int i = 0; i < normalRanklistInfo.Count; i++)
        {
            if (GetModel<GameModel>().LevelCount >= normalRanklistInfo[i].normal_level)
            {
                // 更新玩家排名
                play_rank_num = i + 1;
                break;
            }
            else
            {
                // 玩家排名大于所有玩家
                play_rank_num = normalRanklistInfo.Count + 1;
            }
        }

        //将玩家的信息添加到normalRanklistInfo中
        NormalRanklistInfo player_info = new NormalRanklistInfo()
        {
            id = play_rank_num - 1,
            player_id = 0,
            icon = GetModel<GameModel>().CurId,
            name = GetModel<GameModel>().NickName,
            level = GetModel<GameModel>().PlayerLevel[GetModel<GameModel>().CurId],
            normal_level = GetModel<GameModel>().LevelCount,
            is_player = true
        };
        // 插入新的玩家信息
        normalRanklistInfo.Insert(play_rank_num - 1, player_info);
        // 将插入位置之后的其他玩家的id加1
        for (int i = play_rank_num; i < normalRanklistInfo.Count; i++)
        {
            normalRanklistInfo[i].id += 1;
        }

        // 清空rank_content下的所有元素
        foreach (Transform child in normal_rank_content.transform)
        {
            Destroy(child.gameObject);
        }
        //展示排行榜
        for (int i = 0; i < normalRanklistInfo.Count; i++)
        {
            GameObject rankItem = Instantiate(normalRanklistItemPrefab);
            rankItem.GetComponent<NormalRankItemController>().Init(normalRanklistInfo[i]);


            rankItem.transform.SetParent(normal_rank_content.transform, false);
        }
    }
    //领袖挑战排行榜
    private void InitSecondPage()
    {
        // 玩家排名
        int play_rank_num = 0;

        // 先清除本玩家信息
        for (int i = 0; i < challengeRanklistInfo.Count; i++)
        {
            if (challengeRanklistInfo[i].player_id == 0)
            {
                // 先将 所要删除玩家 位置之后的 其他玩家id减1
                for (int j = i + 1; j < challengeRanklistInfo.Count; j++)
                {
                    challengeRanklistInfo[j].id--;
                }
                // 删除该玩家信息
                challengeRanklistInfo.RemoveAt(i);
                break;
            }
        }

        for (int i = 0; i < challengeRanklistInfo.Count; i++)
        {
            if (GetModel<GameModel>().LeaderChallengeDamage >= challengeRanklistInfo[i].damage)
            {
                // 更新玩家排名
                play_rank_num = i + 1;
                break;
            }
            else
            {
                // 玩家排名大于所有玩家
                play_rank_num = challengeRanklistInfo.Count + 1;
            }
        }

        //将玩家的信息添加到challengeRanklistInfo中
        ChallengeRanklistInfo player_info = new ChallengeRanklistInfo()
        {
            id = play_rank_num - 1,
            player_id = 0,
            icon = GetModel<GameModel>().CurId,
            name = GetModel<GameModel>().NickName,
            level = GetModel<GameModel>().PlayerLevel[GetModel<GameModel>().CurId],
            damage = GetModel<GameModel>().LeaderChallengeDamage,
            is_player = true
        };
        // 插入新的玩家信息
        challengeRanklistInfo.Insert(play_rank_num - 1, player_info);
        // 将插入位置之后的其他玩家的id加1
        for (int i = play_rank_num; i < challengeRanklistInfo.Count; i++)
        {
            challengeRanklistInfo[i].id += 1;
        }

        // 清空rank_content下的所有元素
        foreach (Transform child in challenge_rank_content.transform)
        {
            Destroy(child.gameObject);
        }
        //展示排行榜
        for (int i = 0; i < challengeRanklistInfo.Count; i++)
        {
            GameObject rankItem = Instantiate(challengeRanklistItemPrefab);
            rankItem.GetComponent<ChallengeRankItemController>().Init(challengeRanklistInfo[i]);

            rankItem.transform.SetParent(challenge_rank_content.transform, false);
        }
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
