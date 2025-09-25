using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonAutoMonoBase<GameManager>
{
    private bool init = false;

    private const string LAST_MONTH_KEY = "LastMonth";
    private const string TOTAL_KEY = "Total";
    private int _currentMonth;
    private int _total;

    public int AFKSecond;

    private int AFKRewardCoin;
    private int AFKRewardStone;
    private int AFKRewardExp;
    private int AFKRewardDiamond;


    private void Start()
    {
        LoadData();
        GameManager.Instance.Init(); 
    }

    private void Init()
    {
        if (init) return;
        init = true;
        Application.targetFrameRate = 60;

        RegisterFramework();
        SceneManager.LoadSceneAsync((int)Scene.Loading);
        StartHalfHourEventCoroutine();
    }

    void RegisterFramework()
    {
        Mvc.RegisterView(GameObject.Find("Music").GetComponent<SoundManager>());

        //注册model
        Mvc.RegisterModel(new GameModel());

        //注册controller
        Mvc.RegisterController(Consts.E_ChooseCharacter, typeof(ChooseCharacterController));
        Mvc.RegisterController(Consts.E_UnlockCharacter, typeof(UnlockCharacterController));
        Mvc.RegisterController(Consts.E_Upgrade, typeof(UpgradeController));
        Mvc.RegisterController(Consts.E_ChangeCoin, typeof(ChangeCoinController));
        Mvc.RegisterController(Consts.E_UpdateFriends, typeof(UpdateFriendController));
        Mvc.RegisterController(Consts.E_ChangeExp, typeof(ChangeExpController));
        Mvc.RegisterController(Consts.E_SetEquipment, typeof(SetEquipmentController));
        Mvc.RegisterController(Consts.E_ChangeCurLevel, typeof(ChangeCurLevelController));
        Mvc.RegisterController(Consts.E_SaveData, typeof(SaveDataController));
    }

    public void ChangeScene(Scene scene)
    {
        ObjectPool.Instance.Clear();
        Mvc.GetModel<GameModel>().NextScene = scene;
        SceneManager.LoadScene((int)Scene.Change);
    }
    
    public void ExitGame(string str)
    {
        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        DateTime utcDateTime = epoch.AddSeconds(int.Parse(str));
        DateTime localDateTime = utcDateTime.ToLocalTime();
        int delayTime = (21 - localDateTime.Hour) * 60 * 60 + (0 - localDateTime.Minute) * 60 + (0 - localDateTime.Second);
        StartCoroutine(Exit(delayTime));
    }
    
    public void StartOnlineTimer()
    {
        StartCoroutine(RecordOnlineDuration());
        StartCoroutine(AFKTimer());
    }

    private IEnumerator RecordOnlineDuration()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            GlobalTaskCounter.Instance.AddDailyCount(DailyTask.OnlineTime);
        }
    }

    private void SetAFKData()
    {
        int a = (Mvc.GetModel<GameModel>().CurLevel - 1) / 20 + 1;
        AFKRewardCoin = DataManager.Instance.AFKInfoList.Find(info => info.chapter == (Mvc.GetModel<GameModel>().CurLevel - 1) / 20 + 1).coinPerMin;
        AFKRewardStone = DataManager.Instance.AFKInfoList.Find(info => info.chapter == (Mvc.GetModel<GameModel>().CurLevel - 1) / 20 + 1).stonePer30Min;
        AFKRewardExp = DataManager.Instance.AFKInfoList.Find(info => info.chapter == (Mvc.GetModel<GameModel>().CurLevel - 1) / 20 + 1).expPer30Min;
        AFKRewardDiamond = DataManager.Instance.AFKInfoList.Find(info => info.chapter == (Mvc.GetModel<GameModel>().CurLevel - 1) / 20 + 1).diamondPer60Min;
    }

    private IEnumerator AFKTimer()
    {
        SetAFKData();
        if ( Mvc.GetModel<GameModel>().LastAFKRewardTime ==default)
        {
            Mvc.GetModel<GameModel>().LastAFKRewardTime = DateTime.Now;
        }
        TimeSpan minutePassed = DateTime.Now - Mvc.GetModel<GameModel>().LastAFKRewardTime;
        AFKSecond = (int)minutePassed.TotalSeconds;
        int AFKMinute = 0;
        int cacheMinute = 0;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (AFKSecond <= 43200)
            {
                minutePassed = DateTime.Now - Mvc.GetModel<GameModel>().LastAFKRewardTime;
                AFKSecond = (int)minutePassed.TotalSeconds;
                AFKMinute = AFKSecond / 60;
                if (AFKMinute != cacheMinute && AFKMinute!=0)
                {
                    if (AFKMinute % 1 == 0)
                    {
                        Mvc.GetModel<GameModel>().CacheAFKCoin+=DataManager.Instance.AFKInfoList
                            .Find(info => info.chapter == (Mvc.GetModel<GameModel>().CurLevel - 1) / 20 + 1).coinPerMin;
                        cacheMinute = AFKMinute;
                    }
                    if (AFKMinute % 30 == 0)
                    {
                        Mvc.GetModel<GameModel>().CacheAFKStone+=DataManager.Instance.AFKInfoList
                            .Find(info => info.chapter == (Mvc.GetModel<GameModel>().CurLevel - 1) / 20 + 1).stonePer30Min;
                        Mvc.GetModel<GameModel>().CacheAFKExp+=DataManager.Instance.AFKInfoList
                            .Find(info => info.chapter == (Mvc.GetModel<GameModel>().CurLevel - 1) / 20 + 1).expPer30Min;
                        cacheMinute = AFKMinute;
                    }
                    if (AFKMinute % 60 == 0)
                    {
                        Mvc.GetModel<GameModel>().CacheAFKDiamond+=DataManager.Instance.AFKInfoList
                            .Find(info => info.chapter == (Mvc.GetModel<GameModel>().CurLevel - 1) / 20 + 1).diamondPer60Min;
                        cacheMinute = AFKMinute;
                    }
                }
            }
        }
    }

    public void CalculateOfflineReward()
    {
        SetAFKData();
        TimeSpan minutePassed = DateTime.Now - Mvc.GetModel<GameModel>().LastAFKRewardTime;
        int AFKMinute = (int)minutePassed.TotalMinutes;
        if (AFKMinute>=720)
        {
            AFKMinute = 720;//最多12小时
        }
        Mvc.GetModel<GameModel>().CacheAFKCoin = AFKRewardCoin * AFKMinute;
        Mvc.GetModel<GameModel>().CacheAFKStone = AFKRewardStone * (AFKMinute / 30);
        Mvc.GetModel<GameModel>().CacheAFKExp = AFKRewardExp * (AFKMinute / 30);
        Mvc.GetModel<GameModel>().CacheAFKDiamond= AFKRewardDiamond * (AFKMinute / 60);
    }

    public IEnumerator Exit(int time)
    {
        yield return new WaitForSecondsRealtime(time);
        string currentSceneName = SceneManager.GetActiveScene().name;
        if(currentSceneName == "Main")
        {
            GameObject.Find("Canvas").GetComponent<MainSceneManager>().OnShowWarn();
        }
        if (currentSceneName == "Battle")
        {
            GameObject.Find("Canvas").GetComponent<BattleSceneManager>().OnShowWarn();
        }
        if(currentSceneName == "Load")
        {
            GameObject.Find("Canvas").GetComponent<LoadingSceneManager>().OnShowWarn();
        }
        //Mvc.GetModel<GameModel>().Timeout = true;
    }

    private void StartHalfHourEventCoroutine()
    {
        StartCoroutine(HalfHourEventCoroutine());
    }

    private IEnumerator HalfHourEventCoroutine()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1800); // 30 minutes
            EventManager.Instance.TriggerEvent(EventName.InsFriendAdd);
            EventManager.Instance.TriggerEvent(EventName.UpdateFriendOnline);
        }
    }

    private void LoadData()
    {
        _currentMonth = DateTime.Now.Month;
        int lastMonth = PlayerPrefs.GetInt(LAST_MONTH_KEY, _currentMonth);
        _total = PlayerPrefs.GetInt(TOTAL_KEY, 0);

        // 跨月重置
        if (lastMonth != _currentMonth)
        {
            _total = 0;
            SaveData();
        }
    }
    public (bool isMonthlyExceed, bool isSingleExceed) CheckRecharge(int amount)
    {
        bool isMonthlyExceed = _total + amount > 400;
        bool isSingleExceed = amount > 100;
        return (isMonthlyExceed, isSingleExceed);
    }

    public void ApplyRecharge(int amount)
    {
        _total += amount;
        SaveData();
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(LAST_MONTH_KEY, _currentMonth);
        PlayerPrefs.SetInt(TOTAL_KEY, _total);
        PlayerPrefs.Save();
    }
}
