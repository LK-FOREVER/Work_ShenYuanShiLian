using DG.Tweening;
using System;
using UnityEngine;

public class Soldier : CharacterBase
{
    public Animator SwordAnim { get; private set; }
    public Animator ShieldAnim { get; private set; }

    public SwordStateMachine SoldierSwordStateMachine { get; private set; }
    public ShieldStateMachine SoldierShieldStateMachine { get; private set; }

    public SwordIdleState SoldierSwordIdleState { get; private set; }
    public SwordAttackState SoldierSwordAttackState { get; private set; }
    public ShieldIdleState SoldierShieldIdleState { get; private set; }
    public ShieldDeffendState SoldierShieldDeffendState { get; private set; }

    public bool couldAttack = false;
    public bool couldDeffend = false;

    protected override void InitStateMachine()
    {
        SwordAnim = transform.Find("Sword").GetComponent<Animator>();
        ShieldAnim = transform.Find("Shield").GetComponent<Animator>();

        SoldierSwordStateMachine = new SwordStateMachine();
        SoldierShieldStateMachine = new ShieldStateMachine();

        SoldierSwordIdleState = new SwordIdleState(this, SoldierSwordStateMachine, "Idle");
        SoldierSwordAttackState = new SwordAttackState(this, SoldierSwordStateMachine, "Attack");
        SoldierShieldIdleState = new ShieldIdleState(this, SoldierShieldStateMachine, "Idle");
        SoldierShieldDeffendState = new ShieldDeffendState(this, SoldierShieldStateMachine, "Deffend");

        SoldierSwordStateMachine.Initialize(SoldierSwordIdleState);
        SoldierShieldStateMachine.Initialize(SoldierShieldIdleState);
    }

    protected override void UnderAttack(object sender, EventArgs e)
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Attacked });

        MonsterAttackEventArgs args = e as MonsterAttackEventArgs;
        int damage = args.atk * (100 - dr) / 100;
        if(SoldierShieldStateMachine.CurrentState == SoldierShieldDeffendState)
        {
            SoldierShieldStateMachine.ChangeState(SoldierShieldIdleState);
            if (damage <= Mp)
            {
                Mp -= damage;
                return;
            } 
            else if(damage > Mp)
            {
                Mp = 0;
                damage -= Mp;
            }
        }
        Hp -= damage;
        this.TriggerEvent(EventName.CharacterAttacked);
    }

    protected override void SendAttackCommond(object sender, EventArgs e)
    {
        couldAttack = Hp > 0 && SoldierSwordStateMachine.CurrentState != SoldierSwordAttackState && !moving;
    }

    protected override void SendDeffendCommond(object sender, EventArgs e)
    {
        couldDeffend = Hp > 0 && SoldierShieldStateMachine.CurrentState != SoldierShieldDeffendState && !moving;
    }

    private void Update()
    {
        SoldierSwordStateMachine.CurrentState.Update();
        SoldierShieldStateMachine.CurrentState.Update();
    }

    public void AddHpLimit(int param)
    {
        int num = initHp * param / 100;
        initHp += num;
        Hp += num;
    }

    public void RestoreMp(int param)
    {
        Mp += initMp * param / 100;
    }
}
