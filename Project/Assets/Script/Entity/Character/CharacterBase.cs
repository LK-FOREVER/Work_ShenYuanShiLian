using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CharacterBase : MonoBehaviour
{
    public float offset = 38.4f;
    public int characterId;
    public int dr = 0;

    public int curPos = 0;
    public int extraAtk;

    protected int initHp;
    protected int hp;
    protected int initMp;
    protected int mp;
    protected int atk;
    protected double crit;
    protected double dodge;
    protected double damageAdd;//增伤
    protected double damageReduce;//减伤
    protected bool moving = false;

    public int curGoodId = 30;

    public bool isAutoReleaseSkill = false;
    public bool canAttack = true;     // 攻击冷却状态
    public float atkSpeed = 1.5f; // 攻击速度1.5秒

    private int fireSkillAddedAtk = 0; // 记录烈火技能增加的攻击力
    public bool startCommonSkill_1 = false;// 记录是否释放了普通技能1-荆棘之甲
    public int shieldAmount = 0; // 魔法力场技能的可吸收的伤害值
    public int continuousAttackDamage = 0; // 魔力膨胀技能的伤害值
    public bool startCommonSkill_3 = false;// 记录是否释放了普通技能3-魔力膨胀
    public int CommonSkill_3_count = 0; // 魔力膨胀技能的攻击次数
    public bool startCommonSkill_4 = false;// 记录是否释放了普通技能4-全力一击
    public int CommonSkill_4_Damage = 0; // 全力一击技能的伤害值

    private Coroutine attackCoroutine;
    private Coroutine fireCoroutine;
    private Coroutine darkCoroutine;
    private Coroutine commonSkill_1_Coroutine;
    private Coroutine commonSkill_2_Coroutine;
    private Coroutine commonSkill_3_Coroutine;
    private Coroutine commonSkill_4_Coroutine;

    private BattleSceneManager battleSceneManager;
    private GameObject skillSpineFire;
    private GameObject skillSpineDark;
    private GameObject SpineSkillCommon_1;
    private GameObject SpineSkillCommon_2;
    private GameObject SpineSkillCommon_3;
    private GameObject SpineSkillCommon_4;

    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value >= 0 ? value : 0;
            initHp = Mvc.GetModel<GameModel>().TotalProperty.hp;
            if (hp > initHp) hp = initHp;
            this.TriggerEvent(EventName.CharacterCurrentHpChange, new CharacterCurrentHpChangeEventArgs() { currentHp = hp });
            if (hp <= 0 && skillSpineFire != null)
            {
                Destroy(skillSpineFire);
            }
            this.TriggerEvent(EventName.CharacterHpChange, new CharacterHpChangeEventArgs() { hp = hp, percent = (float)hp / initHp });
        }
    }

    public int Mp
    {
        get
        {
            return mp;
        }
        set
        {
            mp = value >= 0 ? value : 0;
            if (mp > initMp) mp = initMp;
            this.TriggerEvent(EventName.CharacterMpChange, new CharacterMpChangeEventArgs() { mp = mp, percent = (float)mp / initMp });
        }
    }

    public int Atk
    {
        get
        {
            return atk;
        }
        set
        {
            atk = value;
        }
    }
    public int ExtraAtk
    {
        get
        {
            return extraAtk;
        }
        set
        {
            extraAtk = value;
            this.TriggerEvent(EventName.CharacterExtraAtkChange);
        }
    }
    public double Crit
    {
        get
        {
            return crit;
        }
        set
        {
            crit = value;
        }
    }
    public double Dodge
    {
        get
        {
            return dodge;
        }
        set
        {
            dodge = value;
        }
    }
    public double DamageAdd
    {
        get
        {
            return damageAdd;
        }
        set
        {
            damageAdd = value;
        }
    }
    public double DamageReduce
    {
        get
        {
            return damageReduce;
        }
        set
        {
            damageReduce = value;
        }
    }

    protected void Awake()
    {
        EventManager.Instance.AddListener(EventName.MonsterAttack, UnderAttack);
        EventManager.Instance.AddListener(EventName.PassTile, Move);
        EventManager.Instance.AddListener(EventName.RestoreHp, RestoreHp);
        EventManager.Instance.AddListener(EventName.CharacterResurge, Resurge);
        EventManager.Instance.AddListener(EventName.AttackCommand, SendAttackCommond);
        EventManager.Instance.AddListener(EventName.ShieldCommand, SendDeffendCommond);
        // EventManager.Instance.AddListener(EventName.CharacterMoveEnd, ChangeGoodId);
        EventManager.Instance.AddListener(EventName.MonsterDead, StopAttack);
        // EventManager.Instance.AddListener(EventName.InitMonster, StartAttack);
        EventManager.Instance.AddListener(EventName.StopPlayerAttack, StopAttack);
        EventManager.Instance.AddListener(EventName.StartPlayerAttack, StartAttack);
        EventManager.Instance.AddListener(EventName.FireSkill, OnFireSkill);
        EventManager.Instance.AddListener(EventName.DarkSkill, OnDarkSkill);
        EventManager.Instance.AddListener(EventName.CommonSkill_1, OnCommonSkill_1);
        EventManager.Instance.AddListener(EventName.CommonSkill_2, OnCommonSkill_2);
        EventManager.Instance.AddListener(EventName.EndCommonSkill_2, EndCommonSkill_2);
        EventManager.Instance.AddListener(EventName.CommonSkill_3, OnCommonSkill_3);
        EventManager.Instance.AddListener(EventName.EndCommonSkill_3, EndCommonSkill_3);
        EventManager.Instance.AddListener(EventName.CommonSkill_4, OnCommonSkill_4);
        EventManager.Instance.AddListener(EventName.EndCommonSkill_4, EndCommonSkill_4);
        EventManager.Instance.AddListener(EventName.AutoBattle, ChangeAutoState);
    }

    void Start()
    {
        battleSceneManager = GameObject.Find("Canvas").GetComponent<BattleSceneManager>();

        attackCoroutine = StartCoroutine(AttackRoutine());
    }

    protected void OnDestroy()
    {
        EventManager.Instance.RemoveListener(EventName.MonsterAttack, UnderAttack);
        EventManager.Instance.RemoveListener(EventName.PassTile, Move);
        EventManager.Instance.RemoveListener(EventName.RestoreHp, RestoreHp);
        EventManager.Instance.RemoveListener(EventName.CharacterResurge, Resurge);
        EventManager.Instance.RemoveListener(EventName.AttackCommand, SendAttackCommond);
        EventManager.Instance.RemoveListener(EventName.ShieldCommand, SendDeffendCommond);
        // EventManager.Instance.RemoveListener(EventName.CharacterMoveEnd, ChangeGoodId);
        EventManager.Instance.RemoveListener(EventName.MonsterDead, StopAttack);
        // EventManager.Instance.RemoveListener(EventName.InitMonster, StartAttack);
        EventManager.Instance.RemoveListener(EventName.StopPlayerAttack, StopAttack);
        EventManager.Instance.RemoveListener(EventName.StartPlayerAttack, StartAttack);
        EventManager.Instance.RemoveListener(EventName.FireSkill, OnFireSkill);
        EventManager.Instance.RemoveListener(EventName.DarkSkill, OnDarkSkill);
        EventManager.Instance.RemoveListener(EventName.CommonSkill_1, OnCommonSkill_1);
        EventManager.Instance.RemoveListener(EventName.CommonSkill_2, OnCommonSkill_2);
        EventManager.Instance.RemoveListener(EventName.EndCommonSkill_2, EndCommonSkill_2);
        EventManager.Instance.RemoveListener(EventName.CommonSkill_3, OnCommonSkill_3);
        EventManager.Instance.RemoveListener(EventName.EndCommonSkill_3, EndCommonSkill_3);
        EventManager.Instance.RemoveListener(EventName.CommonSkill_4, OnCommonSkill_4);
        EventManager.Instance.RemoveListener(EventName.EndCommonSkill_4, EndCommonSkill_4);
    }

    public virtual void Init(int _id, int _hp, int _mp, int _atk, double _crit, double _dodge, double _damageAdd, double _damageReduce, int _curPos, int _dr)

    {
        characterId = _id;
        hp = _hp;
        mp = _mp;
        atk = _atk;
        crit = _crit;
        dodge = _dodge;
        damageAdd = _damageAdd;
        damageReduce = _damageReduce;
        curPos = _curPos;
        dr = _dr;

        initHp = _hp;
        initMp = _mp;

        InitStateMachine();
    }

    protected virtual void InitStateMachine() { }

    protected virtual void SendAttackCommond(object sender, EventArgs e) { }
    protected virtual void SendDeffendCommond(object sender, EventArgs e) { }

    protected virtual void UnderAttack(object sender, EventArgs e) { }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (canAttack)
            {
                PerformAttack();
                canAttack = false;
                yield return new WaitForSeconds(atkSpeed);
                canAttack = true;
            }
            yield return null; // 每帧检测
        }
    }

    void PerformAttack()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Attack });
        this.TriggerEvent(EventName.AttackCommand);
    }

    private void Move(object sender, EventArgs e)
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Move });

        moving = true;
        transform.DOMoveZ(transform.position.z + offset, .5f)
            .OnComplete(() =>
            {
                moving = false;
                curPos++;
                this.TriggerEvent(EventName.CharacterMove, new CharacterMoveEventArgs() { pos = curPos });
            });
    }
    #region 烈火技能
    // 烈火技能
    protected void OnFireSkill(object sender, EventArgs e)
    {
        SkillEventArgs args = e as SkillEventArgs;
        if (args == null) return;

        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
        }
        fireCoroutine = StartCoroutine(FireTalent(args.skillName, args.cooldown, args.duration));
    }
    private IEnumerator FireTalent(string skillName, int cooldown, int duration)
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.SkillFire });
        //展示spine动画
        if (battleSceneManager != null)
        {
            skillSpineFire = Instantiate(battleSceneManager.SpineSkillFire, new Vector3(transform.position.x, transform.position.y - 3, transform.position.z), Quaternion.identity);
        }
        // gameObject.transform.Find("Wand").GetComponent<SkeletonMecanim>().skeleton.SetColor(new Color(1, 1, 1, 0.8f));
        //技能效果
        ChangeExtraAtk(30); // 增加30%的额外攻击力
        //技能倒计时
        this.TriggerEvent(EventName.SkillCountDown, new SkillEventArgs() { skillName = skillName, cooldown = cooldown, duration = duration });
        yield return new WaitForSeconds(duration);
        //技能结束
        // 移除spine动画
        if (skillSpineFire != null)
        {
            Destroy(skillSpineFire);
        }
        ExtraAtk -= fireSkillAddedAtk; // 重置额外攻击力
        // gameObject.transform.Find("Wand").GetComponent<SkeletonMecanim>().skeleton.SetColor(new Color(1, 1, 1, 1));
    }
    public void ChangeExtraAtk(int percent)
    {
        fireSkillAddedAtk = (int)(atk * percent / 100);
        ExtraAtk += fireSkillAddedAtk;
    }
    #endregion

    #region 魔力汲取技能

    // 魔力汲取技能
    protected void OnDarkSkill(object sender, EventArgs e)
    {
        SkillEventArgs args = e as SkillEventArgs;
        if (args == null) return;
        if (darkCoroutine != null)
        {
            StopCoroutine(darkCoroutine);
        }
        // 等待技能持续时间
        darkCoroutine = StartCoroutine(DarkTalent(args.skillName, args.cooldown, args.duration));
    }
    private IEnumerator DarkTalent(string skillName, int cooldown, int duration)
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.SkillDark });
        // 展示技能动画
        // 技能效果显示在敌人身上
        yield return new WaitForSeconds(0.2f);
        Transform currentMonster = FindObjectOfType<Monster>().transform;
        if (battleSceneManager != null)
        {
            if (skillSpineDark != null) skillSpineDark = null;
            skillSpineDark = Instantiate(battleSceneManager.SpineSkillDark, new Vector3(currentMonster.position.x, currentMonster.position.y / 2.0f, currentMonster.position.z), Quaternion.identity);
        }
        //技能效果
        int darkSkillDamage = (int)((Atk + ExtraAtk) * 1.5f); // 魔力汲取造成 150%攻击力 的伤害
        int restoredHp = 30 + (int)(darkSkillDamage * 0.2f); // 恢复 30+本次伤害20% 作为生命值
        // Debug.Log($"魔力汲取造成的伤害: {darkSkillDamage}, 恢复的生命值: {restoredHp}");
        this.TriggerEvent(EventName.CharacterAttack, new CharacterAttackEventArgs() { pos = curPos, atk = darkSkillDamage });
        Hp += restoredHp; // 恢复生命值

        // 技能倒计时
        this.TriggerEvent(EventName.SkillCountDown, new SkillEventArgs() { skillName = skillName, cooldown = cooldown, duration = duration });
        yield return new WaitForSeconds(duration);
        // 移除技能动画
        // if (skillSpineDark != null)
        // {
        //     Destroy(skillSpineDark);
        // }
    }
    #endregion

    #region 普通技能1-荆棘之甲

    // 普通技能1-荆棘之甲
    protected void OnCommonSkill_1(object sender, EventArgs e)
    {
        SkillEventArgs args = e as SkillEventArgs;
        if (args == null) return;
        if (commonSkill_1_Coroutine != null)
        {
            StopCoroutine(commonSkill_1_Coroutine);
        }
        commonSkill_1_Coroutine = StartCoroutine(CommonSkill_1(args.skillName, args.cooldown, args.duration));
    }
    private IEnumerator CommonSkill_1(string skillName, int cooldown, int duration)
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.SkillCommon_1 });
        //展示spine动画
        if (battleSceneManager != null)
        {
            SpineSkillCommon_1 = Instantiate(battleSceneManager.SpineSkillCommon_1, new Vector3(transform.position.x, transform.position.y - 3.8f, transform.position.z), Quaternion.identity);
        }
        //将玩家的颜色透明度变浅
        gameObject.transform.Find("Wand").GetComponent<SkeletonMecanim>().skeleton.SetColor(new Color(1, 1, 1, 0.5f));
        startCommonSkill_1 = true;
        //技能倒计时
        this.TriggerEvent(EventName.SkillCountDown, new SkillEventArgs() { skillName = skillName, cooldown = cooldown, duration = duration });
        yield return new WaitForSeconds(duration);
        //技能结束
        startCommonSkill_1 = false;
        // 移除spine动画
        if (SpineSkillCommon_1 != null)
        {
            Destroy(SpineSkillCommon_1);
        }
        gameObject.transform.Find("Wand").GetComponent<SkeletonMecanim>().skeleton.SetColor(new Color(1, 1, 1, 1));
    }
    #endregion

    #region 普通技能2-魔法力场
    // 普通技能2-魔法力场
    protected void OnCommonSkill_2(object sender, EventArgs e)
    {
        SkillEventArgs args = e as SkillEventArgs;
        if (args == null) return;
        if (commonSkill_2_Coroutine != null)
        {
            StopCoroutine(commonSkill_2_Coroutine);
        }
        commonSkill_2_Coroutine = StartCoroutine(CommonSkill_2(args.skillName, args.cooldown, args.duration));
    }
    private IEnumerator CommonSkill_2(string skillName, int cooldown, int duration)
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.SkillCommon_2 });
        //展示spine动画
        if (battleSceneManager != null)
        {
            SpineSkillCommon_2 = Instantiate(battleSceneManager.SpineSkillCommon_2, new Vector3(transform.position.x, transform.position.y - 2.2f, transform.position.z), Quaternion.identity);
        }
        //形成一个可吸收(200+100%攻击力)伤害的守护力场      
        shieldAmount = 200 + (int)(Atk * 1.0f); // 200 + 100%攻击力
        //技能倒计时
        this.TriggerEvent(EventName.SkillCountDown, new SkillEventArgs() { skillName = skillName, cooldown = cooldown, duration = duration });
        yield return new WaitForSeconds(duration);
    }

    //技能结束
    //移除spine动画
    private void EndCommonSkill_2(object sender, EventArgs e)
    {
        shieldAmount = 0;
        if (SpineSkillCommon_2 != null)
        {
            Destroy(SpineSkillCommon_2);
        }
    }
    #endregion

    #region 普通技能3-魔力膨胀
    // 普通技能3-魔力膨胀
    protected void OnCommonSkill_3(object sender, EventArgs e)
    {
        SkillEventArgs args = e as SkillEventArgs;
        if (args == null) return;
        if (commonSkill_3_Coroutine != null)
        {
            StopCoroutine(commonSkill_3_Coroutine);
        }
        commonSkill_3_Coroutine = StartCoroutine(CommonSkill_3(args.skillName, args.cooldown, args.duration));
    }
    private IEnumerator CommonSkill_3(string skillName, int cooldown, int duration)
    {
        // while (startCommonSkill_4) yield return null;//如果正在释放技能全力一击，则先等待
        StopAttack(null, null); // 停止自动攻击
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.SkillCommon_3 });

        yield return new WaitForSeconds(1);
        startCommonSkill_3 = true;
        // 快速连续攻击3次，每次攻击造成(攻击力60%)伤害
        continuousAttackDamage = (int)((Atk + ExtraAtk) * 0.6f); // 每次攻击造成60%攻击力的伤害
        atkSpeed /= 5; // 提高攻击速度

        this.TriggerEvent(EventName.AttackCommand);// 触发攻击命令
        // 技能倒计时
        this.TriggerEvent(EventName.SkillCountDown, new SkillEventArgs() { skillName = skillName, cooldown = cooldown, duration = duration });
        yield return new WaitForSeconds(duration);
    }

    // 技能结束
    private void EndCommonSkill_3(object sender, EventArgs e)
    {
        StartCoroutine(EndCommonSkill_3Coroutine());
    }

    private IEnumerator EndCommonSkill_3Coroutine()
    {
        atkSpeed *= 5; // 恢复攻击速度
        startCommonSkill_3 = false;
        CommonSkill_3_count = 0; // 重置攻击次数
        continuousAttackDamage = 0; // 重置连续攻击伤害
        // 等待攻击间隔时长后再恢复普通攻击
        yield return new WaitForSeconds(atkSpeed);
        StartAttack(null, null); // 技能结束后恢复自动攻击
    }
    #endregion

    #region 普通技能4-全力一击
    // 普通技能4-全力一击
    protected void OnCommonSkill_4(object sender, EventArgs e)
    {
        SkillEventArgs args = e as SkillEventArgs;
        if (args == null) return;
        if (commonSkill_4_Coroutine != null)
        {
            StopCoroutine(commonSkill_4_Coroutine);
        }
        commonSkill_4_Coroutine = StartCoroutine(CommonSkill_4(args.skillName, args.cooldown, args.duration));
    }
    private IEnumerator CommonSkill_4(string skillName, int cooldown, int duration)
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.SkillCommon_4 });
        // if (startCommonSkill_3 && !battleSceneManager.isAutoBattle)
        // {
        //     SpineSkillCommon_4 = Instantiate(battleSceneManager.SpineSkillCommon_4, new Vector3(-0.0f, -0.7f, transform.position.z), Quaternion.Euler(50f, 0f, 0f));
        // }
        // else
        // {
            StopAttack(null, null); // 停止自动攻击
            yield return new WaitForSeconds(1);
        // }
        startCommonSkill_4 = true;
        // 全力一击，造成（攻击力*300）伤害，但自身会减少10%生命值
        CommonSkill_4_Damage = (int)((Atk + ExtraAtk) * 3.0f); // 300%攻击力的伤害
        this.TriggerEvent(EventName.AttackCommand);// 触发攻击命令

        // 技能倒计时
        this.TriggerEvent(EventName.SkillCountDown, new SkillEventArgs() { skillName = skillName, cooldown = cooldown, duration = duration });
        yield return new WaitForSeconds(duration);
    }
    private void EndCommonSkill_4(object sender, EventArgs e)
    {
        StartCoroutine(EndCommonSkill_4Coroutine());
    }
    private IEnumerator EndCommonSkill_4Coroutine()
    {
        startCommonSkill_4 = false;
        yield return new WaitForSeconds(1);
        StartAttack(null, null); // 技能结束后恢复自动攻击
    }
    #endregion

    public void RestoreHp(object sender, EventArgs e)
    {
        RestoreHpEventArgs args = e as RestoreHpEventArgs;
        Hp += initHp * args.percent / 100;
    }

    protected void Resurge(object sender, EventArgs e)
    {
        Hp = DataManager.Instance.expUpgradeInfoDic[characterId][Mvc.GetModel<GameModel>().PlayerLevel[characterId] - 1].hp;
        this.TriggerEvent(EventName.CharacterExpChange, new CharacterExpChangeEventArgs() { exp = Mvc.GetModel<GameModel>().Exp, percent = (float)Mvc.GetModel<GameModel>().Exp / DataManager.Instance.expUpgradeInfoDic[characterId][Mvc.GetModel<GameModel>().PlayerLevel[characterId]].expCost });

        // Mp = initMp;
        // this.TriggerEvent(EventName.CharacterMove, new CharacterMoveEventArgs() { pos = curPos });
    }

    protected void ChangeGoodId(object sender, EventArgs e)
    {
        CharacterMoveEndEventArgs args = e as CharacterMoveEndEventArgs;
        curGoodId = args.id;
    }

    protected virtual void StopAttack(object sender, EventArgs e)
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
        canAttack = false;
    }

    protected virtual void StartAttack(object sender, EventArgs e)
    {
        if (attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(AttackRoutine());
        }
        canAttack = true;
    }
    private void ChangeAutoState(object sender, EventArgs e)
    {
        AutoBattleEventArgs args = e as AutoBattleEventArgs;
        isAutoReleaseSkill = args.isAutoBattle;
    }
}