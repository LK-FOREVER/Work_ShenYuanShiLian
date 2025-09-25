using UnityEngine;

public class SwordAttackState : SwordState
{
    public SwordAttackState(Soldier _soldier, SwordStateMachine _stateMachine, string _animBoolName) : base(_soldier, _stateMachine, _animBoolName)
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
        AnimatorStateInfo stateInfo = soldier.SwordAnim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 0.85f && !soldier.SwordAnim.IsInTransition(0))
        {
            this.TriggerEvent(EventName.CharacterAttack, new CharacterAttackEventArgs() { pos = soldier.curPos, atk = soldier.Atk + soldier.extraAtk});
            stateMachine.ChangeState(soldier.SoldierSwordIdleState);
        }
    }
}
