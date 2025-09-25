using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : SingletonBase<DataManager>
{
    public Dictionary<int, CharacterInfo> characterInfoDic;
    public Dictionary<int, List<UpgradeInfo>> upgradeInfoDic = new();
    public Dictionary<int, List<ExpUpgradeInfo>> expUpgradeInfoDic = new();
    public Dictionary<int, EquipmentInfo> equipmentInfoDic;
    public Dictionary<int, List<EquipmentUpgradeInfo>> equipmentUpgradeInfoDic = new();
    public Dictionary<int, MonsterInfo> monsterInfoDic;
    public Dictionary<int, LevelInfo> levelInfoDic;
    public Dictionary<int, MonsterWeight> monsterWeightDic;
    public Dictionary<int, ChapterData> levelMonsterInfo;
    public Dictionary<int, ChestWeight> chestWeightDic;
    public Dictionary<int, CoinInfo> coinInfoDic;
    public Dictionary<int, CardWeight> cardWeightDic;
    public Dictionary<int, EquipmentWeight> equipmentWeightDic;
    public Dictionary<int, CommonBuffWeight> commonBuffWeightDic;
    public Dictionary<int, SoldierBuffWeight> soldierBuffWeightDic;
    public Dictionary<int, ArcherBuffWeight> archerBuffWeightDic;
    public Dictionary<int, MasterBuffWeight> masterBuffWeightDic;
    public Dictionary<int, NormalRanklistInfo> normalRanklistInfoDic;
    public Dictionary<int, ChallengeRanklistInfo> challengeRanklistInfoDic;
    public Dictionary<int, ChallengeRewardInfo> challengeRewardInfoDic;
    public Dictionary<string, Dictionary<int, SkillInfoData>> skillInfoDataDic = new();
    public Dictionary<string, Dictionary<int, SkillInfo>> skillInfoDic = new();
    public Dictionary<string, Dictionary<int, SkillConfig>> skillConfigDic = new();
    public Dictionary<string, Dictionary<int, SkillTrigger>> skillTriggerDic = new();
    public Dictionary<string, Dictionary<int, SkillSelector>> skillSelectorDic = new();
    public Dictionary<string, Dictionary<int, SkillEffect>> skillEffectDic = new();

    public List<NameData> names = new List<NameData>();
    public List<SkillInfoData> allskillShowInfoDataList = new List<SkillInfoData>();
    public Dictionary<int, EquipmentLevelProperty> equipmentLevelDic = new();
    public List<EquipInfoData> equipmentList = new();
    public Dictionary<string, List<EquipInfoData>> equipmentDic = new();
    public List<TaskInfoData> dailyTaskList = new();
    public List<TaskInfoData> weeklyTaskList = new();
    public List<SevenSignInfo> sevenSignList = new();
    public List<ShopItemInfo> shopItemList = new();
    public List<AFKInfo> AFKInfoList = new();
    public HashSet<string> forbiddenWords = new HashSet<string>();

    public int onceDiamondCost = 100;
    public int getTreasureDayLimit= 30;
    public int orangeTarget = 50;
    private bool onceLoad = false;
    

    public Dictionary<string, EquipType> EquipTypeDic = new Dictionary<string, EquipType>()
    {
        {"Weapon",EquipType.Weapon},
        {"Necklace",EquipType.Necklace},
        {"Cloak",EquipType.Cloak},
        {"Head",EquipType.Head},
        {"Armor",EquipType.Armor},
        {"Legs",EquipType.Legs},
    };


    public void LoadDatas()
    {
        if (onceLoad)
        {
            return;
        }
        onceLoad = true;
        TextAsset textAsset = Resources.Load<TextAsset>("Data/character__info");
        string jsonStr = textAsset.text;
        List<CharacterInfo> characterInfo = FullSerializerAPI.Deserialize(typeof(List<CharacterInfo>), jsonStr) as List<CharacterInfo>;
        characterInfoDic = characterInfo.ToDictionary(key => key.id, value => value);

        textAsset = Resources.Load<TextAsset>("Data/character__upgrade");
        jsonStr = textAsset.text;
        List<UpgradeInfo> upgradeInfo = FullSerializerAPI.Deserialize(typeof(List<UpgradeInfo>), jsonStr) as List<UpgradeInfo>;
        foreach (UpgradeInfo item in upgradeInfo)
        {
            if (upgradeInfoDic.ContainsKey(item.type))
                upgradeInfoDic[item.type].Add(item);
            else
                upgradeInfoDic.Add(item.type, new List<UpgradeInfo>() { item });
        }

        textAsset = Resources.Load<TextAsset>("Data/character__exp_upgrade");
        jsonStr = textAsset.text;
        List<ExpUpgradeInfo> expUpgradeInfo = FullSerializerAPI.Deserialize(typeof(List<ExpUpgradeInfo>), jsonStr) as List<ExpUpgradeInfo>;
        foreach (ExpUpgradeInfo item in expUpgradeInfo)
        {
            if (expUpgradeInfoDic.ContainsKey(item.type))
                expUpgradeInfoDic[item.type].Add(item);
            else
                expUpgradeInfoDic.Add(item.type, new List<ExpUpgradeInfo>() { item });
        }

        textAsset = Resources.Load<TextAsset>("Data/equipment__info");
        jsonStr = textAsset.text;
        List<EquipmentInfo> equipmentInfo = FullSerializerAPI.Deserialize(typeof(List<EquipmentInfo>), jsonStr) as List<EquipmentInfo>;
        equipmentInfoDic = equipmentInfo.ToDictionary(key=>key.id, value => value);

        textAsset = Resources.Load<TextAsset>("Data/equipment__upgrade");
        jsonStr = textAsset.text;
        List<EquipmentUpgradeInfo> equipmentUpgradeInfo = FullSerializerAPI.Deserialize(typeof(List<EquipmentUpgradeInfo>), jsonStr) as List<EquipmentUpgradeInfo>;
        foreach (EquipmentUpgradeInfo item in equipmentUpgradeInfo)
        {
            if (equipmentUpgradeInfoDic.ContainsKey(item.type))
                equipmentUpgradeInfoDic[item.type].Add(item);
            else
                equipmentUpgradeInfoDic.Add(item.type, new List<EquipmentUpgradeInfo>() { item });
        }

        textAsset = Resources.Load<TextAsset>("Data/monster__info");
        jsonStr = textAsset.text;
        List<MonsterInfo> monsterInfo = FullSerializerAPI.Deserialize(typeof(List<MonsterInfo>), jsonStr) as List<MonsterInfo>;
        monsterInfoDic = monsterInfo.ToDictionary(key=>key.id, value => value);

        textAsset = Resources.Load<TextAsset>("Data/level__info");
        jsonStr = textAsset.text;
        List<LevelInfo> levelInfo = FullSerializerAPI.Deserialize(typeof(List<LevelInfo>), jsonStr) as List<LevelInfo>;
        levelInfoDic = levelInfo.ToDictionary(key => key.id, value => value);

        textAsset = Resources.Load<TextAsset>("Data/level__monster");
        jsonStr = textAsset.text;
        List<MonsterWeight> monsterWeight = FullSerializerAPI.Deserialize(typeof(List<MonsterWeight>), jsonStr) as List<MonsterWeight>;
        monsterWeightDic = monsterWeight.ToDictionary(key => key.id, value => value);

        textAsset = Resources.Load<TextAsset>("Data/level__monster__info");
        jsonStr = textAsset.text;
        List<ChapterData> gameLevelsData = FullSerializerAPI.Deserialize(typeof(List<ChapterData>), jsonStr) as List<ChapterData>;
        levelMonsterInfo = gameLevelsData.ToDictionary(key => key.chapterId, value => value);

        textAsset = Resources.Load<TextAsset>("Data/level__chest");
        jsonStr = textAsset.text;
        List<ChestWeight> chestWeight = FullSerializerAPI.Deserialize(typeof(List<ChestWeight>), jsonStr) as List<ChestWeight>;
        chestWeightDic = chestWeight.ToDictionary(key => key.id, value => value);

        textAsset = Resources.Load<TextAsset>("Data/level__coin");
        jsonStr = textAsset.text;
        List<CoinInfo> coinInfo = FullSerializerAPI.Deserialize(typeof(List<CoinInfo>), jsonStr) as List<CoinInfo>;
        coinInfoDic = coinInfo.ToDictionary(key => key.id, value => value);

        textAsset = Resources.Load<TextAsset>("Data/level__card");
        jsonStr = textAsset.text;
        List<CardWeight> cardWeight = FullSerializerAPI.Deserialize(typeof(List<CardWeight>), jsonStr) as List<CardWeight>;
        cardWeightDic = cardWeight.ToDictionary(key => key.id, value => value);

        textAsset = Resources.Load<TextAsset>("Data/level__equipment");
        jsonStr = textAsset.text;
        List<EquipmentWeight> equipmentWeight = FullSerializerAPI.Deserialize(typeof(List<EquipmentWeight>), jsonStr) as List<EquipmentWeight>;
        equipmentWeightDic = equipmentWeight.ToDictionary(key => key.id, value => value);

        textAsset = Resources.Load<TextAsset>("Data/level__commonBuff");
        jsonStr = textAsset.text;
        List<CommonBuffWeight> commonBuffWeight = FullSerializerAPI.Deserialize(typeof(List<CommonBuffWeight>), jsonStr) as List<CommonBuffWeight>;
        commonBuffWeightDic = commonBuffWeight.ToDictionary(key => key.id, value => value);

        textAsset = Resources.Load<TextAsset>("Data/level__soldierBuff");
        jsonStr = textAsset.text;
        List<SoldierBuffWeight> soldierBuffWeight = FullSerializerAPI.Deserialize(typeof(List<SoldierBuffWeight>), jsonStr) as List<SoldierBuffWeight>;
        soldierBuffWeightDic = soldierBuffWeight.ToDictionary(key => key.id, value => value);

        textAsset = Resources.Load<TextAsset>("Data/level__archerBuff");
        jsonStr = textAsset.text;
        List<ArcherBuffWeight> archerBuffWeight = FullSerializerAPI.Deserialize(typeof(List<ArcherBuffWeight>), jsonStr) as List<ArcherBuffWeight>;
        archerBuffWeightDic = archerBuffWeight.ToDictionary(key => key.id, value => value);

        textAsset = Resources.Load<TextAsset>("Data/level__masterBuff");
        jsonStr = textAsset.text;
        List<MasterBuffWeight> masterBuffWeight = FullSerializerAPI.Deserialize(typeof(List<MasterBuffWeight>), jsonStr) as List<MasterBuffWeight>;
        masterBuffWeightDic = masterBuffWeight.ToDictionary(key => key.id, value => value);

        textAsset = Resources.Load<TextAsset>("Data/normal_ranklist_info");
        jsonStr = textAsset.text;
        List<NormalRanklistInfo> normalRanklistInfo = FullSerializerAPI.Deserialize(typeof(List<NormalRanklistInfo>), jsonStr) as List<NormalRanklistInfo>;
        normalRanklistInfoDic = normalRanklistInfo.ToDictionary(key => key.player_id, value => value);

        textAsset = Resources.Load<TextAsset>("Data/challenge_ranklist_info");
        jsonStr = textAsset.text;
        List<ChallengeRanklistInfo> challengeRanklistInfo = FullSerializerAPI.Deserialize(typeof(List<ChallengeRanklistInfo>), jsonStr) as List<ChallengeRanklistInfo>;
        challengeRanklistInfoDic = challengeRanklistInfo.ToDictionary(key => key.player_id, value => value);

        textAsset = Resources.Load<TextAsset>("Data/challenge_reward_info");
        jsonStr = textAsset.text;
        List<ChallengeRewardInfo> challengeRewardInfo = FullSerializerAPI.Deserialize(typeof(List<ChallengeRewardInfo>), jsonStr) as List<ChallengeRewardInfo>;
        challengeRewardInfoDic = challengeRewardInfo.ToDictionary(key => key.id, value => value);

        textAsset = Resources.Load<TextAsset>("Data/commonSkill__info");
        jsonStr = textAsset.text;
        List<SkillInfoData> commonSkillInfo = FullSerializerAPI.Deserialize(typeof(List<SkillInfoData>), jsonStr) as List<SkillInfoData>;
        Dictionary<int, SkillInfoData> commonSkillInfoDataDic = commonSkillInfo.ToDictionary(key => key.skillId, value => value);
        skillInfoDataDic.Add("Common", commonSkillInfoDataDic);

        textAsset = Resources.Load<TextAsset>("Data/iceSkill__info");
        jsonStr = textAsset.text;
        List<SkillInfoData> iceSkillInfo = FullSerializerAPI.Deserialize(typeof(List<SkillInfoData>), jsonStr) as List<SkillInfoData>;
        Dictionary<int, SkillInfoData> iceSkillInfoDataDic = iceSkillInfo.ToDictionary(key => key.skillId, value => value);
        skillInfoDataDic.Add("Ice", iceSkillInfoDataDic);

        textAsset = Resources.Load<TextAsset>("Data/fireSkill__info");
        jsonStr = textAsset.text;
        List<SkillInfoData> fireSkillInfo = FullSerializerAPI.Deserialize(typeof(List<SkillInfoData>), jsonStr) as List<SkillInfoData>;
        Dictionary<int, SkillInfoData> fireSkillInfoDataDic = fireSkillInfo.ToDictionary(key => key.skillId, value => value);
        skillInfoDataDic.Add("Fire", fireSkillInfoDataDic);

        textAsset = Resources.Load<TextAsset>("Data/darkSkill__info");
        jsonStr = textAsset.text;
        List<SkillInfoData> darkSkillInfo = FullSerializerAPI.Deserialize(typeof(List<SkillInfoData>), jsonStr) as List<SkillInfoData>;
        Dictionary<int, SkillInfoData> darkSkillInfoDataDic = darkSkillInfo.ToDictionary(key => key.skillId, value => value);
        skillInfoDataDic.Add("Dark", darkSkillInfoDataDic);

        textAsset = Resources.Load<TextAsset>("Data/commonBuff__info");
        jsonStr = textAsset.text;
        List<SkillInfo> commonBuffInfo = FullSerializerAPI.Deserialize(typeof(List<SkillInfo>), jsonStr) as List<SkillInfo>;
        Dictionary<int, SkillInfo> commonBuffDic = commonBuffInfo.ToDictionary(key => key.id, value => value);
        skillInfoDic.Add("Common", commonBuffDic);

        textAsset = Resources.Load<TextAsset>("Data/commonBuff__config");
        jsonStr = textAsset.text;
        List<SkillConfig> commonBuffConfig = FullSerializerAPI.Deserialize(typeof(List<SkillConfig>), jsonStr) as List<SkillConfig>;
        Dictionary<int, SkillConfig> commonBuffConfigDic = commonBuffConfig.ToDictionary(key => key.id, value => value);
        skillConfigDic.Add("Common", commonBuffConfigDic);

        textAsset = Resources.Load<TextAsset>("Data/commonBuff__trigger");
        jsonStr = textAsset.text;
        List<SkillTrigger> commonBuffTrigger = FullSerializerAPI.Deserialize(typeof(List<SkillTrigger>), jsonStr) as List<SkillTrigger>;
        Dictionary<int, SkillTrigger> commonBuffTriggerDic = commonBuffTrigger.ToDictionary(key => key.id, value => value);
        skillTriggerDic.Add("Common", commonBuffTriggerDic);

        textAsset = Resources.Load<TextAsset>("Data/commonBuff__selector");
        jsonStr = textAsset.text;
        List<SkillSelector> commonBuffSelector = FullSerializerAPI.Deserialize(typeof(List<SkillSelector>), jsonStr) as List<SkillSelector>;
        Dictionary<int, SkillSelector> commonBuffSelectorDic = commonBuffSelector.ToDictionary(key => key.id, value => value);
        skillSelectorDic.Add("Common", commonBuffSelectorDic);

        textAsset = Resources.Load<TextAsset>("Data/commonBuff__effect");
        jsonStr = textAsset.text;
        List<SkillEffect> commonBuffEffect = FullSerializerAPI.Deserialize(typeof(List<SkillEffect>), jsonStr) as List<SkillEffect>;
        Dictionary<int, SkillEffect> commonBuffEffectDic = commonBuffEffect.ToDictionary(key => key.id, value => value);
        skillEffectDic.Add("Common", commonBuffEffectDic);

        textAsset = Resources.Load<TextAsset>("Data/soldierBuff__info");
        jsonStr = textAsset.text;
        List<SkillInfo> soldierBuffInfo = FullSerializerAPI.Deserialize(typeof(List<SkillInfo>), jsonStr) as List<SkillInfo>;
        Dictionary<int, SkillInfo> soldierBuffDic = soldierBuffInfo.ToDictionary(key => key.id, value => value);
        skillInfoDic.Add("Soldier", soldierBuffDic);

        textAsset = Resources.Load<TextAsset>("Data/soldierBuff__config");
        jsonStr = textAsset.text;
        List<SkillConfig> soldierBuffConfig = FullSerializerAPI.Deserialize(typeof(List<SkillConfig>), jsonStr) as List<SkillConfig>;
        Dictionary<int, SkillConfig> soldierBuffConfigDic = soldierBuffConfig.ToDictionary(key => key.id, value => value);
        skillConfigDic.Add("Soldier", soldierBuffConfigDic);

        textAsset = Resources.Load<TextAsset>("Data/soldierBuff__trigger");
        jsonStr = textAsset.text;
        List<SkillTrigger> soldierBuffTrigger = FullSerializerAPI.Deserialize(typeof(List<SkillTrigger>), jsonStr) as List<SkillTrigger>;
        Dictionary<int, SkillTrigger> soldierBuffTriggerDic = soldierBuffTrigger.ToDictionary(key => key.id, value => value);
        skillTriggerDic.Add("Soldier", soldierBuffTriggerDic);

        textAsset = Resources.Load<TextAsset>("Data/soldierBuff__selector");
        jsonStr = textAsset.text;
        List<SkillSelector> soldierBuffSelector = FullSerializerAPI.Deserialize(typeof(List<SkillSelector>), jsonStr) as List<SkillSelector>;
        Dictionary<int, SkillSelector> soldierBuffSelectorDic = soldierBuffSelector.ToDictionary(key => key.id, value => value);
        skillSelectorDic.Add("Soldier", soldierBuffSelectorDic);

        textAsset = Resources.Load<TextAsset>("Data/soldierBuff__effect");
        jsonStr = textAsset.text;
        List<SkillEffect> soldierBuffEffect = FullSerializerAPI.Deserialize(typeof(List<SkillEffect>), jsonStr) as List<SkillEffect>;
        Dictionary<int, SkillEffect> soldierBuffEffectDic = soldierBuffEffect.ToDictionary(key => key.id, value => value);
        skillEffectDic.Add("Soldier", soldierBuffEffectDic);

        textAsset = Resources.Load<TextAsset>("Data/archerBuff__info");
        jsonStr = textAsset.text;
        List<SkillInfo> archerBuffInfo = FullSerializerAPI.Deserialize(typeof(List<SkillInfo>), jsonStr) as List<SkillInfo>;
        Dictionary<int, SkillInfo> archerBuffDic = archerBuffInfo.ToDictionary(key => key.id, value => value);
        skillInfoDic.Add("Archer", archerBuffDic);

        textAsset = Resources.Load<TextAsset>("Data/archerBuff__config");
        jsonStr = textAsset.text;
        List<SkillConfig> archerBuffConfig = FullSerializerAPI.Deserialize(typeof(List<SkillConfig>), jsonStr) as List<SkillConfig>;
        Dictionary<int, SkillConfig> archerBuffConfigDic = archerBuffConfig.ToDictionary(key => key.id, value => value);
        skillConfigDic.Add("Archer", archerBuffConfigDic);

        textAsset = Resources.Load<TextAsset>("Data/archerBuff__trigger");
        jsonStr = textAsset.text;
        List<SkillTrigger> archerBuffTrigger = FullSerializerAPI.Deserialize(typeof(List<SkillTrigger>), jsonStr) as List<SkillTrigger>;
        Dictionary<int, SkillTrigger> archerBuffTriggerDic = archerBuffTrigger.ToDictionary(key => key.id, value => value);
        skillTriggerDic.Add("Archer", archerBuffTriggerDic);

        textAsset = Resources.Load<TextAsset>("Data/archerBuff__selector");
        jsonStr = textAsset.text;
        List<SkillSelector> archerBuffSelector = FullSerializerAPI.Deserialize(typeof(List<SkillSelector>), jsonStr) as List<SkillSelector>;
        Dictionary<int, SkillSelector> archerBuffSelectorDic = archerBuffSelector.ToDictionary(key => key.id, value => value);
        skillSelectorDic.Add("Archer", archerBuffSelectorDic);

        textAsset = Resources.Load<TextAsset>("Data/archerBuff__effect");
        jsonStr = textAsset.text;
        List<SkillEffect> archerBuffEffect = FullSerializerAPI.Deserialize(typeof(List<SkillEffect>), jsonStr) as List<SkillEffect>;
        Dictionary<int, SkillEffect> archerBuffEffectDic = archerBuffEffect.ToDictionary(key => key.id, value => value);
        skillEffectDic.Add("Archer", archerBuffEffectDic);

        textAsset = Resources.Load<TextAsset>("Data/masterBuff__info");
        jsonStr = textAsset.text;
        List<SkillInfo> masterBuffInfo = FullSerializerAPI.Deserialize(typeof(List<SkillInfo>), jsonStr) as List<SkillInfo>;
        Dictionary<int, SkillInfo> masterBuffDic = masterBuffInfo.ToDictionary(key => key.id, value => value);
        skillInfoDic.Add("Master", masterBuffDic);

        textAsset = Resources.Load<TextAsset>("Data/masterBuff__config");
        jsonStr = textAsset.text;
        List<SkillConfig> masterBuffConfig = FullSerializerAPI.Deserialize(typeof(List<SkillConfig>), jsonStr) as List<SkillConfig>;
        Dictionary<int, SkillConfig> masterBuffConfigDic = masterBuffConfig.ToDictionary(key => key.id, value => value);
        skillConfigDic.Add("Master", masterBuffConfigDic);

        textAsset = Resources.Load<TextAsset>("Data/masterBuff__trigger");
        jsonStr = textAsset.text;
        List<SkillTrigger> masterBuffTrigger = FullSerializerAPI.Deserialize(typeof(List<SkillTrigger>), jsonStr) as List<SkillTrigger>;
        Dictionary<int, SkillTrigger> masterBuffTriggerDic = masterBuffTrigger.ToDictionary(key => key.id, value => value);
        skillTriggerDic.Add("Master", masterBuffTriggerDic);

        textAsset = Resources.Load<TextAsset>("Data/masterBuff__selector");
        jsonStr = textAsset.text;
        List<SkillSelector> masterBuffSelector = FullSerializerAPI.Deserialize(typeof(List<SkillSelector>), jsonStr) as List<SkillSelector>;
        Dictionary<int, SkillSelector> masterBuffSelectorDic = masterBuffSelector.ToDictionary(key => key.id, value => value);
        skillSelectorDic.Add("Master", masterBuffSelectorDic);

        textAsset = Resources.Load<TextAsset>("Data/masterBuff__effect");
        jsonStr = textAsset.text;
        List<SkillEffect> masterBuffEffect = FullSerializerAPI.Deserialize(typeof(List<SkillEffect>), jsonStr) as List<SkillEffect>;
        Dictionary<int, SkillEffect> masterBuffEffectDic = masterBuffEffect.ToDictionary(key => key.id, value => value);
        skillEffectDic.Add("Master", masterBuffEffectDic);
        
        textAsset = Resources.Load<TextAsset>("Data/player__random_name");
        jsonStr = textAsset.text;
        names = FullSerializerAPI.Deserialize(typeof(List<NameData>), jsonStr) as List<NameData>;
        
        textAsset = Resources.Load<TextAsset>("Data/equip_strength_info");
        jsonStr = textAsset.text;
        List<EquipmentLevelProperty> equipmentLevelData = FullSerializerAPI.Deserialize(typeof(List<EquipmentLevelProperty>), jsonStr) as List<EquipmentLevelProperty>;
        equipmentLevelDic = equipmentLevelData.ToDictionary(key => key.level, value => value);
        
        textAsset = Resources.Load<TextAsset>("Data/equip_new_info");
        jsonStr = textAsset.text;
        List<EquipInfoData> equipmentData = FullSerializerAPI.Deserialize(typeof(List<EquipInfoData>), jsonStr) as List<EquipInfoData>;
        equipmentList = equipmentData;
        equipmentDic = equipmentData.GroupBy(e => e.type).ToDictionary(g => g.Key, g => g.ToList());
        
        textAsset = Resources.Load<TextAsset>("Data/AllSkillShow__info");
        jsonStr = textAsset.text;
        allskillShowInfoDataList = FullSerializerAPI.Deserialize(typeof(List<SkillInfoData>), jsonStr) as List<SkillInfoData>;
        
        textAsset = Resources.Load<TextAsset>("Data/daily_task_info");
        jsonStr = textAsset.text;
        dailyTaskList = FullSerializerAPI.Deserialize(typeof(List<TaskInfoData>), jsonStr) as List<TaskInfoData>;
        
        textAsset = Resources.Load<TextAsset>("Data/weekly_task_info");
        jsonStr = textAsset.text;
        weeklyTaskList = FullSerializerAPI.Deserialize(typeof(List<TaskInfoData>), jsonStr) as List<TaskInfoData>;
        
        textAsset = Resources.Load<TextAsset>("Data/seven_days_sign_reward");
        jsonStr = textAsset.text;
        sevenSignList = FullSerializerAPI.Deserialize(typeof(List<SevenSignInfo>), jsonStr) as List<SevenSignInfo>;
        
        textAsset = Resources.Load<TextAsset>("Data/shop_item_info");
        jsonStr = textAsset.text;
        shopItemList = FullSerializerAPI.Deserialize(typeof(List<ShopItemInfo>), jsonStr) as List<ShopItemInfo>;
        
        textAsset = Resources.Load<TextAsset>("Data/AFK_info");
        jsonStr = textAsset.text;
        AFKInfoList = FullSerializerAPI.Deserialize(typeof(List<AFKInfo>), jsonStr) as List<AFKInfo>;
        
        textAsset = Resources.Load<TextAsset>("Data/SensitiveWords");
        if (textAsset != null)
        {
            string[] words = textAsset.text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                forbiddenWords.Add(word.Trim().ToLower());
            }
            Debug.Log("加载屏蔽词数量: " + forbiddenWords.Count);
        }
        else
        {
            Debug.LogError("未找到屏蔽词文件");
        }
    }
}
