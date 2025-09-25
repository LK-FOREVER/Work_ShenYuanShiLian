using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowStateMachine
{
    public BowState CurrentState { get; private set; }
    public void Initialize(BowState _startState)
    {
        CurrentState = _startState;
        CurrentState.Enter();
    }

    public void ChangeState(BowState _newState)
    {
        CurrentState.Exit();
        CurrentState = _newState;
        CurrentState.Enter();
    }
}
