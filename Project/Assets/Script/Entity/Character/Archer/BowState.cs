using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowState
{
    protected BowStateMachine stateMachine;
    protected Archer archer;

    protected string animBoolName;

    public BowState(Archer _archer, BowStateMachine _stateMachine, string _animBoolName)
    {
        this.archer = _archer;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        archer.Anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || archer.couldAttack)
        {
            archer.couldAttack = false;
            archer.couldMissAtk = false;
            stateMachine.ChangeState(archer.AttackState);
        }
        else if(Input.GetKeyDown(KeyCode.Q) || archer.couldMissAtk)
        {
            archer.couldAttack = false;
            archer.couldMissAtk = false;
            stateMachine.ChangeState(archer.MissAtkState);
        }
    }

    public virtual void Exit()
    {
        archer.Anim.SetBool(animBoolName, false);
    }
}
