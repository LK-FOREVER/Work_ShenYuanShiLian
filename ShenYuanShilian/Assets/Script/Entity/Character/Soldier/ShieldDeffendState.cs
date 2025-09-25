using UnityEngine;

public class ShieldDeffendState : ShieldState
{
    public ShieldDeffendState(Soldier _soldier, ShieldStateMachine _stateMachine, string _animBoolName) : base(_soldier, _stateMachine, _animBoolName)
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
        AnimatorStateInfo stateInfo = soldier.ShieldAnim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 2f && !soldier.ShieldAnim.IsInTransition(0))
        {
            stateMachine.ChangeState(soldier.SoldierShieldIdleState);
        }
    }
}
