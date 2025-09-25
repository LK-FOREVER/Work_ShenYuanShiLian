using System;
using System.Collections.Generic;

public class GameData
{
    public string account = "5171786397";
    public string timeStamp = "1721635756";
    public int age = 3;
    
    //public List<CharacterModel> characterModelList = new List<CharacterModel>()
    //{
    //    new CharacterModel(0,0,0,0,true),
    //    new CharacterModel(1,0,0,0,false),
    //    new CharacterModel(2,0,0,0,false),
    //};


    //原来游戏是有三个角色，所以以下数据结构包含三个元素
    public int[] hpLevel = { 0, 0, 0 };//废弃
    public int[] atkLevel = { 0, 0, 0 };//废弃
    public int[] propLevel = { 0, 0, 0 };//废弃
    public int[] helmetLevel = { 0, 0, 0 };//头盔等级，废弃
    public int[] corseletLevel = { 0, 0, 0 };//盔甲等级，废弃
    public int[] cuishLevel = { 0, 0, 0 };//鞋子等级，废弃
    public bool[] unlock = { false, false, true };//废弃
    public int[] soldierEquipments = { -1, -1, -1 };//废弃
    public int[] archerEquipments = { -1, -1, -1 };//废弃
    public int[] masterEquipments = { -1, -1, -1 };//废弃


    public int[] playerLevel = { 1, 1, 1 };//角色等级，三个角色共享等级,所以升级的时候会同步升级
    public string curName = "玩家";
    public int curId = 1; // 当前选中的角色ID，0-冰魔法师，1-火魔法师，2-暗魔法师
    public int coin = 0; // 拥有的金币数量
    public int diamond = 0; // 拥有的金币数量
    public int exp = 0; // 拥有的经验值
    public int stonesCount; //强化石数量
    public int skillPoint = 0; //技能点数量，玩家一共200级，每升4级获得1个技能点，总共50个技能点。

    public int levelType = 0;//0-普通关卡，1-领袖挑战模式
    public int levelCount = 1;//当前已解锁的关卡数，总共80关，但是为了处理MapView中关卡按钮的状态，设置81时，才表示所有关卡 通关

    public float musicVolume = 1f;
    public float soundVolume = 1f;

    public List<FriendClass> friendList = new List<FriendClass>();
    public TotalProperty totalProperty = new TotalProperty();

    public int getTreasureTimes = 0;

    public int orangeTreasureTarget = 0;

    public bool firstGetTreasure;

    public bool firstPay;

    public int[] unlockCharacter = { 0, 0 };

    public int[] dailyBuyLimit = { 0, 0, 0 };

    public int[] weeklyBuyLimit = { 0, 0, 0 };


    public ObservableDictionary<int, TaskState> dailyTaskProgress = new ObservableDictionary<int, TaskState>()
    { };

    public ObservableDictionary<int, TaskState> weeklyTaskProgress = new ObservableDictionary<int, TaskState>()
    { };


    public DateTime firstLoginOfDay;
    
    public DateTime lastAFKRewardTime;
    
    public int cacheAFKCoin = 0;
    public int cacheAFKStone = 0;
    public int cacheAFKExp = 0;
    public int cacheAFKDiamond = 0;
    public bool todayQuickAFK;

    public string nickName;
    public int friendID;
    
    public bool finishGuidance;
    

    //穿着的装备
    public ObservableDictionary<EquipType, EquipInfoData> wornEquipments = new()
    {
        { EquipType.Weapon ,null},
        { EquipType.Necklace ,null},
        { EquipType.Cloak ,null},
        { EquipType.Head ,null},
        { EquipType.Armor ,null},
        { EquipType.Legs ,null},
    };
    //拥有的装备
    public ObservableDictionary<EquipType, List<EquipInfoData>> ownedEquipments = new ObservableDictionary<EquipType, List<EquipInfoData>>()
    {
        { EquipType.Weapon ,new List<EquipInfoData>()},
        { EquipType.Necklace ,new List<EquipInfoData>()},
        { EquipType.Cloak ,new List<EquipInfoData>()},
        { EquipType.Head ,new List<EquipInfoData>()},
        { EquipType.Armor ,new List<EquipInfoData>()},
        { EquipType.Legs ,new List<EquipInfoData>()},
    };
    //装备槽位的强化等级
    public ObservableDictionary<EquipType, int> strengthEquipLevel = new()
    {
        { EquipType.Weapon ,0},
        { EquipType.Necklace ,0},
        { EquipType.Cloak ,0},
        { EquipType.Head ,0},
        { EquipType.Armor ,0},
        { EquipType.Legs ,0},
    };

