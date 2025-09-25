using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class MonsterAttackState : MonsterState
{
    public MonsterAttackState(Monster _monster, MonsterStateMachine _stateMachine, string _animBoolName) : base(_monster, _stateMachine, _animBoolName)
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
        AnimatorStateInfo stateInfo = monster.Anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 1f && !monster.Anim.IsInTransition(0))
        {
            stateMachine.ChangeState(monster.IdleState);
            bool isCrit = false;
            if (monster.Crit != 0)
            {
                float randomValue = Random.Range(0.0f, 100.0f);
                //是否暴击成功
                if (randomValue < monster.Crit)
                    isCrit = true;
                else
                {
                    isCrit = false;
                }
            }
            this.TriggerEvent(EventName.MonsterAttack, new MonsterAttackEventArgs() { atk = isCrit? (int)(monster.Atk * 2f) : monster.Atk });
        }
    }
}
