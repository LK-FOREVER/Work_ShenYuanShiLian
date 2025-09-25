using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandStateMachine
{
    public WandState CurrentState { get; private set; }
    public void Initialize(WandState _startState)
    {
        CurrentState = _startState;
        CurrentState.Enter();
    }

    public void ChangeState(WandState _newState)
    {
        CurrentState.Exit();
        CurrentState = _newState;
        CurrentState.Enter();
    }
}
