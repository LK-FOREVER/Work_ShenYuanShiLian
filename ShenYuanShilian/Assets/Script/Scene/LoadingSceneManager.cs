using Newtonsoft.Json;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static Spine.Unity.Editor.SkeletonBaker.BoneWeightContainer;

public class LoadingSceneManager : View
{
    public override string Name
    {
        get { return Consts.V_LoadingScene; }
    }

    public GameObject warnBoard;
    public GameObject noteBoard;
    public GameObject noticBoard;
    public GameObject btnStart;
    private bool startLoad = false;



    protected void Awake()
    {
        Render();
        DataManager.Instance.LoadDatas();
    }


    protected void Render()
    {
        Utils.FadeOut();
        noticBoard.SetActive(true);
        StartCoroutine(CloseNoticBoard());
    }

    private IEnumerator CloseNoticBoard()
    {
        yield return new WaitForSecondsRealtime(10f);
        Utils.FadeIn();
        yield return new WaitForSecondsRealtime(1f);
        noticBoard.SetActive(false);
        Utils.FadeOut();
    }

    // private void Update()
    // {
    //     if (!startLoad) return;
    //     curProgress += Time.deltaTime / loadingTime;
    //     if (curProgress > 1f)
    //     {
    //         curProgress = 1f;
    //         startLoad = false;
    //     }
    //
    //     OnSliderValueChange(curProgress);
    // }
    //
    //
    //
    // private void OnSliderValueChange(float value)
    // {
    //     slider.value = value;
    //     if (slider.value >= 1)
    //     {
    //         btnStart.SetActive(true);
    //     }
    // }

    public void OnBtnIcon()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        noteBoard.SetActive(true);
    }

    public void OnShowWarn()
    {
        warnBoard.SetActive(true);
    }

    public void OnBtnWarnClose()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        warnBoard.SetActive(false);
    }

    public void OnBtnNoteClose()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        noteBoard.SetActive(false);
    }

    public void OnBtnStart()
    {
        //GameManager.Instance.ChangeScene(Scene.Main);
        //return;
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });

#if UNITY_EDITOR
        string str =
            "{\"adult_level\":\"3\",\"is_holiday\":\"false\",\"user_id\":\"123456789\",\"nickname\":\"q751531499\",\"timestamp\":\"2024-11-19T09:15:00Z\"}";
        LoginCallBack(str);
#elif UNITY_ANDROID
        //Android平台调用SDK
        AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        unityActivity.Call("login");
