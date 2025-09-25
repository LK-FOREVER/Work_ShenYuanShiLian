using DG.Tweening;
using Spine.Unity;
using System;
using System.Collections;
using UnityEngine;

public class Monster : Entity
{
    public int monsterId;

    private int initHp;

    private int hp;
    private int aspd = 2;//攻击速度
    private int atk;
    private float dex;
    private float crit;
    private int exp;
    private int dropCoin;

    private bool isDead = false;

    private bool isFrozen = false;
    private GameObject skillSpineIce;

    private Coroutine monsterAttackCoroutine;
    private Coroutine freezeCoroutine;

    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
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
    public float Crit
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
    public bool IsDead
    {
        get
        {
            return isDead;
        }
        set
        {
            isDead = value;
        }
    }
    public bool IsFrozen
    {
        get
        {
            return isFrozen;
        }
        set
        {
            isFrozen = value;
        }
    }

    public Animator Anim { get; private set; }

    public MonsterStateMachine StateMachine { get; private set; }
    public MonsterIdleState IdleState { get; private set; }
    public MonsterAttackState AttackState { get; private set; }
    public MonsterDeadState DeadState { get; private set; }

    public event Action<int, int> UpdateHpBar;

    protected override void Awake()
    {
        base.Awake();

        EventManager.Instance.AddListener(EventName.CharacterAttack, UnderAttack);
        EventManager.Instance.AddListener(EventName.Poison, Poison);
        EventManager.Instance.AddListener(EventName.IceSkill, OnIceSkill);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        StopAllCoroutines();

        EventManager.Instance.RemoveListener(EventName.CharacterAttack, UnderAttack);
        EventManager.Instance.RemoveListener(EventName.Poison, Poison);
        EventManager.Instance.RemoveListener(EventName.IceSkill, OnIceSkill);
    }

    protected void Start()
    {
        monsterAttackCoroutine = StartCoroutine(Attack());
    }
    public override void Init(int _id, int _level, int _characterId)
    {
        base.Init(_id, _level, _characterId);
        goodId = monsterId;
        hp = DataManager.Instance.monsterInfoDic[monsterId].hp;
        if (Mvc.GetModel<GameModel>().LevelType == 1) hp = 999999999;
        atk = DataManager.Instance.monsterInfoDic[monsterId].atk;
        dex = DataManager.Instance.monsterInfoDic[monsterId].dex;
        crit = DataManager.Instance.monsterInfoDic[monsterId].crit;
        exp = DataManager.Instance.monsterInfoDic[monsterId].exp;
        dropCoin = UnityEngine.Random.Range(DataManager.Instance.monsterInfoDic[monsterId].minIcon, DataManager.Instance.monsterInfoDic[monsterId].maxIcon + 1);
        // hp = UnityEngine.Random.Range(DataManager.Instance.monsterInfoDic[monsterId].hpMin, DataManager.Instance.monsterInfoDic[monsterId].hpMax + 1);
        // atk = UnityEngine.Random.Range(DataManager.Instance.monsterInfoDic[monsterId].atkMin, DataManager.Instance.monsterInfoDic[monsterId].atkMax + 1);
        // aspd = DataManager.Instance.monsterInfoDic[monsterId].aspd;
        // dex = DataManager.Instance.monsterInfoDic[monsterId].dex;

        initHp = hp;

        Anim = GetComponent<Animator>();

        InitStateMachine();
    }

    private void InitStateMachine()
    {
        StateMachine = new MonsterStateMachine();
        IdleState = new MonsterIdleState(this, StateMachine, "Idle");
        AttackState = new MonsterAttackState(this, StateMachine, "Attack");
        DeadState = new MonsterDeadState(this, StateMachine, "Dead");

        StateMachine.Initialize(IdleState);
    }