    // 技能树
    // 三个角色共享等级
    public int atkProLevel = 0; // 攻击属性等级，0-未解锁
    public int[] critProLevel = { 0, 0, 0, 0 }; // 从下到上四个暴击属性点的等级，0-未解锁
    public int[] addDamageProLevel = { 0, 0, 0, 0 }; // 从下到上四个增伤属性点的等级，0-未解锁
    public int hpProLevel = 0; // 生命属性等级，0-未解锁
    public int[] dodgeProLevel = { 0, 0, 0, 0 }; // 从下到上四个闪避属性点的等级，0-未解锁
    public int[] damageReducProLevel = { 0, 0, 0, 0 }; // 从下到上四个伤害减免属性点的等级，0-未解锁
    public int skill_1_Level = 0; // 技能1-魔力膨胀等级，0-未解锁, 1-已解锁
    public int skill_2_Level = 0; // 技能2-全力一击等级，0-未解锁, 1-已解锁
    public int skill_3_Level = 0; // 技能3-荆棘之甲等级，0-未解锁, 1-已解锁
    public int skill_4_Level = 0; // 技能4-魔法力场等级，0-未解锁, 1-已解锁
    public bool[] critBranchLine = { false, false, false, false, false, false }; // 从下到上六条暴击属性分支线，false-未解锁
    public bool[] addDamageBranchLine = { false, false, false, false, false, false }; // 从下到上六条增伤属性分支线，false-未解锁
    public bool[] dodgeBranchLine = { false, false, false, false, false, false }; // 从下到上六条闪避属性分支线，false-未解锁
    public bool[] damageReducBranchLine = { false, false, false, false, false, false }; // 从下到上六条伤害减免属性分支线，false-未解锁

    // 技能树带来的属性加成
    public int skillTreeAtk = 0;// 攻击属性加成
    public int skillTreeHp = 0;// 生命属性加成
    public int skillTreeCritRate = 0;// 暴击率属性加成
    public int skillTreeDamageAdd = 0;// 增伤属性加成
    public int skillTreeDodgeRate = 0;// 闪避率属性加成
    public int skillTreeDamageReduc = 0;// 伤害减免属性加成
    
    public Scene nextScene;

    //领袖挑战
    public int leaderChallengeDamage = 0;//历史最高伤害
    public bool[] challengeRewardGet = { false, false, false, false, false, false, false, false, false, false };//记录挑战奖励是否领取
}

public class TaskState
{
    public int progress;
    public bool isGetReward;
}

public class GameModel : Model
{
    public override string Name
    {
        get
        {
            return Consts.M_GameModel;
        }
    }

    #region 字段

    public GameData gameData = new();


    //private List<CharacterModel> characterModelList = new List<CharacterModel>()
    //{
    //    new CharacterModel(0,0,0,0,true),
    //    new CharacterModel(1,0,0,0,false),
    //    new CharacterModel(2,0,0,0,false),
    //};

    private int curLevel = 1;//当前关卡
    private bool hasLoad = false;
    private bool timeout = false;
    private bool deffend = true;
    private bool haveMonster = false; // 是否有怪物存活

    public List<string> SVIP = new() { "q75531499", "r75531507", "x75531511", "f755315116",      "f75531596", "v75531618", "q75531638",         "t75531606", "y75531629","g75531648" };
    public List<string> VIP = new() { "q75531519", "p75531522", "h75531526", "p75531529",        "k75531600", "b75531622", "s75531642",            "t75531609", "a75531631","k75531651" };
    #endregion

    #region 属性
    public GameData GameData
    {
        get
        {
            return gameData;
        }
        set
        {
            gameData = value;
            StrengthEquipLevel.OnChanged += () =>
            {
                CalculateTotalProvide();
                SendEvent(Consts.E_SaveData);
            };
            WornEquipments.OnChanged += () =>
            {
                CalculateTotalProvide();
                SendEvent(Consts.E_SaveData);
            };
            OwnedEquipments.OnChanged += () =>
            {
                CalculateTotalProvide();
                SendEvent(Consts.E_SaveData);
            };
            DailyTaskProgress.OnChanged += () =>
            {
                SendEvent(Consts.E_SaveData);
            };
            WeeklyTaskProgress.OnChanged += () =>
            {
                SendEvent(Consts.E_SaveData);
            };
        }
    }

