using UnityEngine;

public class SwordStateMachine
{
    public SwordState CurrentState { get; private set; }
    public void Initialize(SwordState _startState)
    {
        CurrentState = _startState;
        CurrentState.Enter();
    }

    public void ChangeState(SwordState _newState)
    {
        CurrentState.Exit();
        CurrentState = _newState;
        CurrentState.Enter();
    }
}
