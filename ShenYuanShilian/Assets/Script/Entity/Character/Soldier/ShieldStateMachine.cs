using UnityEngine;

public class ShieldStateMachine
{
    public ShieldState CurrentState { get; private set; }
    public void Initialize(ShieldState _startState)
    {
        CurrentState = _startState;
        CurrentState.Enter();
    }

    public void ChangeState(ShieldState _newState)
    {
        CurrentState.Exit();
        CurrentState = _newState;
        CurrentState.Enter();
    }
}