    public string Account
    {
        get
        {
            return gameData.account;
        }
        set
        {
            gameData.account = value;
        }
    }
    public string TimeStamp
    {
        get
        {
            return gameData.timeStamp;
        }
        set
        {
            gameData.timeStamp = value;
        }
    }
    public int Age
    {
        get
        {
            return gameData.age;
        }
        set
        {
            gameData.age = value;
        }
    }

    //public List<CharacterModel> CharacterModelList
    //{
    //    get
    //    {
    //        return gameData.characterModelList;
    //    }
    //    set
    //    {
    //        gameData.characterModelList = value;
    //        SendEvent(Consts.E_SaveData);
    //    }
    //}

    public int[] PlayerLevel
    {
        get
        {
            return gameData.playerLevel;
        }
        set
        {
            gameData.playerLevel = value;
            EventManager.Instance.TriggerEvent(EventName.RefreshTopMessage);
            SendEvent(Consts.E_UpdateHp);
            // SendEvent(Consts.E_ChangeCharacterModel);
            SendEvent(Consts.E_SaveData);
        }
    }
    public int[] HpLevel
    {
        get
        {
            return gameData.hpLevel;
        }
        set
        {
            gameData.hpLevel = value;
            SendEvent(Consts.E_ChangeCharacterModel);
            SendEvent(Consts.E_SaveData);
        }
    }
    public int[] AtkLevel
    {
        get
        {
            return gameData.atkLevel;
        }
        set
        {
            gameData.atkLevel = value;
            SendEvent(Consts.E_ChangeCharacterModel);
            SendEvent(Consts.E_SaveData);
        }
    }
    public int[] PropLevel
    {
        get
        {
            return gameData.propLevel;
        }
        set
        {
            gameData.propLevel = value;
            SendEvent(Consts.E_ChangeCharacterModel);
            SendEvent(Consts.E_SaveData);
        }
    }

    public int[] HelmetLevel
    {
        get
        {
            return gameData.helmetLevel;
        }
        set
        {
            gameData.helmetLevel = value;
            SendEvent(Consts.E_ChangeCharacterModel);
            SendEvent(Consts.E_SaveData);
        }
    }
    public int[] CorseletLevel
    {
        get
        {
            return gameData.corseletLevel;
        }
        set
        {
            gameData.corseletLevel = value;
            SendEvent(Consts.E_ChangeCharacterModel);
            SendEvent(Consts.E_SaveData);
        }
    }
    public int[] CuishLevel
    {
        get
        {
            return gameData.cuishLevel;
        }
        set
        {
            gameData.cuishLevel = value;
            SendEvent(Consts.E_ChangeCharacterModel);
            SendEvent(Consts.E_SaveData);
        }
    }

    public bool[] Unlock
    {
        get
        {
            return gameData.unlock;
        }
        set
        {
            gameData.unlock = value;
            SendEvent(Consts.E_ChangeCharacterModel);
            SendEvent(Consts.E_SaveData);
        }
    }

    public int[] SoldierEquipments
    {
        get
        {
            return gameData.soldierEquipments;
        }
        set
        {
            gameData.soldierEquipments = value;
            SendEvent(Consts.E_UpdateEquipment);
            SendEvent(Consts.E_SaveData);
        }
    }

    public int[] ArcherEquipments
    {
        get
        {
            return gameData.archerEquipments;
        }
        set
        {
            gameData.archerEquipments = value;
            SendEvent(Consts.E_UpdateEquipment);
            SendEvent(Consts.E_SaveData);
        }
    }

    public int[] MasterEquipments
    {
        get
        {
            return gameData.masterEquipments;
        }
        set
        {
            gameData.masterEquipments = value;
            SendEvent(Consts.E_UpdateEquipment);
            SendEvent(Consts.E_SaveData);
        }
    }

