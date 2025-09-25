using UnityEngine;

public class ShieldState
{
    protected ShieldStateMachine stateMachine;
    protected Soldier soldier;

    protected string animBoolName;

    public ShieldState(Soldier _soldier, ShieldStateMachine _stateMachine, string _animBoolName)
    {
        this.soldier = _soldier;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        soldier.ShieldAnim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) || soldier.couldDeffend)
        {
            soldier.couldDeffend = false;
            stateMachine.ChangeState(soldier.SoldierShieldDeffendState);
        }
    }

    public virtual void Exit()
    {
        soldier.ShieldAnim.SetBool(animBoolName, false);
    }
}
