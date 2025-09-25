using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : CharacterBase
{
    public Animator Anim { get; private set; }

    public BowStateMachine StateMachine { get; private set; }

    public BowIdleState IdleState { get; private set; }
    public BowAttackState AttackState { get; private set; }
    public BowMissIdleState MissIdleState { get; private set; }
    public BowMissAtkState MissAtkState { get; private set; }

    public int cost = 30;
    public int time = 1;
    public bool couldAttack = false;
    public bool couldMissAtk = false;
    public bool miss = false;
    public int atkByBuff = 0;

    public GameObject arrow;

    public override void Init(int _id, int _hp, int _mp, int _atk, double _crit, double _dodge, double _damageAdd, double _damageReduce, int _curPos, int _dr)
    {
        base.Init(_id, _hp, _mp, _atk, _crit, _dodge, _damageAdd, _damageReduce, _curPos, _dr);
        StartCoroutine(Restore());
    }

    protected override void InitStateMachine()
    {
        Anim = transform.Find("Bow").GetComponent<Animator>();

        StateMachine = new BowStateMachine();

        IdleState = new BowIdleState(this, StateMachine, "Idle");
        AttackState = new BowAttackState(this, StateMachine, "Attack");
        MissIdleState = new BowMissIdleState(this, StateMachine, "MissIdle");
        MissAtkState = new BowMissAtkState(this, StateMachine, "MissAtk");

        StateMachine.Initialize(IdleState);
    }

    protected override void UnderAttack(object sender, EventArgs e)
    {
        if (miss)
        {
            this.TriggerEvent(EventName.Miss);
            miss = false;
            StateMachine.ChangeState(IdleState);
            return;
        }
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Attacked });
        MonsterAttackEventArgs args = e as MonsterAttackEventArgs;
        Hp -= args.atk * (100 - dr) / 100;
        this.TriggerEvent(EventName.CharacterAttacked);
    }

    IEnumerator Restore()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (Mp < initMp) Mp += 2;
        }
    }

    protected override void SendAttackCommond(object sender, EventArgs e)
    {
        if(StateMachine.CurrentState == MissIdleState)
        {
            couldAttack = false;
            couldMissAtk = Hp > 0 && !moving;
        }
        else if(StateMachine.CurrentState == IdleState)
        {
            couldAttack = Hp > 0 && !moving;
            couldMissAtk = false;
        }
    }

    protected override void SendDeffendCommond(object sender, EventArgs e)
    {
        if (moving) return;
        if (Mp < cost) return;
        Mp -= cost;
        miss = true;
        StateMachine.ChangeState(MissIdleState);
    }

    IEnumerator Blur()
    {
        Mp -= cost;
        miss = true;
        yield return new WaitForSeconds(time);
        miss = false;
    }

    public void AddMpLimit(int param)
    {
        int num = initMp * param / 100;
        initMp += num;
        Mp += num;
    }

    public void AddAtk(int param)
    {
        atkByBuff += Atk * param / 100;
    }

    private void Update()
    {
        StateMachine.CurrentState.Update();
    }
}