    public int CurId
    {
        get
        {
            return gameData.curId;
        }
        set
        {
            gameData.curId = value;
            EventManager.Instance.TriggerEvent(EventName.RefreshTopMessage);
            SendEvent(Consts.E_ChangeCharacterModel);
            SendEvent(Consts.E_SaveData);
        }
    }
    public string CurName
    {
        get
        {
            return gameData.curName;
        }
        set
        {
            gameData.curName = value;
            SendEvent(Consts.E_SaveData);
        }
    }
    public int Coin
    {
        get
        {
            return gameData.coin;
        }
        set
        {
            gameData.coin = value;
            SendEvent(Consts.E_UpdateCoin);
            SendEvent(Consts.E_SaveData);
        }
    }

    public int Diamond
    {
        get
        {
            return gameData.diamond;
        }
        set
        {
            gameData.diamond = value;
            SendEvent(Consts.E_UpdateDiamond);
            SendEvent(Consts.E_SaveData);
        }
    }

    public int Exp
    {
        get
        {
            return gameData.exp;
        }
        set
        {
            gameData.exp = value;
            SendEvent(Consts.E_UpdateExp);
            SendEvent(Consts.E_SaveData);
        }
    }
    public int SkillPoint
    {
        get
        {
            return gameData.skillPoint;
        }
        set
        {
            gameData.skillPoint = value;
            SendEvent(Consts.E_SaveData);
        }
    }


    public int LevelType
    {
        get
        {
            return gameData.levelType;
        }
        set
        {
            gameData.levelType = value;
            SendEvent(Consts.E_SaveData);
        }
    }
    public int LevelCount
    {
        get
        {
            return gameData.levelCount;
        }
        set
        {
            gameData.levelCount = value;
            SendEvent(Consts.E_SaveData);
        }
    }

    public float MusicVolume
    {
        get
        {
            return gameData.musicVolume;
        }
        set
        {
            gameData.musicVolume = value;
            SendEvent(Consts.E_ChangeVolume);
        }
    }
    public float SoundVolume
    {
        get
        {
            return gameData.soundVolume;
        }
        set
        {
            gameData.soundVolume = value;
            SendEvent(Consts.E_ChangeVolume);
        }
    }

    public int CurLevel
    {
        get
        {
            return curLevel;
        }
        set
        {
            curLevel = value;
        }
    }
    public bool HasLoad
    {
        get
        {
            return hasLoad;
        }
        set
        {
            hasLoad = value;
        }
    }
    public bool Timeout
    {
        get
        {
            return timeout;
        }
        set
        {
            timeout = value;
        }
    }
    public bool Deffend
    {
        get
        {
            return deffend;
        }
        set
        {
            deffend = value;
        }
    }
    public bool HaveMonster
    {
        get
        {
            return haveMonster;
        }
        set
        {
            haveMonster = value;
        }
    }
    public List<FriendClass> FriendList
    {
        get
        {
            return gameData.friendList;
        }
        set
        {
            gameData.friendList = value;
            SendEvent(Consts.E_SaveData);
        }
    }

    public int FriendLimit = 20;

    public TotalProperty TotalProperty
    {
        get
        {
            CalculateTotalProvide();
            return gameData.totalProperty;
        }
        set
        {
            gameData.totalProperty = value;
            SendEvent(Consts.E_SaveData);
        }
    }

    public ObservableDictionary<EquipType, EquipInfoData> WornEquipments
    {
        get
        {
            return gameData.wornEquipments;
        }
        set
        {
            gameData.wornEquipments = value;
        }
    }


    public ObservableDictionary<EquipType, int> StrengthEquipLevel
    {
        get
        {
            return gameData.strengthEquipLevel;
        }
        set
        {
            gameData.strengthEquipLevel = value;
        }
    }

    public ObservableDictionary<EquipType, List<EquipInfoData>> OwnedEquipments
    {
        get
        {
            return gameData.ownedEquipments;
        }
        set
        {
            gameData.ownedEquipments = value;
        }
    }

    public int StonesCount
    {
        get
        {
            return gameData.stonesCount;
        }
        set
        {
            gameData.stonesCount = value;
            SendEvent(Consts.E_SaveData);
        }
    }

    public int GetTreasureTimes
    {
        get
        {
            return gameData.getTreasureTimes;
        }
        set
        {
            gameData.getTreasureTimes = value;
            SendEvent(Consts.E_SaveData);
        }
    }

    public int OrangeTreasureTarget
    {
        get
        {
            return gameData.orangeTreasureTarget;
        }
        set
        {
            gameData.orangeTreasureTarget = value;
            SendEvent(Consts.E_SaveData);
        }
    }

