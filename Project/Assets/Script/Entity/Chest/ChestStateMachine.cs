using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestStateMachine
{
    public ChestState CurrentState { get; private set; }
    public void Initialize(ChestState _startState)
    {
        CurrentState = _startState;
        CurrentState.Enter();
    }

    public void ChangeState(ChestState _newState)
    {
        CurrentState.Exit();
        CurrentState = _newState;
        CurrentState.Enter();
    }
}
