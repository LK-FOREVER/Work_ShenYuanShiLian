using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowMissAtkState : BowState
{
    public BowMissAtkState(Archer _archer, BowStateMachine _stateMachine, string _animBoolName) : base(_archer, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        AnimatorStateInfo stateInfo = archer.Anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 0.95f && !archer.Anim.IsInTransition(0))
        {
            this.TriggerEvent(EventName.CharacterAttack, new CharacterAttackEventArgs() { pos = archer.curPos, atk = archer.Atk + archer.extraAtk + archer.atkByBuff });
            archer.atkByBuff = 0;
            if(archer.miss)
            {
                stateMachine.ChangeState(archer.MissIdleState);
            }
            else
            {
                stateMachine.ChangeState(archer.IdleState);
            }
        }
    }
}