    public int FriendID
    {
        get
        {
            return gameData.friendID;
        }
        set
        {
            gameData.friendID = value;
            SendEvent(Consts.E_SaveData);
        }
    }

    public bool FirstPay
    {
        get
        {
            return gameData.firstPay;
        }
        set
        {
            gameData.firstPay = value;
            SendEvent(Consts.E_SaveData);
        }
    }
    public string NickName
    {
        get
        {
            return gameData.nickName;
        }
        set
        {
            gameData.nickName = value;
            EventManager.Instance.TriggerEvent(EventName.RefreshTopMessage);
            SendEvent(Consts.E_SaveData);
        }
    }
    
    public ObservableDictionary<int,TaskState> DailyTaskProgress
    {
        get
        {
            return gameData.dailyTaskProgress;
        }
        set
        {
            gameData.dailyTaskProgress = value;
        }
    }

    public ObservableDictionary<int, TaskState> WeeklyTaskProgress
    {
        get
        {
            return gameData.weeklyTaskProgress;
        }
        set
        {
            gameData.weeklyTaskProgress = value;
        }
    }

    public DateTime FirstLoginOfDay
    {
        get
        {
            return gameData.firstLoginOfDay;
        }
        set
        {
            gameData.firstLoginOfDay = value;
            SendEvent(Consts.E_SaveData);
        }
    }
    
    public DateTime LastAFKRewardTime
    {
        get
        {
            return gameData.lastAFKRewardTime;
        }
        set
        {
            gameData.lastAFKRewardTime = value;
            SendEvent(Consts.E_SaveData);
        }
    }




    public void CalculateTotalProvide()
    {
        //总共属性 = 角色基础属性 + 武器提供属性 + 装备槽提供属性 + 技能树提供属性
        int weaponAttack = gameData.wornEquipments[EquipType.Weapon] == null ? 0 : (int)gameData.wornEquipments[EquipType.Weapon].value;
        gameData.totalProperty.atk = DataManager.Instance.expUpgradeInfoDic[gameData.curId][gameData.playerLevel[gameData.curId] - 1].atk +
                                         weaponAttack +
                                         DataManager.Instance.equipmentLevelDic[gameData.strengthEquipLevel[EquipType.Weapon]].weaponAttack +
                                         SkillTreeAtk;

        int headHp = gameData.wornEquipments[EquipType.Head] == null ? 0 : (int)gameData.wornEquipments[EquipType.Head].value;
        gameData.totalProperty.hp = DataManager.Instance.expUpgradeInfoDic[gameData.curId][gameData.playerLevel[gameData.curId] - 1].hp +
                                    headHp +
                                    DataManager.Instance.equipmentLevelDic[gameData.strengthEquipLevel[EquipType.Head]].headHP +
                                    SkillTreeHp;

        float legsDodgeRate = gameData.wornEquipments[EquipType.Legs] == null ? 0 : gameData.wornEquipments[EquipType.Legs].value;
        gameData.totalProperty.dodgeRate = DataManager.Instance.expUpgradeInfoDic[gameData.curId][gameData.playerLevel[gameData.curId] - 1].dodge +
                                            legsDodgeRate +
                                            DataManager.Instance.equipmentLevelDic[gameData.strengthEquipLevel[EquipType.Legs]].legsDodgeRate + SkillTreeDodgeRate;
        gameData.totalProperty.dodgeRate = Math.Round(gameData.totalProperty.dodgeRate / 100, 2);

        float necklaceCritRate = gameData.wornEquipments[EquipType.Necklace] == null ? 0 : gameData.wornEquipments[EquipType.Necklace].value;
        gameData.totalProperty.critRate = DataManager.Instance.expUpgradeInfoDic[gameData.curId][gameData.playerLevel[gameData.curId] - 1].crit +
                                          necklaceCritRate +
                                          DataManager.Instance.equipmentLevelDic[gameData.strengthEquipLevel[EquipType.Necklace]].necklaceCritRate + SkillTreeCritRate;
        gameData.totalProperty.critRate = Math.Round(gameData.totalProperty.critRate / 100, 2);


        float cloakDamageBonus = gameData.wornEquipments[EquipType.Cloak] == null ? 0 : gameData.wornEquipments[EquipType.Cloak].value;
        gameData.totalProperty.damageBonus = cloakDamageBonus +
                                             DataManager.Instance.equipmentLevelDic[gameData.strengthEquipLevel[EquipType.Cloak]].cloakDamageBonus;
        gameData.totalProperty.damageBonus = Math.Round(gameData.totalProperty.damageBonus / 100, 2);

        float armorDamageReduction = gameData.wornEquipments[EquipType.Armor] == null ? 0 : gameData.wornEquipments[EquipType.Armor].value;
        gameData.totalProperty.damageReduction = armorDamageReduction +
                                                 DataManager.Instance.equipmentLevelDic[gameData.strengthEquipLevel[EquipType.Armor]].armorDamageReduction;
        gameData.totalProperty.damageReduction = Math.Round(gameData.totalProperty.damageReduction / 100, 2);


        gameData.totalProperty.skillDamageBonus = SkillTreeDamageAdd / 100;
        gameData.totalProperty.skillDamageReduction = SkillTreeDamageReduc / 100;
    }

