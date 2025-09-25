using UnityEngine;

public class WandState
{
    protected WandStateMachine stateMachine;
    protected Master master;

    protected string animBoolName;

    public WandState(Master _master, WandStateMachine _stateMachine, string _animBoolName)
    {
        this.master = _master;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        master.Anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        // if (Input.GetKeyDown(KeyCode.A) || master.couldAttack)
        if (master.couldAttack)
        {
            master.couldAttack = false;
            stateMachine.ChangeState(master.AttackState);
        }
    }

    public virtual void Exit()
    {
        master.Anim.SetBool(animBoolName, false);
    }
}
