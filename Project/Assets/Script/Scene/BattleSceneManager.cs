using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleSceneManager : View
{
    public override string Name
    {
        get
        {
            return Consts.V_BattleScene;
        }
    }

    [SerializeField]
    private GameObject mainCamera;
    [SerializeField]
    private GameObject equipmentContainer;
    [SerializeField]
    private GameObject buffContainer;
    [SerializeField]
    private GameObject talentSkill;//天赋技能
    [SerializeField]
    private GameObject talentSkillMask;//天赋技能 遮罩
    [SerializeField]
    private GameObject commonSkill_1_Mask;//通用技能1——荆棘之甲 遮罩
    [SerializeField]
    private GameObject commonSkill_2_Mask;//通用技能2——魔法力场 遮罩
    [SerializeField]
    private GameObject commonSkill_3_Mask;//通用技能3——魔力膨胀 遮罩

    [SerializeField]
    private GameObject commonSkill_4_Mask;//通用技能4——全力一击 遮罩
    [SerializeField]
    private Text talentSkillText;//天赋技能 名称
    [SerializeField]
    private Text commonSkill_1_Text;//通用技能1——荆棘之甲 名称
    [SerializeField]
    private Text commonSkill_2_Text;//通用技能2——魔法力场 名称
    [SerializeField]
    private Text commonSkill_3_Text;//通用技能3——魔力膨胀 名称
    [SerializeField]
    private Text commonSkill_4_Text;//通用技能4——全力一击 名称
    [SerializeField]
    private GameObject commonSkillUnlock_1;// 通用技能1——荆棘之甲 未解锁时的遮罩
    [SerializeField]
    private GameObject commonSkillUnlock_2;// 通用技能2——魔法力场 未解锁时的遮罩
    [SerializeField]
    private GameObject commonSkillUnlock_3;// 通用技能3——魔力膨胀 未解锁时的遮罩
    [SerializeField]
    private GameObject commonSkillUnlock_4;// 通用技能4——全力一击 未解锁时的遮罩
    [SerializeField]
    private GameObject pauseBoard;
    [SerializeField]
    private GameObject PauseObj;// 暂停按钮
    [SerializeField]
    private GameObject setBoard;
    [SerializeField]
    private GameObject deadBoard;
    [SerializeField]
    private GameObject failBoard;
    [SerializeField]
    private GameObject successBoard;
    [SerializeField]
    private GameObject guidanceBoard;
    [SerializeField]
    private GameObject attacked;
    [SerializeField]
    private GameObject playerInfoBoard; //玩家信息面板
    [SerializeField]
    private RectTransform def;
    [SerializeField]
    private RectTransform hp;
    [SerializeField]
    private RectTransform exp;
    [SerializeField]
    private GameObject autoBattleBtn;// 自动战斗按钮
    [SerializeField]
    private List<Sprite> PauseButtonSprites;// 暂停按钮图标
    [SerializeField]
    private List<Sprite> talentSkillImg;//天赋技能图标
    [SerializeField]
    private List<Sprite> mapSprite;//关卡地图
    [SerializeField]
    private Image map;//关卡地图对象
    [SerializeField]
    private Text diamondNum;
    [SerializeField]
    private Text playerLevelNum;
    [SerializeField]
    private Text levelCount;
    [SerializeField]
    private GameObject countDown;
    [SerializeField]
    private GameObject currentDamage;
    [SerializeField]
    private GameObject maxDamage;
    [SerializeField]
    private Text stepNum;
    [SerializeField]
    private Slider stepSlider;
    [SerializeField]
    private GameObject headIconObj;// 角色头像
    [SerializeField]
    private List<Sprite> headIconSprites;// 角色头像
    [SerializeField]
    private List<Sprite> playerInfoSprites;// 玩家信息面板角色形象
    [SerializeField]
    private List<Sprite> soldierEquipmentSprites;
    [SerializeField]
    private List<Sprite> archerEquipmentSprites;
    [SerializeField]
    private List<Sprite> masterEquipmentSprites;
    [SerializeField]
    private List<Sprite> commonBuffSprites;
    [SerializeField]
    private List<Sprite> soldierBuffSprites;
    [SerializeField]
    private List<Sprite> archerBuffSprites;
    [SerializeField]
    private List<Sprite> masterBuffSprites;
    [SerializeField]
    private List<GameObject> equipmentSlots;
    [SerializeField]
    private List<GameObject> buffSlots;
    [SerializeField]
    private GameObject equipmentPrefab;
    [SerializeField]
    private GameObject buffPrefab;
    [SerializeField]
    private List<GameObject> characters;
    [SerializeField]
    private GameObject tile;
    [SerializeField]
    private GameObject warnBoard;
    [SerializeField]
    private GameObject challengeEndBoard;//领袖挑战结束弹窗
    [SerializeField]
    private List<GameObject> monsterPrefabList;// 怪物预制体

    public GameObject SpineSkillIce;//冰封术Spine
    public GameObject SpineSkillFire;//烈火
    public GameObject SpineSkillDark;//魔力汲取
    public GameObject SpineSkillCommon_1;//荆棘之甲
    public GameObject SpineSkillCommon_2;//魔法力场
    public GameObject SpineSkillCommon_3;//魔力膨胀
    public GameObject SpineSkillCommon_4;//全力一击

    private Dictionary<string, LevelData> currentLevelMonsterData = new Dictionary<string, LevelData>();// 当前关卡的怪物数据
    private LevelData currentMonsterInfo;
    private MonsterData currentMonsterData;
    private int currentMonsterId = 0;
    private int currentMonsterCount = 0;
    private int currentChapter = 0;//当前章节
    private int currentLevel = 0;//当前关卡

    private float offset = 38.4f;
    private int level;
    private int characterId;
    private GameObject character;
    private int monsterMaxCount = 0;//本关的怪物总数
    private int currentMonsterNum = 0;//当前已生成的怪物数量
    private int tileCount = 0;
    private int[] value = { -1, -1, -1 };

    private int timePercent = 0;

    private int currentHp = 0;//当前剩余生命值（分子）,会随着受伤和升级而变化，所以需要临时保存一下
    private int levelAllNum = 0;//全部关卡数量
    public bool isAutoBattle = false;//是否开启了自动战斗
    private Coroutine autoBattleCoroutine;

    private float totalTime = 90f; // 领袖挑战倒计时总时间（秒）
    private float currentTime;// 领袖挑战倒计时剩余时间（秒）
    private int challengeCurrentDamage = 0;//当前造成的伤害
    private Coroutine countdownCoroutine; // 存储协程引用

    public GameObject pauseButton;
    public Step6 guidanceBattleSuccess;

    // 技能冷却状态
    private Dictionary<string, bool> isSkillOnCooldown = new Dictionary<string, bool>()
    {
        { "冰封术", false },
        { "烈火", false },
        { "魔力汲取", false },
        { "荆棘之甲", false },
        { "魔法力场", false },
        { "魔力膨胀", false },
        { "全力一击", false }
    };

    private void Awake()
    {
        EventManager.Instance.AddListener(EventName.SetBuff, UpdateBuff);
        EventManager.Instance.AddListener(EventName.GetCoin, GetCoin);
        EventManager.Instance.AddListener(EventName.GetExp, GetExp);
        EventManager.Instance.AddListener(EventName.CharacterHpChange, OnHpSlider);
        EventManager.Instance.AddListener(EventName.CharacterCurrentHpChange, ChangeCurrentHp);
        EventManager.Instance.AddListener(EventName.CharacterMpChange, OnMpSlider);
        EventManager.Instance.AddListener(EventName.CharacterExpChange, OnExpSlider);
        EventManager.Instance.AddListener(EventName.AddBuffTime, AddBuffTime);
        EventManager.Instance.AddListener(EventName.MonsterDead, ChangeHaveMonsterState);
        // EventManager.Instance.AddListener(EventName.MonsterDead, UpdateStep);
        // EventManager.Instance.AddListener(EventName.CharacterMove, AddTile);
        EventManager.Instance.AddListener(EventName.Pass, Pass);
        EventManager.Instance.AddListener(EventName.CharacterAttacked, OnAttacked);
        EventManager.Instance.AddListener(EventName.ChangePauseButtonSprites, OnChangePauseSprites);
        EventManager.Instance.AddListener(EventName.InitMonster, InitMonster);
        EventManager.Instance.AddListener(EventName.SkillCountDown, OnSkillCountDown);
        EventManager.Instance.AddListener(EventName.CharacterExtraAtkChange, InitPlayerInfoBoard);
        EventManager.Instance.AddListener(EventName.AutoBattle, OnAutoReleaseSkill);
        EventManager.Instance.AddListener(EventName.UpdateChallengeDamage, OnUpdateChallengeDamage);
        Render();
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveListener(EventName.SetBuff, UpdateBuff);
        EventManager.Instance.RemoveListener(EventName.GetCoin, GetCoin);
        EventManager.Instance.RemoveListener(EventName.GetExp, GetExp);
        EventManager.Instance.RemoveListener(EventName.CharacterHpChange, OnHpSlider);
        EventManager.Instance.RemoveListener(EventName.CharacterCurrentHpChange, ChangeCurrentHp);
        EventManager.Instance.RemoveListener(EventName.CharacterMpChange, OnMpSlider);
        EventManager.Instance.RemoveListener(EventName.CharacterExpChange, OnExpSlider);
        EventManager.Instance.RemoveListener(EventName.AddBuffTime, AddBuffTime);
        EventManager.Instance.RemoveListener(EventName.MonsterDead, ChangeHaveMonsterState);
        // EventManager.Instance.RemoveListener(EventName.MonsterDead, UpdateStep);
        // EventManager.Instance.RemoveListener(EventName.CharacterMove, AddTile);
        EventManager.Instance.RemoveListener(EventName.Pass, Pass);
        EventManager.Instance.RemoveListener(EventName.CharacterAttacked, OnAttacked);
        EventManager.Instance.RemoveListener(EventName.ChangePauseButtonSprites, OnChangePauseSprites);
        EventManager.Instance.RemoveListener(EventName.InitMonster, InitMonster);
        EventManager.Instance.RemoveListener(EventName.SkillCountDown, OnSkillCountDown);
        EventManager.Instance.RemoveListener(EventName.CharacterExtraAtkChange, InitPlayerInfoBoard);
        EventManager.Instance.RemoveListener(EventName.AutoBattle, OnAutoReleaseSkill);
        EventManager.Instance.RemoveListener(EventName.UpdateChallengeDamage, OnUpdateChallengeDamage);
    }

    private void Render()
    {
        // 重置关卡怪物相关变量
        currentLevelMonsterData.Clear();
        currentMonsterInfo = null;
        currentMonsterData = null;
        currentMonsterId = 0;
        currentMonsterCount = 0;
        monsterMaxCount = 0;
        currentMonsterNum = 0;

        level = GetModel<GameModel>().CurLevel;//关卡序号
        characterId = GetModel<GameModel>().CurId;//角色ID

        //新手引导阶段不展示暂停按钮
        pauseButton.SetActive(!(GetModel<GameModel>().LevelType == 0 && GetModel<GameModel>().CurLevel == 1 && !GetModel<GameModel>().FinishGuidance));

        InitMonsterInfo();
        InitUI();
        InitScene();
        Utils.FadeOut();
        // if (GetModel<GameModel>().GuidanceId != -1)
        // {
        //     guidanceBoard.SetActive(true);
        //     guidanceBoard.GetComponent<BattleSceneGuidanceBoard>().Init();
        // }
        // else
        // {
        //     guidanceBoard.SetActive(false);
        //     // this.TriggerEvent(EventName.PassTile);
        // }
    }

    private void InitMonsterInfo()
    {
        currentLevelMonsterData.Clear();
        monsterMaxCount = 0;

        int processedLevel = 0;

        List<LevelData> chapterLevels = null;
        if (level >= 1 && level <= 20)
        {
            chapterLevels = DataManager.Instance.levelMonsterInfo[1].levels;
            processedLevel = level;
        }
        else if (level >= 21 && level <= 40)
        {
            chapterLevels = DataManager.Instance.levelMonsterInfo[2].levels;
            processedLevel = level - 20;
        }
        else if (level >= 41 && level <= 60)
        {
            chapterLevels = DataManager.Instance.levelMonsterInfo[3].levels;
            processedLevel = level - 40;
        }
        else if (level >= 61 && level <= 81)
        {
            chapterLevels = DataManager.Instance.levelMonsterInfo[4].levels;
            processedLevel = level - 60;
        }

        if (chapterLevels != null)
        {
            // { "levelId": "2-1", "monsters": [ { "monsterId": 108, "count": 5 }, { "monsterId": 109, "count": 5 } ] },
            foreach (var levelItem in chapterLevels)
            {
                if (levelItem.levelId.EndsWith("-" + processedLevel.ToString()))
                {
                    // 深拷贝LevelData和其monsters列表
                    LevelData copy = new LevelData();
                    copy.levelId = levelItem.levelId;
                    copy.monsters = new List<MonsterData>();
                    foreach (var m in levelItem.monsters)
                    {
                        copy.monsters.Add(new MonsterData { monsterId = m.monsterId, count = m.count });
                    }
                    currentLevelMonsterData.Add(copy.levelId, copy);
                }
            }
        }

        foreach (var item in currentLevelMonsterData)
        {
            // Debug.Log($"Key: {item.Key}, Value: {JsonUtility.ToJson(item.Value)}");
            foreach (var monster in item.Value.monsters)
            {
                monsterMaxCount += monster.count;
            }
        }
    }
    private void InitUI()
    {
        // def.Find("Image").GetComponent<Image>().sprite = propertyIcons[characterId];
        // def.Find("Slider").Find("Fill Area").Find("Fill").GetComponent<Image>().sprite = propertySliderFills[characterId];
        // string defNum = DataManager.Instance.upgradeInfoDic[characterId][GetModel<GameModel>().PropLevel[characterId]].mp.ToString();
        // def.Find("Num").GetComponent<Text>().text = defNum + "/" + defNum;
        // def.Find("Slider").GetComponent<Slider>().value = 1;

        //头像
        headIconObj.GetComponent<Image>().sprite = headIconSprites[characterId];

        // 血条
        string hpNum = GetModel<GameModel>().TotalProperty.hp.ToString();
        hp.Find("Num").GetComponent<Text>().text = hpNum + "/" + hpNum;
        hp.Find("Slider").GetComponent<Slider>().value = 1;

        // 经验条
        if (GetModel<GameModel>().PlayerLevel[characterId] == DataManager.Instance.expUpgradeInfoDic[characterId].Count)
        {
            exp.Find("Num").gameObject.SetActive(false);
            exp.Find("Slider").GetComponent<Slider>().value = 1;
        }
        else
        {
            string expNum = DataManager.Instance.expUpgradeInfoDic[characterId][GetModel<GameModel>().PlayerLevel[characterId]].expCost.ToString();
            exp.Find("Num").GetComponent<Text>().text = GetModel<GameModel>().Exp + "/" + expNum;
            exp.Find("Slider").GetComponent<Slider>().value = (float)GetModel<GameModel>().Exp / int.Parse(expNum);
        }

        // 关卡信息
        if (GetModel<GameModel>().LevelType == 0)
        {
            levelCount.gameObject.SetActive(true);
            countDown.SetActive(false);
            currentDamage.SetActive(false);
            maxDamage.SetActive(false);
            // levelCount.text = level == DataManager.Instance.levelInfoDic.Count ? "当前关卡" : $"第{level}关";

            if (level == 81)
            {
                currentChapter = 4;
                currentLevel = 20;
            }
            else
            {
                currentChapter = (level - 1) / 20 + 1;
                currentLevel = level % 20 == 0 ? 20 : level % 20;
            }
            levelCount.text = $"第{currentChapter}-" + $"{currentLevel}关";
            //战斗背景
            map.sprite = mapSprite[currentChapter - 1];
        }
        else
        {
            levelCount.gameObject.SetActive(false);
            countDown.SetActive(true);
            currentDamage.SetActive(true);
            maxDamage.SetActive(true);
            currentDamage.gameObject.transform.Find("damage").GetComponent<Text>().text = "造成伤害：0";
            maxDamage.gameObject.transform.Find("damage").GetComponent<Text>().text = $"最高伤害：{GetModel<GameModel>().LeaderChallengeDamage}";
            //战斗背景
            map.sprite = mapSprite[3];

            StartCountdown();
        }

        //暂停按钮图标
        this.TriggerEvent(EventName.ChangePauseButtonSprites, new ChangePauseButtonSpritesEventArgs() { isPause = false });

        // 技能栏信息
        talentSkill.GetComponent<Image>().sprite = talentSkillImg[characterId];
        if (GetModel<GameModel>().CurId == 0)
        {
            talentSkillText.text = DataManager.Instance.skillInfoDataDic["Ice"][0].skillName;
        }
        else if (GetModel<GameModel>().CurId == 1)
        {
            talentSkillText.text = DataManager.Instance.skillInfoDataDic["Fire"][0].skillName;
        }
        else if (GetModel<GameModel>().CurId == 2)
        {
            talentSkillText.text = DataManager.Instance.skillInfoDataDic["Dark"][0].skillName;
        }
        commonSkill_1_Text.text = DataManager.Instance.skillInfoDataDic["Common"][1].skillName;
        commonSkill_2_Text.text = DataManager.Instance.skillInfoDataDic["Common"][2].skillName;
        commonSkill_3_Text.text = DataManager.Instance.skillInfoDataDic["Common"][3].skillName;
        commonSkill_4_Text.text = DataManager.Instance.skillInfoDataDic["Common"][4].skillName;
        commonSkillUnlock_1.SetActive(GetModel<GameModel>().Skill3Level == 0);
        commonSkillUnlock_2.SetActive(GetModel<GameModel>().Skill4Level == 0);
        commonSkillUnlock_3.SetActive(GetModel<GameModel>().Skill1Level == 0);
        commonSkillUnlock_4.SetActive(GetModel<GameModel>().Skill2Level == 0);

        // 金币
        UpdateCoin();
    }
    private void InitScene()
    {
        if (GetModel<GameModel>().LevelType == 1)
            InitChallengeLevel();
        else
            InitNormalLevel();
    }

    private void InitNormalLevel()
    {
        // for(int i = 0; i <= monsterMaxCount; i++)
        // {
        //     GameObject go = ObjectPool.Instance.CreateObject(tile, new Vector3(0,-5.3f,offset*i), Quaternion.identity);
        //     go.GetComponent<Entity>().Init(i, level, characterId);
        // }
        this.TriggerEvent(EventName.InitMonster, new MonsterInitEventArgs() { monsterNum = 1 });
        // Debug.Log("InitMonster");
        // // MonsterInitEventArgs args = e as MonsterInitEventArgs;
        // // if (args.monsterNum > monsterMaxCount) return;
        // GameObject go = ObjectPool.Instance.CreateObject(tile, new Vector3(0, -5.3f, offset * 0), Quaternion.identity);
        // go.GetComponent<Entity>().Init(0, level, characterId);
        // currentMonsterNum++;
        InitCharacter();
    }

    private void InitChallengeLevel()
    {
        this.TriggerEvent(EventName.InitMonster);
        InitCharacter();
    }

    private void AddTile(object sender, EventArgs e)
    {
        if (level != DataManager.Instance.levelInfoDic.Count) return;
        tileCount++;
        GameObject go = ObjectPool.Instance.CreateObject(tile, new Vector3(0, -5.3f, offset * tileCount), Quaternion.identity);
        go.GetComponent<Entity>().Init(tileCount, level, characterId);
    }

    private void InitMonster(object sender, EventArgs e)
    {
        if (GetModel<GameModel>().LevelType == 1)
        {
            StartCoroutine(delaySpawnChallengeMonster());
        }
        else
        {
            MonsterInitEventArgs args = e as MonsterInitEventArgs;
            if (args.monsterNum > monsterMaxCount) return;
            // GameObject go = ObjectPool.Instance.CreateObject(tile, new Vector3(0, -5.3f, offset * 0), Quaternion.identity);
            // go.GetComponent<Entity>().Init(0, level, characterId);
            StartCoroutine(delaySpawnMonster());
        }
    }
    private IEnumerator delaySpawnChallengeMonster()
    {
        EventManager.Instance.TriggerEvent(EventName.StopPlayerAttack);
        yield return new WaitForSeconds(0.1f);
        SpawnChallengeMonster();
    }
    private IEnumerator delaySpawnMonster()
    {
        EventManager.Instance.TriggerEvent(EventName.StopPlayerAttack);
        yield return new WaitForSeconds(0.1f);
        SpawnMonster();
        currentMonsterNum++;
    }
    private void SpawnChallengeMonster()
    {
        InitMonsterPrefab(118);
    }
    private void SpawnMonster()
    {
        // MonsterWeight monsterWeight = DataManager.Instance.monsterWeightDic[level];
        // int[] monsterWeights = new int[] {monsterWeight.monster0, monsterWeight.monster1, monsterWeight.monster2, monsterWeight.monster3,
        //     monsterWeight.monster4,monsterWeight.monster5,monsterWeight.monster6,monsterWeight.monster7,monsterWeight.monster8,
        //     monsterWeight.monster9,monsterWeight.monster10,monsterWeight.monster11,monsterWeight.monster12,monsterWeight.monster13,
        //     monsterWeight.monster14,monsterWeight.monster15,monsterWeight.monster16,monsterWeight.monster17,monsterWeight.monster18,
        //     monsterWeight.monster19,monsterWeight.monster20,monsterWeight.monster21,monsterWeight.monster22,monsterWeight.monster23,
        //     monsterWeight.monster24,monsterWeight.monster25,monsterWeight.monster26,monsterWeight.monster27};
        // GameObject monsterPrefab = Utils.GetRandomObjectByWeight(monsterWeights, monsters);

        if (currentMonsterId != 0 && currentMonsterCount > 0)
        {
            // Debug.Log("currentMonsterId:" + currentMonsterId + "currentMonsterCount:" + currentMonsterCount);
            // 如果当前怪物还有剩余，则不生成新的怪物
            InitMonsterPrefab(currentMonsterId);
        }
        else
        {
            // 如果当前怪物数量为0，则从currentMonsterInfo中获取下一组怪物信息
            if (currentMonsterInfo != null && currentMonsterInfo.monsters != null && currentMonsterInfo.monsters.Count > 0)
            {
                currentMonsterData = currentMonsterInfo.monsters[0];
                currentMonsterInfo.monsters.RemoveAt(0);
                currentMonsterId = currentMonsterData.monsterId;
                currentMonsterCount = currentMonsterData.count;

                if (currentMonsterCount <= 0)
                {
                    return;
                }
                InitMonsterPrefab(currentMonsterId);
                return;
            }
            else
            {
                // 如果没有更多怪物，则从currentLevelMonsterData获取下一组怪物信息
                currentMonsterId = 0;
                currentMonsterCount = 0;
                GetNextMonster();
            }
        }
    }

    //实例化敌人预制体
    private void InitMonsterPrefab(int monsterId)
    {
        if (monsterId <= 0)
        {
            Debug.LogError("怪物ID无效: " + monsterId);
            return;
        }
        int monsterIndex = monsterId % 100 - 1;
        GameObject monsterPrefab = monsterPrefabList[monsterIndex];
        if (monsterPrefab == null)
        {
            Debug.LogError($"未找到敌人预制体: {monsterId}");
            return;
        }
        GameObject monster = ObjectPool.Instance.CreateObject(monsterPrefab, new Vector3(0, 3, 15), Quaternion.identity);
        monster.GetComponent<Entity>().Init(0, level, characterId);
        if (GetModel<GameModel>().LevelType != 1)
            currentMonsterCount--;
        GetModel<GameModel>().HaveMonster = true; // 有怪物存活 

    }

    private void ChangeHaveMonsterState(object sender, EventArgs e)
    {
        GetModel<GameModel>().HaveMonster = false;
        if (GetModel<GameModel>().LevelType == 1)
        {
            Time.timeScale = 0;
            challengeEndBoard.SetActive(true);
        }
    }
    private void GetNextMonster()
    {
        if (currentLevelMonsterData.Count == 0)
        {
            return;
        }

        // 获取第一个KeyValuePair
        var enumerator = currentLevelMonsterData.GetEnumerator();
        if (!enumerator.MoveNext()) return;

        // 获取第一个元素
        var firstPair = enumerator.Current;

        // 移除该元素
        currentLevelMonsterData.Remove(firstPair.Key);
        currentMonsterInfo = firstPair.Value;//{"levelId":"3-1","monsters":[{"monsterId":101,"count":2},{"monsterId":102,"count":2}]}
        if (currentMonsterInfo.monsters != null && currentMonsterInfo.monsters.Count > 0)
        {
            currentMonsterData = currentMonsterInfo.monsters[0];
            currentMonsterInfo.monsters.RemoveAt(0);
            currentMonsterId = currentMonsterData.monsterId;
            currentMonsterCount = currentMonsterData.count;

            if (currentMonsterCount <= 0)
            {
                return;
            }
            InitMonsterPrefab(currentMonsterId);
        }
    }

    private void InitCharacter()
    {
        character = Instantiate(characters[characterId]);
        int hp = GetModel<GameModel>().TotalProperty.hp;
        currentHp = hp;

        // int mp = DataManager.Instance.upgradeInfoDic[characterId][GetModel<GameModel>().PropLevel[characterId]].mp;
        // int atk = UnityEngine.Random.Range(DataManager.Instance.upgradeInfoDic[characterId][GetModel<GameModel>().AtkLevel[characterId]].atkMin,
        //     DataManager.Instance.upgradeInfoDic[characterId][GetModel<GameModel>().AtkLevel[characterId]].atkMax + 1);
        int atk = GetModel<GameModel>().TotalProperty.atk;
        double crit = GetModel<GameModel>().TotalProperty.critRate * 100;
        double dodge = GetModel<GameModel>().TotalProperty.dodgeRate * 100;
        double damageAdd = GetModel<GameModel>().TotalProperty.damageBonus + GetModel<GameModel>().TotalProperty.skillDamageBonus;
        double damageReduce = GetModel<GameModel>().TotalProperty.damageReduction + GetModel<GameModel>().TotalProperty.skillDamageReduction;
        character.GetComponent<CharacterBase>().Init(characterId, hp, 0, atk, crit, dodge, damageAdd, damageReduce, 0, 0);

        mainCamera.GetComponent<CinemachineVirtualCamera>().Follow = character.transform;
        UpdateEquipment();
        UpdateUpgrade();
        UpdateExp();

        //防止玩家刚进入战斗场景，敌人生成之前就进行攻击
        EventManager.Instance.TriggerEvent(EventName.StopPlayerAttack);
    }

    public void OnEquipmentToggleTrigger(bool isOn)
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        equipmentContainer.SetActive(isOn);
    }

    public void OnBuffToggleTrigger(bool isOn)
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        buffContainer.SetActive(isOn);
    }

    public void OnBtnPause()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        Time.timeScale = 0;
        pauseBoard.SetActive(true);
        this.TriggerEvent(EventName.ChangePauseButtonSprites, new ChangePauseButtonSpritesEventArgs() { isPause = true });
    }
    private void OnChangePauseSprites(object sender, EventArgs e)
    {
        ChangePauseButtonSpritesEventArgs args = e as ChangePauseButtonSpritesEventArgs;
        if (args.isPause)
        {
            PauseObj.GetComponent<Image>().sprite = PauseButtonSprites[0];
        }
        else
        {
            PauseObj.GetComponent<Image>().sprite = PauseButtonSprites[1];
        }
    }

    public void OnSetBtn()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        setBoard.SetActive(true);
    }

    public void OnShowWarn()
    {
        warnBoard.SetActive(true);
        Time.timeScale = 0;
    }
    public void OnCloseWarn()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        warnBoard.SetActive(false);
        Time.timeScale = 1;
        GameManager.Instance.ChangeScene(Scene.Loading);
    }

    private void Pass(object sender, EventArgs e)
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Success });
        int levelCount = DataManager.Instance.levelMonsterInfo[1].levels.Count + DataManager.Instance.levelMonsterInfo[2].levels.Count +
                        DataManager.Instance.levelMonsterInfo[3].levels.Count + DataManager.Instance.levelMonsterInfo[4].levels.Count; // 关卡总数,80
        if (GetModel<GameModel>().LevelType == 0 && GetModel<GameModel>().CurLevel == 1 && !GetModel<GameModel>().FinishGuidance)
        {
            guidanceBattleSuccess.OnComplete();
        }
        else
        {
            successBoard.SetActive(true);
        }

        if (GetModel<GameModel>().LevelType == 0)
        {
            if (GetModel<GameModel>().LevelCount <= levelCount && GetModel<GameModel>().LevelCount == GetModel<GameModel>().CurLevel)
            {
                GetModel<GameModel>().LevelCount++;
            }
            // else if (GetModel<GameModel>().CurLevel >= levelCount)
            // {
            //     // GetModel<GameModel>().LevelType = 1;
            //     // GetModel<GameModel>().LevelCount = 0;

            //     // GetModel<GameModel>().GuidanceId++;
            //     // Utils.FadeIn();
            //     // GameManager.Instance.ChangeScene(Scene.Main);
            //     // return;

            //     //通关后，从第一章节第一关重新开始
            //     GetModel<GameModel>().CurLevel = 1;
            //     GameManager.Instance.ChangeScene(Scene.Main);
            //     return;
            // }
        }

        Time.timeScale = 0;
        GlobalTaskCounter.Instance.AddDailyCount(DailyTask.Battle);
        GlobalTaskCounter.Instance.AddWeeklyCount(WeeklyTask.Battle);
        if (GetModel<GameModel>().LevelCount == levelCount + 1 && GetModel<GameModel>().CurLevel == 80)
        {
            //全部通关
            successBoard.transform.Find("GameObject").Find("CountineButton").gameObject.SetActive(false);
        }
        else
            successBoard.transform.Find("GameObject").Find("CountineButton").gameObject.SetActive(true);
    }

    private void OnAttacked(object sender, EventArgs e)
    {
        attacked.SetActive(false);
        StopCoroutine(Attacked());
        StartCoroutine(Attacked());
    }

    IEnumerator Attacked()
    {
        attacked.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        attacked.SetActive(false);
    }

    private void UpdateCoin()
    {
        if (diamondNum != null)
        {
            diamondNum.text = $"x{GetModel<GameModel>().Diamond}";
        }
    }

    private void UpdateEquipment()
    {
        if (SceneManager.GetActiveScene().name != "Battle") return;

        switch (characterId)
        {
            case 0:
                value = GetModel<GameModel>().SoldierEquipments;
                break;
            case 1:
                value = GetModel<GameModel>().ArcherEquipments;
                break;
            case 2:
                value = GetModel<GameModel>().MasterEquipments;
                break;
            default: break;
        }
        int dr = 0;

        for (int i = 0; i < value.Length; i++)
        {
            if (equipmentSlots[i].transform.childCount > 0) DestroyImmediate(equipmentSlots[i].transform.GetChild(0).gameObject);
            if (value[i] == -1) continue;
            EquipmentInfo info = DataManager.Instance.equipmentInfoDic[value[i]];
            GameObject equipment = Instantiate(equipmentPrefab);
            dr += info.dr;
            switch (GetModel<GameModel>().CurId)
            {
                case 0:
                    equipment.transform.Find("Icon").GetComponent<Image>().sprite = soldierEquipmentSprites[info.type];
                    break;
                case 1:
                    equipment.transform.Find("Icon").GetComponent<Image>().sprite = archerEquipmentSprites[info.type];
                    break;
                case 2:
                    equipment.transform.Find("Icon").GetComponent<Image>().sprite = masterEquipmentSprites[info.type];
                    break;
                default: break;
            }

            equipment.transform.Find("Name").GetComponent<Text>().text = info.name;
            equipment.transform.Find("Desc").GetComponent<Text>().text = info.desc;
            equipment.transform.SetParent(equipmentSlots[info.type].transform, false);
        }
        character.GetComponent<CharacterBase>().dr = dr;
    }

    private void UpdateUpgrade()
    {
        if (playerLevelNum != null)
        {
            playerLevelNum.text = $"等级：{GetModel<GameModel>().PlayerLevel[characterId]}";
        }
    }

    // private void UpdateHp()
    // {
    //     if (hpNum != null)
    //     {
    //         hpNum.text = $"HP：{GetModel<GameModel>().HpLevel[characterId]}";
    //     }
    // }

    private void UpdateExp()
    {
        if (GetModel<GameModel>().PlayerLevel[characterId] == DataManager.Instance.expUpgradeInfoDic[characterId].Count)
            return;

        this.TriggerEvent(EventName.CharacterExpChange, new CharacterExpChangeEventArgs()
        {
            exp = GetModel<GameModel>().Exp,
            percent = (float)GetModel<GameModel>().Exp / DataManager.Instance.expUpgradeInfoDic[characterId][GetModel<GameModel>().PlayerLevel[characterId]].expCost,
        });
    }

    private void UpdateBuff(object sender, EventArgs e)
    {
        SetBuffEventArgs args = e as SetBuffEventArgs;
        SkillInfo buffInfo = DataManager.Instance.skillInfoDic[args.type][args.id];
        GameObject buff = Instantiate(buffPrefab);
        switch (args.type)
        {
            case "Common":
                buff.transform.Find("Icon").GetComponent<Image>().sprite = commonBuffSprites[args.id];
                break;
            case "Soldier":
                buff.transform.Find("Icon").GetComponent<Image>().sprite = soldierBuffSprites[args.id];
                break;
            case "Archer":
                buff.transform.Find("Icon").GetComponent<Image>().sprite = archerBuffSprites[args.id];
                break;
            case "Master":
                buff.transform.Find("Icon").GetComponent<Image>().sprite = masterBuffSprites[args.id];
                break;
            default: break;
        }
        buff.transform.Find("Name").GetComponent<Text>().text = buffInfo.name;
        buff.transform.Find("Desc").GetComponent<Text>().text = buffInfo.desc;

        for (int i = 0; i < buffSlots.Count; i++)
        {
            if (buffSlots[i].transform.childCount > 0) continue;
            SkillDeployer deployer = character.AddComponent<SkillDeployer>();
            deployer.InitDeployer(args.id, args.type);
            buff.GetComponent<Buff>().Init(deployer);
            buff.transform.SetParent(buffSlots[i].transform, false);
            if (buffInfo.time != -1)
            {
                int time = buffInfo.time == 0 ? 0 : buffInfo.time + buffInfo.time * timePercent / 100;
                StartCoroutine(Countdown(buff, buffInfo.time));
            }
            break;
        }
    }

    IEnumerator Countdown(GameObject go, int time)
    {
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float remainingTime = time - elapsedTime;

            go.transform.Find("Mask").GetComponent<Image>().fillAmount = 1 - (remainingTime / time);

            yield return null;
        }
        Destroy(go);
    }

    private void UpdateStep(object sender, EventArgs e)
    {
        MonsterDeadEventArgs args = e as MonsterDeadEventArgs;
        if (args.currentMonsterNum > monsterMaxCount) stepNum.text = monsterMaxCount.ToString();
        else stepNum.text = args.currentMonsterNum.ToString();
        levelAllNum = DataManager.Instance.levelMonsterInfo[1].levels.Count +
            DataManager.Instance.levelMonsterInfo[2].levels.Count +
            DataManager.Instance.levelMonsterInfo[3].levels.Count +
            DataManager.Instance.levelMonsterInfo[4].levels.Count;
        if (level <= levelAllNum)
            stepSlider.value = (float)args.currentMonsterNum / monsterMaxCount;
    }

    private void GetCoin(object sender, EventArgs e)
    {
        GetCoinEventArgs args = e as GetCoinEventArgs;
        int coin = GetModel<GameModel>().Coin + args.coin;
        SendViewEvent(Consts.E_ChangeCoin, new ChangeCoin() { coin = coin });
    }
    private void GetExp(object sender, EventArgs e)
    {
        GetExpEventArgs args = e as GetExpEventArgs;
        int exp = GetModel<GameModel>().Exp + args.exp;
        int remainExp = CheckUpgrade(exp);
        if (GetModel<GameModel>().PlayerLevel[characterId] < DataManager.Instance.expUpgradeInfoDic[characterId].Count)
            SendViewEvent(Consts.E_ChangeExp, new ChangeExp() { exp = remainExp });
    }

    //先升级，再存数据
    private int CheckUpgrade(int exp)
    {
        int remainExp = 0;
        if (GetModel<GameModel>().PlayerLevel[characterId] < DataManager.Instance.expUpgradeInfoDic[characterId].Count)
        {
            //未满级
            int nextLevelExp = DataManager.Instance.expUpgradeInfoDic[characterId][GetModel<GameModel>().PlayerLevel[characterId]].expCost;
            if (exp >= nextLevelExp)
            {
                //满足升级条件,等级先+1
                GetModel<GameModel>().PlayerLevel[0]++;
                GetModel<GameModel>().PlayerLevel[1]++;
                GetModel<GameModel>().PlayerLevel[2]++;
                // 每四级增加一个技能点
                if (GetModel<GameModel>().PlayerLevel[characterId] % 4 == 0)
                {
                    GetModel<GameModel>().SkillPoint++;
                }
                // 升级后剩余的经验值
                remainExp = exp - nextLevelExp;
                this.TriggerEvent(EventName.CharacterExpChange, new CharacterExpChangeEventArgs()
                {
                    exp = remainExp,
                    percent = (float)remainExp /
                    DataManager.Instance.expUpgradeInfoDic[characterId][GetModel<GameModel>().PlayerLevel[characterId]].expCost
                });

                //升级后的基础生命值
                int hpCurrent = DataManager.Instance.expUpgradeInfoDic[characterId][GetModel<GameModel>().PlayerLevel[characterId] - 1].hp;
                //升级后，增加的基础生命值
                int hpAdd = hpCurrent - DataManager.Instance.expUpgradeInfoDic[characterId][GetModel<GameModel>().PlayerLevel[characterId] - 2].hp;
                //升级增加的生命值+当前剩余的生命值，作为血量的分子
                int realHp = hpAdd + currentHp;
                if (realHp >= hpCurrent)
                {
                    realHp = hpCurrent;
                }
                //更新血量UI
                this.TriggerEvent(EventName.CharacterHpChange, new CharacterHpChangeEventArgs()
                {
                    hp = realHp,
                    percent = (float)realHp / hpCurrent,
                });
                // 同步血量
                currentHp = realHp;
                if (character != null)
                    character.GetComponent<CharacterBase>().Hp = realHp;
                //同步攻击力
                int atkCurrent = GetModel<GameModel>().TotalProperty.atk;
                if (character != null)
                    character.GetComponent<CharacterBase>().Atk = atkCurrent;
                //同步闪避
                double dodgeCurrent = GetModel<GameModel>().TotalProperty.dodgeRate * 100;
                if (character != null)
                    character.GetComponent<CharacterBase>().Dodge = dodgeCurrent;
                //同步暴击
                double criticalCurrent = GetModel<GameModel>().TotalProperty.critRate * 100;
                if (character != null)
                    character.GetComponent<CharacterBase>().Crit = criticalCurrent;
                //同步增伤
                double damageBonusCurrent = GetModel<GameModel>().TotalProperty.damageBonus + GetModel<GameModel>().TotalProperty.skillDamageBonus;
                if (character != null)
                    character.GetComponent<CharacterBase>().DamageAdd = damageBonusCurrent;
                //同步减伤
                double damageReductionCurrent = GetModel<GameModel>().TotalProperty.damageReduction + GetModel<GameModel>().TotalProperty.skillDamageReduction;
                if (character != null)
                    character.GetComponent<CharacterBase>().DamageReduce = damageReductionCurrent;
                //更新玩家信息UI
                if (playerInfoBoard.activeSelf)
                {
                    InitPlayerInfoBoard(null, null);
                }
            }
            else
            {
                remainExp = exp;
            }
        }
        else
        {
            remainExp = exp;
        }
        return remainExp;
    }
    private void OnHpSlider(object sender, EventArgs e)
    {
        CharacterHpChangeEventArgs args = e as CharacterHpChangeEventArgs;
        hp.Find("Num").GetComponent<Text>().text = args.hp.ToString() + "/" + GetModel<GameModel>().TotalProperty.hp.ToString();
        hp.Find("Slider").GetComponent<Slider>().value = args.percent;
        if (args.hp <= 0)
        {
            Time.timeScale = 0;
            if (GetModel<GameModel>().LevelType == 1)
                challengeEndBoard.SetActive(true);
            else
                deadBoard.SetActive(true);
        }
    }

    // 原项目功能，现已废弃 
    private void OnMpSlider(object sender, EventArgs e)
    {
        CharacterMpChangeEventArgs args = e as CharacterMpChangeEventArgs;
        def.Find("Num").GetComponent<Text>().text = args.mp.ToString() + "/" +
            DataManager.Instance.upgradeInfoDic[characterId][GetModel<GameModel>().PropLevel[characterId]].mp.ToString();
        def.Find("Slider").GetComponent<Slider>().value = args.percent;
    }

    private void OnExpSlider(object sender, EventArgs e)
    {
        if (GetModel<GameModel>().PlayerLevel[characterId] >= DataManager.Instance.expUpgradeInfoDic[characterId].Count)
        {
            exp.Find("Num").gameObject.SetActive(false);
            exp.Find("Slider").GetComponent<Slider>().value = 1;
            return;
        }
        CharacterExpChangeEventArgs args = e as CharacterExpChangeEventArgs;
        exp.Find("Num").GetComponent<Text>().text = args.exp.ToString() + "/" +
            DataManager.Instance.expUpgradeInfoDic[characterId][GetModel<GameModel>().PlayerLevel[characterId]].expCost.ToString();
        exp.Find("Slider").GetComponent<Slider>().value = args.percent;
    }

    private void AddBuffTime(object sender, EventArgs e)
    {
        AddBuffTimeEventArgs args = e as AddBuffTimeEventArgs;
        timePercent += args.time;
    }

    public override void RegisterViewEvent()
    {
        base.RegisterViewEvent();
        attractEventList.Add(Consts.E_UpdateCoin);
        attractEventList.Add(Consts.E_UpdateEquipment);
        attractEventList.Add(Consts.E_UpdateHp);
        attractEventList.Add(Consts.E_UpdateExp);
    }

    public override void HandleEvent(object data = null)
    {
        UpdateCoin();
        UpdateEquipment();
        UpdateUpgrade();
        // UpdateHp();
        UpdateExp();
    }

    //点击释放技能
    public void OnClickSkill(int skillIndex)
    {
        //如果场景中没有敌人或者玩家已死亡或者就不能释放技能
        if (!GetModel<GameModel>().HaveMonster || character.GetComponent<CharacterBase>().Hp <= 0) return;
        // this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        SkillInfoData skillInfoData = null;
        if (skillIndex == 0)
        {
            if (GetModel<GameModel>().CurId == 0)
            {
                skillInfoData = DataManager.Instance.skillInfoDataDic["Ice"][skillIndex];
            }
            else if (GetModel<GameModel>().CurId == 1)
            {
                skillInfoData = DataManager.Instance.skillInfoDataDic["Fire"][skillIndex];
            }
            else if (GetModel<GameModel>().CurId == 2)
            {
                skillInfoData = DataManager.Instance.skillInfoDataDic["Dark"][skillIndex];
            }
        }
        else
        {
            skillInfoData = DataManager.Instance.skillInfoDataDic["Common"][skillIndex];
        }
        if (skillInfoData == null) return;
        // 检查技能是否在冷却中
        if (isSkillOnCooldown.ContainsKey(skillInfoData.skillName) && isSkillOnCooldown[skillInfoData.skillName]) return;

        switch (skillIndex)
        {
            case 0:
                if (GetModel<GameModel>().CurId == 0)
                {
                    //冰封
                    this.TriggerEvent(EventName.IceSkill, new SkillEventArgs() { skillName = skillInfoData.skillName, cooldown = skillInfoData.cooldown, duration = skillInfoData.duration });
                }
                else if (GetModel<GameModel>().CurId == 1)
                {
                    //烈火
                    this.TriggerEvent(EventName.FireSkill, new SkillEventArgs() { skillName = skillInfoData.skillName, cooldown = skillInfoData.cooldown, duration = skillInfoData.duration });
                }
                else if (GetModel<GameModel>().CurId == 2)
                {
                    //魔力汲取
                    this.TriggerEvent(EventName.DarkSkill, new SkillEventArgs() { skillName = skillInfoData.skillName, cooldown = skillInfoData.cooldown, duration = skillInfoData.duration });
                }
                break;
            case 1:
                //荆棘之甲
                if (GetModel<GameModel>().Skill3Level == 0) return;// 未解锁
                this.TriggerEvent(EventName.CommonSkill_1, new SkillEventArgs() { skillName = skillInfoData.skillName, cooldown = skillInfoData.cooldown, duration = skillInfoData.duration });
                break;
            case 2:
                //魔法力场
                if (GetModel<GameModel>().Skill4Level == 0) return;// 未解锁
                this.TriggerEvent(EventName.CommonSkill_2, new SkillEventArgs() { skillName = skillInfoData.skillName, cooldown = skillInfoData.cooldown, duration = skillInfoData.duration });
                break;
            case 3:
                //魔力膨胀
                if (GetModel<GameModel>().Skill1Level == 0) return;// 未解锁
                this.TriggerEvent(EventName.CommonSkill_3, new SkillEventArgs() { skillName = skillInfoData.skillName, cooldown = skillInfoData.cooldown, duration = skillInfoData.duration });
                break;
            case 4:
                //全力一击
                if (GetModel<GameModel>().Skill2Level == 0) return;// 未解锁
                this.TriggerEvent(EventName.CommonSkill_4, new SkillEventArgs() { skillName = skillInfoData.skillName, cooldown = skillInfoData.cooldown, duration = skillInfoData.duration });
                break;
            default:
                break;
        }
    }

    // 技能倒计时事件处理
    private void OnSkillCountDown(object sender, EventArgs e)
    {
        SkillEventArgs args = e as SkillEventArgs;
        if (args == null) return;

        // 更新技能倒计时UI
        StartCoroutine(StartCooldown(args.skillName, args.cooldown));

        // Debug.Log($"技能名称：{args.skillName}，技能倒计时: {args.cooldown}秒, 持续时间: {args.duration}秒");
    }
    IEnumerator StartCooldown(string skillName, int cooldownTime)
    {
        GameObject currentMask = null;
        if (skillName == "冰封术" || skillName == "烈火" || skillName == "魔力汲取")
        {
            if (talentSkillMask != null) talentSkillMask.SetActive(true);
            currentMask = talentSkillMask;
        }
        else if (skillName == "荆棘之甲")
        {
            if (commonSkill_1_Mask != null) commonSkill_1_Mask.SetActive(true);
            currentMask = commonSkill_1_Mask;
        }
        else if (skillName == "魔法力场")
        {
            if (commonSkill_2_Mask != null) commonSkill_2_Mask.SetActive(true);
            currentMask = commonSkill_2_Mask;
        }
        else if (skillName == "魔力膨胀")
        {
            if (commonSkill_3_Mask != null) commonSkill_3_Mask.SetActive(true);
            currentMask = commonSkill_3_Mask;
        }
        else if (skillName == "全力一击")
        {
            if (commonSkill_4_Mask != null) commonSkill_4_Mask.SetActive(true);
            currentMask = commonSkill_4_Mask;
        }
        float timer = cooldownTime;
        if (isSkillOnCooldown[skillName])
        {
            // 如果技能正在冷却，直接返回
            yield break;
        }
        isSkillOnCooldown[skillName] = true;
        while (timer > 0)
        {
            timer -= Time.deltaTime;

            if (currentMask != null)
            {
                currentMask.GetComponent<Image>().fillAmount = timer / cooldownTime;
                if (timer <= 0)
                {
                    currentMask.SetActive(false);
                }
            }
            yield return null;
        }
        isSkillOnCooldown[skillName] = false;
        if (currentMask != null) currentMask.GetComponent<Image>().fillAmount = 0;
    }

    //自动战斗（开启后，自动释放技能）
    public void OnAutoBattle()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        if (!isAutoBattle)
        {
            isAutoBattle = true;
            autoBattleBtn.transform.Find("text").GetComponent<Text>().text = "取消自动战斗";
            this.TriggerEvent(EventName.AutoBattle, new AutoBattleEventArgs() { isAutoBattle = true });
        }
        else
        {
            isAutoBattle = false;
            autoBattleBtn.transform.Find("text").GetComponent<Text>().text = "开启自动战斗";
            this.TriggerEvent(EventName.AutoBattle, new AutoBattleEventArgs() { isAutoBattle = false });
        }
    }

    private void OnAutoReleaseSkill(object sender, EventArgs e)
    {
        AutoBattleEventArgs args = e as AutoBattleEventArgs;
        if (args.isAutoBattle)
        {
            if (autoBattleCoroutine == null)
                autoBattleCoroutine = StartCoroutine(AutoReleaseSkillCoroutine());
        }
        else
        {
            if (autoBattleCoroutine != null)
            {
                StopCoroutine(autoBattleCoroutine);
                autoBattleCoroutine = null;
            }
        }
    }

    private IEnumerator AutoReleaseSkillCoroutine()
    {
        while (isAutoBattle)
        {
            OnClickSkill(0);
            OnClickSkill(1);
            OnClickSkill(2);
            OnClickSkill(3);
            OnClickSkill(4);
            yield return new WaitForSeconds(1f); // Adjust interval as needed
        }
    }

    public int GetCurrentMonsterNum()
    {
        return currentMonsterNum;
    }
    public int GetMaxMonsterNum()
    {
        return monsterMaxCount;
    }

    //展示玩家信息
    public void OnBtnPlayerInfo()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        playerInfoBoard.SetActive(true);
        // playerInfoBoard.GetComponent<PlayerInfoBoard>().Init();
        InitPlayerInfoBoard(null, null);
    }
    private void InitPlayerInfoBoard(object sender, EventArgs e)
    {
        // 角色形象
        playerInfoBoard.transform.Find("player").GetComponent<Image>().sprite = playerInfoSprites[characterId];
        //屬性信息
        playerInfoBoard.transform.Find("life").Find("lifeNum").GetComponent<Text>().text = GetModel<GameModel>().TotalProperty.hp.ToString();
        playerInfoBoard.transform.Find("attack").Find("attackNum").GetComponent<Text>().text = (GetModel<GameModel>().TotalProperty.atk + character.GetComponent<CharacterBase>().ExtraAtk).ToString();
        double crit = GetModel<GameModel>().TotalProperty.critRate * 100;
        if (crit == 0)
        {
            playerInfoBoard.transform.Find("critical").Find("criticalNum").GetComponent<Text>().text = "0";
        }
        else
        {
            playerInfoBoard.transform.Find("critical").Find("criticalNum").GetComponent<Text>().text = crit + "%";
        }
        double dodge = GetModel<GameModel>().TotalProperty.dodgeRate * 100;
        if (dodge == 0)
        {
            playerInfoBoard.transform.Find("dodge").Find("dodgeNum").GetComponent<Text>().text = "0";
        }
        else
        {
            playerInfoBoard.transform.Find("dodge").Find("dodgeNum").GetComponent<Text>().text = dodge + "%";
        }
    }
    public void OnClosePlayerInfo()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        playerInfoBoard.SetActive(false);
    }

    public void ChangeCurrentHp(object sender, EventArgs e)
    {
        CharacterCurrentHpChangeEventArgs args = e as CharacterCurrentHpChangeEventArgs;
        currentHp = args.currentHp;
    }

    //领袖挑战倒计时
    public void StartCountdown()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine); // 防止重复启动
        }
        countdownCoroutine = StartCoroutine(Countdown());
    }
    IEnumerator Countdown()
    {
        currentTime = totalTime;
        UpdateCountdownDisplay();

        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f); // 每秒更新一次
            currentTime -= 1f;
            UpdateCountdownDisplay();
        }

        // 倒计时结束
        OnCountdownEnd();
    }
    // 更新UI显示
    void UpdateCountdownDisplay()
    {
        countDown.gameObject.transform.Find("time").GetComponent<Text>().text = $"{currentTime}秒";
    }

    // 倒计时结束事件
    public void OnCountdownEnd()
    {
        Time.timeScale = 0;
        challengeEndBoard.SetActive(true);
    }

    public void OnUpdateChallengeDamage(object sender, EventArgs e)
    {
        if (challengeEndBoard.activeSelf) return;
        ChallengeDamageArgs args = e as ChallengeDamageArgs;
        challengeCurrentDamage += args.challengeDamage;
        currentDamage.gameObject.transform.Find("damage").GetComponent<Text>().text = $"造成伤害：{challengeCurrentDamage}";
        if (challengeCurrentDamage > GetModel<GameModel>().LeaderChallengeDamage)
        {
            GetModel<GameModel>().LeaderChallengeDamage = challengeCurrentDamage;
            maxDamage.gameObject.transform.Find("damage").GetComponent<Text>().text = $"最高伤害：{GetModel<GameModel>().LeaderChallengeDamage}";
        }
    }
    public int GetChallengeCurrentDamage()
    {
        return challengeCurrentDamage;
    }
}