    #region 技能树
    public int AtkProLevel
    {
        get
        {
            return gameData.atkProLevel;
        }
        set
        {
            gameData.atkProLevel = value;
            SendEvent(Consts.E_SaveData);
        }
    }

    public int[] CritProLevel
    {
        get
        {
            return gameData.critProLevel;
        }
        set
        {
            gameData.critProLevel = value;
            SendEvent(Consts.E_SaveData);
        }
    }
    public int[] AddDamageProLevel
    {
        get
        {
            return gameData.addDamageProLevel;
        }
        set
        {
            gameData.addDamageProLevel = value;
            SendEvent(Consts.E_SaveData);
        }
    }

    public int HpProLevel
    {
        get
        {
            return gameData.hpProLevel;
        }
        set
        {
            gameData.hpProLevel = value;
            SendEvent(Consts.E_SaveData);
        }
    }
    public int[] DodgeProLevel
    {
        get
        {
            return gameData.dodgeProLevel;
        }
        set
        {
            gameData.dodgeProLevel = value;
            SendEvent(Consts.E_SaveData);
        }
    }
    public int[] DamageReducProLevel
    {
        get
        {
            return gameData.damageReducProLevel;
        }
        set
        {
            gameData.damageReducProLevel = value;
            SendEvent(Consts.E_SaveData);
        }
    }

    public int Skill1Level
    {
        get
        {
            return gameData.skill_1_Level;
        }
        set
        {
            gameData.skill_1_Level = value;
            SendEvent(Consts.E_SaveData);
        }
    }
    public int Skill2Level
    {
        get
        {
            return gameData.skill_2_Level;
        }
        set
        {
            gameData.skill_2_Level = value;
            SendEvent(Consts.E_SaveData);
        }
    }
    public int Skill3Level
    {
        get
        {
            return gameData.skill_3_Level;
        }
        set
        {
            gameData.skill_3_Level = value;
            SendEvent(Consts.E_SaveData);
        }
    }
    public int Skill4Level
    {
        get
        {
            return gameData.skill_4_Level;
        }
        set
        {
            gameData.skill_4_Level = value;
            SendEvent(Consts.E_SaveData);
        }
    }

    public bool[] CritBranchLine
    {
        get
        {
            return gameData.critBranchLine;
        }
        set
        {
            gameData.critBranchLine = value;
            SendEvent(Consts.E_SaveData);
        }
    }
    public bool[] AddDamageBranchLine
    {
        get
        {
            return gameData.addDamageBranchLine;
        }
        set
        {
            gameData.addDamageBranchLine = value;
            SendEvent(Consts.E_SaveData);
        }
    }
    public bool[] DodgeBranchLine
    {
        get
        {
            return gameData.dodgeBranchLine;
        }
        set
        {
            gameData.dodgeBranchLine = value;
            SendEvent(Consts.E_SaveData);
        }
    }
    public bool[] DamageReducBranchLine
    {
        get
        {
            return gameData.damageReducBranchLine;
        }
        set
        {
            gameData.damageReducBranchLine = value;
            SendEvent(Consts.E_SaveData);
        }
    }

    public int SkillTreeAtk
    {
        get
        {
            return gameData.skillTreeAtk;
        }
        set
        {
            gameData.skillTreeAtk = value;
            SendEvent(Consts.E_SaveData);
        }
    }

