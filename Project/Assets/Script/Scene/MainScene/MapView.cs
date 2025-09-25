using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapView : View
{
    public override string Name
    {
        get
        {
            return Consts.V_MapView;
        }
    }

    [SerializeField]
    private GameObject[] btnLevels;//关卡按钮
    [SerializeField]
    private Sprite lockedMapArrow;//未解锁的路线箭头状态
    [SerializeField]
    private Sprite unlockedMapArrow;//已解锁的路线箭头状态
    [SerializeField]
    private Sprite completedBtn;// 已完成关卡按钮状态
    [SerializeField]
    private Sprite inCompletedBtn;// 已解锁但未完成的关卡按钮状态
    [SerializeField]
    private Sprite lockedBtn;// 未解锁关卡按钮状态

    private LevelType levelType;
    private int levelCount;
    private AsyncOperation asyncLoad;
    protected void Awake()
    {
        GetModel<GameModel>().LevelType = 0;
        levelType = (LevelType)GetModel<GameModel>().LevelType;
        levelCount = GetModel<GameModel>().LevelCount;
        // 关卡总数
        int allLevelNum = DataManager.Instance.levelMonsterInfo[1].levels.Count
                        + DataManager.Instance.levelMonsterInfo[2].levels.Count
                        + DataManager.Instance.levelMonsterInfo[3].levels.Count
                        + DataManager.Instance.levelMonsterInfo[4].levels.Count;

        for (int i = 0; i < allLevelNum; i++)
        {
            SetBtnLevelState(i);
        }
        if (levelCount <= allLevelNum)
        {
            btnLevels[levelCount - 1].GetComponent<Image>().sprite = inCompletedBtn;
        }
        // btnEndlessLevel.GetComponent<Image>().sprite = levelType == LevelType.Endless ? unlocked : locked;
    }

    private void SetBtnLevelState(int i)
    {
        if (levelType == LevelType.Normal)
        {
            btnLevels[i].GetComponent<Image>().sprite = i < levelCount - 1 ? completedBtn : lockedBtn;
            btnLevels[i].transform.Find("Text").GetComponent<Text>().text = $"第{(i + 1).ToString()}关";
            btnLevels[i].transform.Find("mapLockArrow").GetComponent<Image>().sprite = i + 1 < levelCount ? unlockedMapArrow : lockedMapArrow;
        }
        else
        {
            // btnLevels[i].GetComponent<Image>().sprite = unlocked;
        }
    }

    public void OnBtnLevel(int level)
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        if (GetModel<GameModel>().LevelType == 0 && level > GetModel<GameModel>().LevelCount) return;
        SendViewEvent(Consts.E_ChangeCurLevel, new ChangeCurLevel() { curLevel = level });
        Utils.FadeIn();
        GameManager.Instance.ChangeScene(Scene.Battle);
    }

    public override void HandleEvent(object data = null)
    {
    }
}
