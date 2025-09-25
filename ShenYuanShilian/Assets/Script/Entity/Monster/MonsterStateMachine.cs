using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateMachine
{
    public MonsterState CurrentState { get; private set; }
    public void Initialize(MonsterState _startState)
    {
        CurrentState = _startState;
        CurrentState.Enter();
    }

    public void ChangeState(MonsterState _newState)
    {
        if (CurrentState.GetType().Name == "MonsterDeadState") return;
        CurrentState.Exit();
        CurrentState = _newState;
        CurrentState.Enter();
    }
}