    public int SkillTreeHp
    {
        get
        {
            return gameData.skillTreeHp;
        }
        set
        {
            gameData.skillTreeHp = value;
            SendEvent(Consts.E_SaveData);
        }
    }

    public int SkillTreeCritRate
    {
        get
        {
            return gameData.skillTreeCritRate;
        }
        set
        {
            gameData.skillTreeCritRate = value;
            SendEvent(Consts.E_SaveData);
        }
    }

    public int SkillTreeDamageAdd
    {
        get
        {
            return gameData.skillTreeDamageAdd;
        }
        set
        {
            gameData.skillTreeDamageAdd = value;
            SendEvent(Consts.E_SaveData);
        }
    }

    public int SkillTreeDodgeRate
    {
        get
        {
            return gameData.skillTreeDodgeRate;
        }
        set
        {
            gameData.skillTreeDodgeRate = value;
            SendEvent(Consts.E_SaveData);
        }
    }

    public int SkillTreeDamageReduc
    {
        get
        {
            return gameData.skillTreeDamageReduc;
        }
        set
        {
            gameData.skillTreeDamageReduc = value;
            SendEvent(Consts.E_SaveData);
        }
    }

    public bool FirstGetTreasure
    {
        get
        {
            return gameData.firstGetTreasure;
        }
        set
        {
            gameData.firstGetTreasure = value;
            SendEvent(Consts.E_SaveData);
        }
    }

    public int[] UnlockCharacter
    {
        get
        {
            return gameData.unlockCharacter;
        }
        set
        {
            gameData.unlockCharacter = value;
            SendEvent(Consts.E_SaveData);
        }
    }

    public int[] DailyBuyLimit
    {
        get
        {
            return gameData.dailyBuyLimit;
        }
        set
        {
            gameData.dailyBuyLimit = value;
            SendEvent(Consts.E_SaveData);
        }
    }

    public int[] WeeklyBuyLimit
    {
        get
        {
            return gameData.weeklyBuyLimit;
        }
        set
        {
            gameData.weeklyBuyLimit = value;
            SendEvent(Consts.E_SaveData);
        }
    }
    
    public bool TodayQuickAFK
    {
        get
        {
            return gameData.todayQuickAFK;
        }
        set
        {
            gameData.todayQuickAFK = value;
            SendEvent(Consts.E_SaveData);
        }
    }
    
    public int CacheAFKCoin
    {
        get
        {
            return gameData.cacheAFKCoin;
        }
        set
        {
            gameData.cacheAFKCoin = value;
            EventManager.Instance.TriggerEvent(EventName.RefreshAFK);
        }
    }
    
    public int CacheAFKStone
    {
        get
        {
            return gameData.cacheAFKStone;
        }
        set
        {
            gameData.cacheAFKStone = value;
            EventManager.Instance.TriggerEvent(EventName.RefreshAFK);
        }
    }
    
    public int CacheAFKExp
    {
        get
        {
            return gameData.cacheAFKExp;
        }
        set
        {
            gameData.cacheAFKExp = value;
            EventManager.Instance.TriggerEvent(EventName.RefreshAFK);
        }
    }
    
    public int CacheAFKDiamond
    {
        get
        {
            return gameData.cacheAFKDiamond;
        }
        set
        {
            gameData.cacheAFKDiamond = value;
            EventManager.Instance.TriggerEvent(EventName.RefreshAFK);
        }
    }

    

    public int LeaderChallengeDamage
    {
        get
        {
            return gameData.leaderChallengeDamage;
        }
        set
        {
            //更新最高伤害
            if (value > gameData.leaderChallengeDamage)
            {
                gameData.leaderChallengeDamage = value;
            }
            SendEvent(Consts.E_SaveData);
        }
    }

    public bool[] ChallengeRewardGet
    {
        get
        {
            return gameData.challengeRewardGet;
        }
        set
        {
            gameData.challengeRewardGet = value;
            SendEvent(Consts.E_SaveData);
        }
    }
    
    public bool FinishGuidance
    {
        get
        {
            return gameData.finishGuidance;
        }
        set
        {
            gameData.finishGuidance = value;
            SendEvent(Consts.E_SaveData);
        }
    }
    
    public Scene NextScene
    {
        get
        {
            return gameData.nextScene;
        }
        set
        {
            gameData.nextScene = value;
        }
    }

    
    #endregion

    #endregion
}
