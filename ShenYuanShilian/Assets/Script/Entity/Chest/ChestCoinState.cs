using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ChestCoinState : ChestState
{
    public ChestCoinState(Chest _chest, ChestStateMachine _stateMachine, string _animBoolName) : base(_chest, _stateMachine, _animBoolName)
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
        if (chest.IsDead) return;
        AnimatorStateInfo stateInfo = chest.Anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 1.2f && !chest.Anim.IsInTransition(0))
        {
            chest.IsDead = true;
            this.TriggerEvent(EventName.PassTile);
        }
    }
}
