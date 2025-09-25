using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class BowAttackState : BowState
{
    public BowAttackState(Archer _archer, BowStateMachine _stateMachine, string _animBoolName) : base(_archer, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        GameObject go = ObjectPool.Instance.CreateObject(archer.arrow, new Vector3(1.2f, -2f, archer.transform.position.z), Quaternion.identity);
        float y = archer.curGoodId > 3 ? -0.5f : -1.5f;
        // 定义弹跳的起始位置、终点位置、最大高度、跳次数和每次跳的持续时间
        Vector3 jumpStartPosition = go.transform.position;
        Vector3 jumpEndPosition = new Vector3(0, y, go.transform.position.z + 3f);
        float jumpHeight = 3f;
        int numberOfJumps = 1;
        float jumpDuration = 0.5f;

        // 使用DOTween的DOJump方法来创建弹跳动画
        go.transform.DOJump(jumpEndPosition, jumpHeight, numberOfJumps, jumpDuration);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        AnimatorStateInfo stateInfo = archer.Anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 0.85f && !archer.Anim.IsInTransition(0))
        {
            this.TriggerEvent(EventName.CharacterAttack, new CharacterAttackEventArgs() { pos = archer.curPos, atk = archer.Atk + archer.extraAtk + archer.atkByBuff});
            archer.atkByBuff = 0;
            if (archer.miss)
            {
                stateMachine.ChangeState(archer.MissIdleState);
            }
            else
            {
                stateMachine.ChangeState(archer.IdleState);
            }
        }
    }
}
