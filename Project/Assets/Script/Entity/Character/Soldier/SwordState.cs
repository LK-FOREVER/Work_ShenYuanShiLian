using UnityEngine;

public class SwordState
{
    protected SwordStateMachine stateMachine;
    protected Soldier soldier;

    protected string animBoolName;

    public SwordState(Soldier _soldier, SwordStateMachine _stateMachine, string _animBoolName)
    {
        this.soldier = _soldier;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        soldier.SwordAnim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || soldier.couldAttack)
        {
            soldier.couldAttack = false;
            stateMachine.ChangeState(soldier.SoldierSwordAttackState);
        }
    }

    public virtual void Exit()
    {
        soldier.SwordAnim.SetBool(animBoolName, false);
    }
}