#endif
    }

    public void LoginCallBack(string str)
    {
        LoginParam param = JsonConvert.DeserializeObject<LoginParam>(str);
        if (PlayerPrefs.HasKey(param.nickname))
        {
            //旧号
            GetModel<GameModel>().GameData =
                FullSerializerAPI.Deserialize(typeof(GameData), PlayerPrefs.GetString(param.nickname)) as GameData;
        }
        //新号
        else
        {
            //高级账号
            if (GetModel<GameModel>().SVIP.IndexOf(param.nickname) != -1)
            {
                SVIPLogin();
            }
            //中级账号
            else if (GetModel<GameModel>().VIP.IndexOf(param.nickname) != -1)
            {
                VIPLogin();
            }
        }

        GetModel<GameModel>().Account = param.nickname;
        GetModel<GameModel>().Age = Convert.ToInt32(param.adult_level);
        GetModel<GameModel>().TimeStamp = param.timestamp;

        if (GetModel<GameModel>().Age == 0 || GetModel<GameModel>().Age == 1)
        {
            return;
        }
        else if (GetModel<GameModel>().Age == 2)
        {
            GameManager.Instance.ExitGame(GetModel<GameModel>().TimeStamp);
        }

        Utils.FadeIn();
        GameManager.Instance.StartOnlineTimer();
        GameManager.Instance.ChangeScene(Scene.Main);
    }


    public override void HandleEvent(object data = null)
    {
    }

    private void SVIPLogin()
    {
        //新手引导
        GetModel<GameModel>().FinishGuidance = true;
        GetModel<GameModel>().NickName = "高级账号";
        //角色
        GetModel<GameModel>().UnlockCharacter[0] = 1;
        GetModel<GameModel>().UnlockCharacter[1] = 1;
        GetModel<GameModel>().PlayerLevel[0] = 200;
        GetModel<GameModel>().PlayerLevel[1] = 200;
        GetModel<GameModel>().PlayerLevel[2] = 200;
        //资源
        GetModel<GameModel>().Coin = 1000000;
        GetModel<GameModel>().Diamond = 1000000;
        GetModel<GameModel>().StonesCount = 1000000;
        //关卡
        GetModel<GameModel>().LevelType = 0;
        GetModel<GameModel>().LevelCount = 81;
        //技能树
        int[] critProLevel = { 2, 2, 2, 2 };
        int[] addDamageProLevel = { 2, 2, 2, 2 };
        int[] dodgeProLevel = { 2, 2, 2, 2 };
        int[] damageReducProLevel = { 2, 2, 2, 2 };
        bool[] critBranchLine = { true, true, true, true, true, true };
        bool[] addDamageBranchLine = { true, true, true, true, true, true };
        bool[] dodgeBranchLine = { true, true, true, true, true, true };
        bool[] damageReducBranchLine = { true, true, true, true, true, true };
        GetModel<GameModel>().CritProLevel = critProLevel;
        GetModel<GameModel>().AddDamageProLevel = addDamageProLevel;
        GetModel<GameModel>().DodgeProLevel = dodgeProLevel;
        GetModel<GameModel>().DamageReducProLevel = damageReducProLevel;
        GetModel<GameModel>().AtkProLevel = 5;
        GetModel<GameModel>().HpProLevel = 5;
        GetModel<GameModel>().Skill1Level = 1;
        GetModel<GameModel>().Skill2Level = 1;
        GetModel<GameModel>().Skill3Level = 1;
        GetModel<GameModel>().Skill4Level = 1;
        GetModel<GameModel>().CritBranchLine = critBranchLine;
        GetModel<GameModel>().AddDamageBranchLine = addDamageBranchLine;
        GetModel<GameModel>().DodgeBranchLine = dodgeBranchLine;
        GetModel<GameModel>().DamageReducBranchLine = damageReducBranchLine;
        GetModel<GameModel>().SkillTreeAtk += 50;
        GetModel<GameModel>().SkillTreeHp += 50;
        GetModel<GameModel>().SkillTreeCritRate += 16;
        GetModel<GameModel>().SkillTreeDamageAdd += 16;
        GetModel<GameModel>().SkillTreeDodgeRate += 16;
        GetModel<GameModel>().SkillTreeDamageReduc += 16;

        //装备
        // GetModel<GameModel>().WornEquipments = new()
        // {
        //     { EquipType.Weapon , new EquipInfoData(){id = 6, name = "星陨杖", value = 70, type = "Weapon", cnType = "法杖", propertyType="攻击力", isPercent=false, quality = 3, convertStones = 20}},
        //     { EquipType.Necklace ,new EquipInfoData(){id = 6, name = "星陨链", value = 18, type = "Necklace", cnType = "项链", propertyType="重击率", isPercent=true, quality = 3, convertStones = 20}},
        //     { EquipType.Cloak ,new EquipInfoData(){id = 6, name = "龙鳞袍", value = 12, type = "Cloak", cnType = "斗篷", propertyType="伤害增益", isPercent=true, quality = 3, convertStones = 20}},
        //     { EquipType.Head ,new EquipInfoData(){id = 6, name = "水晶盔", value = 220, type = "Head", cnType = "头盔", propertyType="生命值", isPercent=false, quality = 3, convertStones = 20}},
        //     { EquipType.Armor ,new EquipInfoData(){id = 6, name = "日冕铠", value = 12, type = "Armor", cnType = "胸甲", propertyType="伤害减免", isPercent=true, quality = 3, convertStones = 20}},
        //     { EquipType.Legs ,new EquipInfoData(){id = 6, name = "极光护腿", value = 10, type = "Legs", cnType = "腿甲", propertyType="闪避率", isPercent=true, quality = 3, convertStones = 20}},
        // };
        GetModel<GameModel>().WornEquipments[EquipType.Weapon] =
            DataManager.Instance.equipmentDic[nameof(EquipType.Weapon)][5];
        GetModel<GameModel>().WornEquipments[EquipType.Necklace] =
            DataManager.Instance.equipmentDic[nameof(EquipType.Necklace)][5];
        GetModel<GameModel>().WornEquipments[EquipType.Cloak] =
            DataManager.Instance.equipmentDic[nameof(EquipType.Cloak)][5];
        GetModel<GameModel>().WornEquipments[EquipType.Head] =
            DataManager.Instance.equipmentDic[nameof(EquipType.Head)][5];
        GetModel<GameModel>().WornEquipments[EquipType.Armor] =
            DataManager.Instance.equipmentDic[nameof(EquipType.Armor)][5];
        GetModel<GameModel>().WornEquipments[EquipType.Legs] =
            DataManager.Instance.equipmentDic[nameof(EquipType.Legs)][5];

        ClearOwnDic();
        for (int i = 0; i < 6; i++)
        {
            GetModel<GameModel>().OwnedEquipments[EquipType.Weapon]
                .Add(DataManager.Instance.equipmentDic[nameof(EquipType.Weapon)][i]);
            GetModel<GameModel>().OwnedEquipments[EquipType.Necklace]
                .Add(DataManager.Instance.equipmentDic[nameof(EquipType.Necklace)][i]);
            GetModel<GameModel>().OwnedEquipments[EquipType.Cloak]
                .Add(DataManager.Instance.equipmentDic[nameof(EquipType.Cloak)][i]);
            GetModel<GameModel>().OwnedEquipments[EquipType.Head]
                .Add(DataManager.Instance.equipmentDic[nameof(EquipType.Head)][i]);
            GetModel<GameModel>().OwnedEquipments[EquipType.Armor]
                .Add(DataManager.Instance.equipmentDic[nameof(EquipType.Armor)][i]);
            GetModel<GameModel>().OwnedEquipments[EquipType.Legs]
                .Add(DataManager.Instance.equipmentDic[nameof(EquipType.Legs)][i]);
        }

        GetModel<GameModel>().StrengthEquipLevel = new()
                {
                    { EquipType.Weapon, 60 },
                    { EquipType.Necklace, 60 },
                    { EquipType.Cloak, 60 },
                    { EquipType.Head, 60 },
                    { EquipType.Armor, 60 },
                    { EquipType.Legs, 60 },
                };
    }

    private void VIPLogin()
    {
        //新手引导
        GetModel<GameModel>().FinishGuidance = true;
        GetModel<GameModel>().NickName = "中级账号";
        //角色
        // GetModel<GameModel>().UnlockCharacter[0] = 1;
        GetModel<GameModel>().PlayerLevel[0] = 100;
        GetModel<GameModel>().PlayerLevel[1] = 100;
        //资源
        GetModel<GameModel>().Coin = 500000;
        GetModel<GameModel>().Diamond = 500000;
        GetModel<GameModel>().StonesCount = 50000;
        //关卡
        GetModel<GameModel>().LevelType = 0;
        GetModel<GameModel>().LevelCount = 40;
        //技能树
        int[] critProLevel = { 2, 2, 0, 0 };
        int[] addDamageProLevel = { 2, 2, 0, 0 };
        int[] dodgeProLevel = { 2, 2, 0, 0 };
        int[] damageReducProLevel = { 2, 2, 0, 0 };
        bool[] critBranchLine = { true, true, true, true, false, false };
        bool[] addDamageBranchLine = { true, true, true, true, false, false };
        bool[] dodgeBranchLine = { true, true, true, true, false, false };
        bool[] damageReducBranchLine = { true, true, true, true, false, false };
        GetModel<GameModel>().CritProLevel = critProLevel;
        GetModel<GameModel>().AddDamageProLevel = addDamageProLevel;
        GetModel<GameModel>().DodgeProLevel = dodgeProLevel;
        GetModel<GameModel>().DamageReducProLevel = damageReducProLevel;
        GetModel<GameModel>().AtkProLevel = 5;
        GetModel<GameModel>().HpProLevel = 5;
        GetModel<GameModel>().Skill1Level = 1;
        GetModel<GameModel>().Skill2Level = 0;
        GetModel<GameModel>().Skill3Level = 1;
        GetModel<GameModel>().Skill4Level = 0;
        GetModel<GameModel>().CritBranchLine = critBranchLine;
        GetModel<GameModel>().AddDamageBranchLine = addDamageBranchLine;
        GetModel<GameModel>().DodgeBranchLine = dodgeBranchLine;
        GetModel<GameModel>().DamageReducBranchLine = damageReducBranchLine;
        GetModel<GameModel>().SkillTreeAtk += 50;
        GetModel<GameModel>().SkillTreeHp += 50;
        GetModel<GameModel>().SkillTreeCritRate += 8;
        GetModel<GameModel>().SkillTreeDamageAdd += 8;
        GetModel<GameModel>().SkillTreeDodgeRate += 8;
        GetModel<GameModel>().SkillTreeDamageReduc += 8;

        //装备
        // GetModel<GameModel>().WornEquipments = new()
        // {
        //     { EquipType.Weapon , new EquipInfoData(){id = 4, name = "水晶杖", value = 35, type = "Weapon", cnType = "法杖", propertyType="攻击力", isPercent=false, quality = 2, convertStones = 10}},
        //     { EquipType.Necklace ,new EquipInfoData(){id = 4, name = "水晶链", value = 10, type = "Necklace", cnType = "项链", propertyType="重击率", isPercent=true, quality = 2, convertStones = 10}},
        //     { EquipType.Cloak ,new EquipInfoData(){id = 4, name = "雾纱袍", value = 8, type = "Cloak", cnType = "斗篷", propertyType="伤害增益", isPercent=true, quality = 2, convertStones = 10}},
        //     { EquipType.Head ,new EquipInfoData(){id = 4, name = "雷鸣盔", value = 130, type = "Head", cnType = "头盔", propertyType="生命值", isPercent=false, quality = 2, convertStones = 10}},
        //     { EquipType.Armor ,new EquipInfoData(){id = 4, name = "遁纹甲", value = 8, type = "Armor", cnType = "胸甲", propertyType="伤害减免", isPercent=true, quality = 2, convertStones = 10}},
        //     { EquipType.Legs ,new EquipInfoData(){id = 4, name = "镜纹护腿", value = 6, type = "Legs", cnType = "腿甲", propertyType="闪避率", isPercent=true, quality = 2, convertStones = 10}}
        // };
        GetModel<GameModel>().WornEquipments[EquipType.Weapon] =
            DataManager.Instance.equipmentDic[nameof(EquipType.Weapon)][3];
        GetModel<GameModel>().WornEquipments[EquipType.Necklace] =
            DataManager.Instance.equipmentDic[nameof(EquipType.Necklace)][3];
        GetModel<GameModel>().WornEquipments[EquipType.Cloak] =
            DataManager.Instance.equipmentDic[nameof(EquipType.Cloak)][3];
        GetModel<GameModel>().WornEquipments[EquipType.Head] =
            DataManager.Instance.equipmentDic[nameof(EquipType.Head)][3];
        GetModel<GameModel>().WornEquipments[EquipType.Armor] =
            DataManager.Instance.equipmentDic[nameof(EquipType.Armor)][3];
        GetModel<GameModel>().WornEquipments[EquipType.Legs] =
            DataManager.Instance.equipmentDic[nameof(EquipType.Legs)][3];

        ClearOwnDic();
        for (int i = 0; i < 4; i++)
        {
            GetModel<GameModel>().OwnedEquipments[EquipType.Weapon]
                .Add(DataManager.Instance.equipmentDic[nameof(EquipType.Weapon)][i]);
            GetModel<GameModel>().OwnedEquipments[EquipType.Necklace]
                .Add(DataManager.Instance.equipmentDic[nameof(EquipType.Necklace)][i]);
            GetModel<GameModel>().OwnedEquipments[EquipType.Cloak]
                .Add(DataManager.Instance.equipmentDic[nameof(EquipType.Cloak)][i]);
            GetModel<GameModel>().OwnedEquipments[EquipType.Head]
                .Add(DataManager.Instance.equipmentDic[nameof(EquipType.Head)][i]);
            GetModel<GameModel>().OwnedEquipments[EquipType.Armor]
                .Add(DataManager.Instance.equipmentDic[nameof(EquipType.Armor)][i]);
            GetModel<GameModel>().OwnedEquipments[EquipType.Legs]
                .Add(DataManager.Instance.equipmentDic[nameof(EquipType.Legs)][i]);
        }

        GetModel<GameModel>().StrengthEquipLevel = new()
                {
                    { EquipType.Weapon, 30 },
                    { EquipType.Necklace, 30 },
                    { EquipType.Cloak, 30 },
                    { EquipType.Head, 30 },
                    { EquipType.Armor, 30 },
                    { EquipType.Legs, 30 },
                };
    }

    void ClearOwnDic()
    {
        GetModel<GameModel>().OwnedEquipments[EquipType.Weapon].Clear();
        GetModel<GameModel>().OwnedEquipments[EquipType.Necklace].Clear();
        GetModel<GameModel>().OwnedEquipments[EquipType.Head].Clear();
        GetModel<GameModel>().OwnedEquipments[EquipType.Cloak].Clear();
        GetModel<GameModel>().OwnedEquipments[EquipType.Legs].Clear();
        GetModel<GameModel>().OwnedEquipments[EquipType.Armor].Clear();
    }

}
