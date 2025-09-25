using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterState
{
    protected MonsterStateMachine stateMachine;
    protected Monster monster;

    protected string animBoolName;

    public MonsterState(Monster _monster, MonsterStateMachine _stateMachine, string _animBoolName)
    {
        this.monster = _monster;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        monster.Anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
    }

    public virtual void Exit()
    {
        monster.Anim.SetBool(animBoolName, false);
    }
}
