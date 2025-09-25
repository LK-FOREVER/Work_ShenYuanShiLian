using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;

public class MonsterDeadState : MonsterState
{
    public MonsterDeadState(Monster _monster, MonsterStateMachine _stateMachine, string _animBoolName) : base(_monster, _stateMachine, _animBoolName)
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
        if (monster.IsDead) return;
        AnimatorStateInfo stateInfo = monster.Anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 1.2f && !monster.Anim.IsInTransition(0))
        {
            monster.StopCoroutine(spawnNewMonster());
            monster.IsDead = true;
            this.TriggerEvent(EventName.MonsterDead, new MonsterDeadEventArgs() { currentMonsterNum = GameObject.Find("Canvas").GetComponent<BattleSceneManager>().GetCurrentMonsterNum() });
            stateMachine.ChangeState(monster.IdleState);
            // this.TriggerEvent(EventName.PassTile);
            monster.StartCoroutine(spawnNewMonster());
        }

        //AnimatorStateInfo stateInfo = monster.Anim.GetCurrentAnimatorStateInfo(0);
        //if (stateInfo.normalizedTime >= 1f && !monster.Anim.IsInTransition(0))
        //{
        //    stateMachine.ChangeState(monster.IdleState);
        //    this.TriggerEvent(EventName.MonsterAttack, new MonsterAttackEventArgs() { atk = monster.Atk });
        //}
    }

    // 延迟1秒后生成新的敌人
    IEnumerator spawnNewMonster()
    {
        yield return new WaitForSeconds(1.5f);
        int maxMonsterNum = GameObject.Find("Canvas").GetComponent<BattleSceneManager>().GetMaxMonsterNum();
        int curMonsterNum = GameObject.Find("Canvas").GetComponent<BattleSceneManager>().GetCurrentMonsterNum();
        if (curMonsterNum >= maxMonsterNum)
        {
            this.TriggerEvent(EventName.Pass);
            yield break;
        }
        this.TriggerEvent(EventName.InitMonster, new MonsterInitEventArgs() { monsterNum = curMonsterNum + 1 });
    }
}
