using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestState
{
    protected ChestStateMachine stateMachine;
    protected Chest chest;

    protected string animBoolName;

    public ChestState(Chest _chest, ChestStateMachine _stateMachine, string _animBoolName)
    {
        this.chest = _chest;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        chest.Anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
    }

    public virtual void Exit()
    {
        chest.Anim.SetBool(animBoolName, false);
    }
}