    private void UnderAttack(object sender, EventArgs e)
    {
        CharacterAttackEventArgs args = e as CharacterAttackEventArgs;
        if (StateMachine.CurrentState == DeadState) return;

        if (args.pos == id)
        {
            if (dex == 0.0f)
            {
            }
            else
            {
                float randomValue = UnityEngine.Random.Range(0.0f, 100.0f);
                //闪避成功
                if (randomValue < dex) return;
            }
            StopCoroutine(ChangeImuneState(.5f));

            StartCoroutine(ChangeImuneState(.5f));
            hp -= args.atk;
            UpdateHpBar?.Invoke(hp, initHp);
            if(Mvc.GetModel<GameModel>().LevelType == 1)
                this.TriggerEvent(EventName.UpdateChallengeDamage, new ChallengeDamageArgs() { challengeDamage = args.atk });

            if (hp <= 0) Dead();
        }
    }

    private IEnumerator ChangeImuneState(float time)
    {
        // 该协程用于临时改变怪物的免疫状态（如受击闪烁效果）
        Sequence mySequence = DOTween.Sequence();
        mySequence.InsertCallback(0.2f, () =>
        {
            // 0.2秒时，设置材质属性和骨骼动画透明度
            gameObject.GetComponent<Renderer>().material.SetFloat("_FillPhase", 0.55f);
            if (IsFrozen)
            {
                gameObject.GetComponent<SkeletonMecanim>().skeleton.SetColor(new Color(1, 1, 1, 0.5f));
            }
            else
            {
                gameObject.GetComponent<SkeletonMecanim>().skeleton.SetColor(new Color(1, 1, 1, 1));
            }
        });
        mySequence.InsertCallback(0.3f, () =>
        {
            // 0.3秒时，恢复材质属性并设置骨骼动画为透明
            gameObject.GetComponent<Renderer>().material.SetFloat("_FillPhase", 0f);
            gameObject.GetComponent<SkeletonMecanim>().skeleton.SetColor(new Color(1, 1, 1, 0));
        });
        // 等待指定时间后，结束效果并还原
        yield return new WaitForSeconds(time);
        mySequence.Kill();
        gameObject.GetComponent<Renderer>().material.SetFloat("_FillPhase", 0f);
        if (IsFrozen)
        {
            gameObject.GetComponent<SkeletonMecanim>().skeleton.SetColor(new Color(1, 1, 1, 0.5f));
        }
        else
        {
            gameObject.GetComponent<SkeletonMecanim>().skeleton.SetColor(new Color(1, 1, 1, 1));
        }
    }

    private void Update()
    {
        StateMachine.CurrentState.Update();
    }

    protected override void CheckPlayerPos(object sender, EventArgs e)
    {
        base.CheckPlayerPos(sender, e);

        StopCoroutine(Attack());

        if (StateMachine.CurrentState == DeadState || !gameObject.activeSelf) return;
        if (characterPos != id) return;

        // this.TriggerEvent(EventName.CharacterMoveEnd, new CharacterMoveEndEventArgs() { id = goodId });

        StartCoroutine(Attack());
    }

    public void Kill()
    {
        hp = 0;
        UpdateHpBar?.Invoke(hp, initHp);
        Dead();
    }

    public void Dead()
    {
        hp = 0;
        // StateMachine.ChangeState(DeadState);
        if (IsDead) return;
        if (skillSpineIce != null)
        {
            Destroy(skillSpineIce);
        }
        StopCoroutine(spawnNewMonster());
        IsDead = true;
        DropCoin(dropCoin);
        DropExp(exp);
        this.TriggerEvent(EventName.MonsterDead, new MonsterDeadEventArgs() { currentMonsterNum = GameObject.Find("Canvas").GetComponent<BattleSceneManager>().GetCurrentMonsterNum() });
        // stateMachine.ChangeState(monster.IdleState);
        // this.TriggerEvent(EventName.PassTile);
        GlobalTaskCounter.Instance.AddDailyCount(DailyTask.KillEnemy);
        GlobalTaskCounter.Instance.AddWeeklyCount(WeeklyTask.KillEnemy);
        StopCoroutine(DeadAndSpawn());
        StartCoroutine(DeadAndSpawn());
    }
    private IEnumerator DeadAndSpawn()
    {
        yield return StartCoroutine(spawnNewMonster());
        Destroy(gameObject); // 协程执行完再销毁
    }

