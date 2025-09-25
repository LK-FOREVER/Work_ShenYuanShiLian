using DG.Tweening;
using Spine.Unity;
using System;
using System.Collections;
using UnityEngine;

public class Master : CharacterBase
{
    public Animator Anim { get; private set; }

    public WandStateMachine StateMachine { get; private set; }

    public WandIdleState IdleState { get; private set; }
    public WandAttackState AttackState { get; private set; }

    public int cost = 30;
    public int time = 3;
    public bool couldAttack = false;
    public bool blurred = false;

    public GameObject fire;
    public GameObject skillSpine_3;
    public GameObject skillSpine_4;

    protected override void InitStateMachine()
    {
        Anim = transform.Find("Wand").GetComponent<Animator>();

        StateMachine = new WandStateMachine();

        IdleState = new WandIdleState(this, StateMachine, "Idle");
        AttackState = new WandAttackState(this, StateMachine, "Attack");

        StateMachine.Initialize(IdleState);
    }

    protected override void UnderAttack(object sender, EventArgs e)
    {
        if (blurred || Hp <= 0) return;
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Attacked });
        MonsterAttackEventArgs args = e as MonsterAttackEventArgs;
        //是否闪避
        // Debug.Log("闪避率：" + Dodge);
        bool isDodge = UnityEngine.Random.Range(0.0f, 100.0f) < Dodge;
        if (isDodge)
        {
            return;
        }
        else
        {
            if (shieldAmount > 0)
            {
                if (shieldAmount >= args.atk)
                {
                    //如果护盾值大于等于伤害值，则只扣除护盾值
                    shieldAmount -= args.atk;
                    args.atk = 0;
                    if (shieldAmount == 0)
                    {
                        this.TriggerEvent(EventName.EndCommonSkill_2);
                    }
                }
                else
                {
                    //如果护盾值小于伤害值，则扣除护盾值并减少伤害值
                    args.atk -= shieldAmount;
                    shieldAmount = 0;
                    this.TriggerEvent(EventName.EndCommonSkill_2);
                }
            }

            //计算减伤
            if (0 < args.atk * (1 - DamageReduce) && args.atk * (1 - DamageReduce) < 1)
            {
                args.atk = 1;
            }
            else
            {
                args.atk = (int)(args.atk * (1 - DamageReduce));
            }
            
            Hp -= args.atk;
            this.TriggerEvent(EventName.CharacterAttacked);
            if (startCommonSkill_1)
            {
                //荆棘之甲反弹10%的伤害
                int reboundDamage = (int)(args.atk * 0.1f);
                if (reboundDamage == 0) reboundDamage = 1; // 至少反弹1点伤害
                // Debug.Log("荆棘之甲反弹伤害：" + reboundDamage);
                this.TriggerEvent(EventName.CharacterAttack, new CharacterAttackEventArgs() { pos = curPos, atk = reboundDamage });
            }
        }
    }

    protected override void SendAttackCommond(object sender, EventArgs e)
    {
        couldAttack = Hp > 0 && StateMachine.CurrentState != AttackState && !moving;
    }

    protected override void SendDeffendCommond(object sender, EventArgs e)
    {
        if (moving) return;
        if (Mp < cost) return;
        StopCoroutine(Blur());
        StartCoroutine(Blur());
    }
    // protected override void StopAttack(object sender, EventArgs e)
    // {
    //     base.StopAttack(sender, e);
    //     // couldAttack = false;
    //     // StateMachine.ChangeState(IdleState);
    // }

    // protected override void StartAttack(object sender, EventArgs e)
    // {
    //     base.StartAttack(sender, e);
    //     // couldAttack = Hp > 0 && StateMachine.CurrentState != AttackState && !moving;
    // }

    IEnumerator Blur()
    {
        Mp -= cost;
        blurred = true;
        transform.Find("Wand").GetComponent<SkeletonMecanim>().skeleton.SetColor(new Color(1, 1, 1, 0.5f));
        yield return new WaitForSeconds(time);
        blurred = false;
        transform.Find("Wand").GetComponent<SkeletonMecanim>().skeleton.SetColor(new Color(1, 1, 1, 1));
    }

    public void RestoreMp(int param)
    {
        Mp += initMp * param / 100;
    }

    private void Update()
    {
        StateMachine.CurrentState.Update();
    }
}
