using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowIdleState : BowState
{
    public BowIdleState(Archer _archer, BowStateMachine _stateMachine, string _animBoolName) : base(_archer, _stateMachine, _animBoolName)
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
    }
}