    // 延迟1.5秒后生成新的敌人
    IEnumerator spawnNewMonster()
    {
        // yield return new WaitForSeconds(1.5f);
        int maxMonsterNum = GameObject.Find("Canvas").GetComponent<BattleSceneManager>().GetMaxMonsterNum();
        int curMonsterNum = GameObject.Find("Canvas").GetComponent<BattleSceneManager>().GetCurrentMonsterNum();
        if (curMonsterNum >= maxMonsterNum)
        {
            //通关
            this.TriggerEvent(EventName.Pass);
            yield break;
        }
        this.TriggerEvent(EventName.InitMonster, new MonsterInitEventArgs() { monsterNum = curMonsterNum + 1 });
    }

    private IEnumerator Attack()
    {
        if (StateMachine.CurrentState == DeadState || !gameObject.activeSelf) yield break;
        while (true)
        {
            yield return new WaitForSeconds(aspd);
            if (StateMachine.CurrentState != AttackState && StateMachine.CurrentState != DeadState && !isFrozen)
            {
                StateMachine.ChangeState(AttackState);
            }
        }
    }

    public void DropCoin(int coin)
    {
        // int coin = initHp / param;
        this.TriggerEvent(EventName.GetCoin, new GetCoinEventArgs() { coin = coin });
    }

    public void DropExp(int exp)
    {
        this.TriggerEvent(EventName.GetExp, new GetExpEventArgs() { exp = exp });
    }

    public void Poison(object sender, EventArgs e)
    {
        PoisonEventArgs args = e as PoisonEventArgs;
        if (args.pos != id) return;
        hp -= hp * args.percent / 100;
        UpdateHpBar?.Invoke(hp, initHp);
    }
    public void OnIceSkill(object sender, EventArgs e)
    {
        SkillEventArgs args = e as SkillEventArgs;
        if (StateMachine.CurrentState == DeadState || !gameObject.activeSelf) return;
        if (IsFrozen) return;
        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine);
        }
        freezeCoroutine = StartCoroutine(IceTalent(args.skillName, args.cooldown, args.duration));

    }
    private IEnumerator IceTalent(string skillName, int cooldown, int duration)
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.SkillIce });
        //展示特效
        skillSpineIce = Instantiate(GameObject.Find("Canvas").GetComponent<BattleSceneManager>().SpineSkillIce, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        //将敌人的颜色透明度变浅
        gameObject.GetComponent<SkeletonMecanim>().skeleton.SetColor(new Color(1, 1, 1, 0.5f));

        //技能倒计时
        this.TriggerEvent(EventName.SkillCountDown, new SkillEventArgs() { skillName = skillName, cooldown = cooldown, duration = duration });

        // 设置怪物为冰冻状态
        IsFrozen = true;
        if (StateMachine.CurrentState != DeadState)
        {
            // 暂停动画
            if (Anim != null)
            {
                Anim.speed = 0f;
            }
        }
        yield return new WaitForSeconds(duration);
        // 销毁特效
        if (skillSpineIce != null)
        {
            Destroy(skillSpineIce);
        }
        // 恢复动画
        if (Anim != null)
        {
            Anim.speed = 1f;
        }
        //恢复敌人状态
        IsFrozen = false;
        gameObject.GetComponent<SkeletonMecanim>().skeleton.SetColor(new Color(1, 1, 1, 1));
        // if (StateMachine.CurrentState != DeadState && gameObject.activeSelf)
        // {
        //     StateMachine.ChangeState(IdleState);
        // }
    }
}
