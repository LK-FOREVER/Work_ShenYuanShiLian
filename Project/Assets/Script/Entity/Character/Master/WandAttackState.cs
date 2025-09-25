using DG.Tweening;
using Spine.Unity;
using UnityEngine;

public class WandAttackState : WandState
{
    public WandAttackState(Master _master, WandStateMachine _stateMachine, string _animBoolName) : base(_master, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //技能——魔力膨胀
        if (master.startCommonSkill_3)
        {
            // GameObject go = ObjectPool.Instance.CreateObject(master.skillSpine_3, new Vector3(-0.0f, -0.7f, master.transform.position.z), Quaternion.Euler(50f, 0f, 0f));
            GameObject go = Object.Instantiate(master.skillSpine_3, new Vector3(-0.0f, -0.7f, master.transform.position.z), Quaternion.Euler(50f, 0f, 0f));
            go.transform.DOMove(new Vector3(0, 4.1f, 5.56f), .7f)
                .OnComplete(() =>
                {
                    Object.Destroy(go);
                });
        }
        //技能——全力一击
        if (master.startCommonSkill_4)
        {
            GameObject go = ObjectPool.Instance.CreateObject(master.skillSpine_4, new Vector3(-0.0f, -0.7f, master.transform.position.z), Quaternion.Euler(50f, 0f, 0f));
        }
        //普通攻击
        if (!master.startCommonSkill_3 && !master.startCommonSkill_4)
        {
            // GameObject go = ObjectPool.Instance.CreateObject(master.fire, new Vector3(0.3f, -0.52f, master.transform.position.z), Quaternion.identity);
            // // float y = master.curGoodId > 3 ? -0.5f : -1.8f;
            // go.transform.DOMove(new Vector3(0, 4.3f, 14.5f), .3f)
            //     .OnComplete(() =>
            //     {
            //         SkeletonAnimation skeletonAnimation = go.GetComponent<SkeletonAnimation>();
            //         skeletonAnimation.AnimationState.SetAnimation(0, "show_attack", false);
            //         // 回收到对象池
            //         if (go != null && go.activeInHierarchy)
            //         {
            //             ObjectPool.Instance.CollectObject(go);
            //         }
            //     });


            GameObject go = Object.Instantiate(master.fire, new Vector3(0.3f, -0.52f, master.transform.position.z), Quaternion.identity);
            go.transform.DOMove(new Vector3(0, 4.3f, 14.5f), .3f)
                .OnComplete(() =>
                {
                    Object.Destroy(go);
                });

        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        AnimatorStateInfo stateInfo = master.Anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 1f && !master.Anim.IsInTransition(0))
        {
            // 技能-魔力膨胀，连续攻击
            if (master.startCommonSkill_3)
            {
                if (master.CommonSkill_3_count < 3)
                {
                    master.CommonSkill_3_count++;
                    this.TriggerEvent(EventName.CharacterAttack, new CharacterAttackEventArgs() { pos = master.curPos, atk = master.continuousAttackDamage * (int)(1 + master.DamageAdd) });
                }
                // Debug.Log("连续攻击次数：" + master.CommonSkill_3_count);
                // Debug.Log("连续攻击伤害：" + master.continuousAttackDamage);
            }
            if (master.startCommonSkill_4 && master.startCommonSkill_3 && !master.isAutoReleaseSkill)
            {
                ObjectPool.Instance.CreateObject(master.skillSpine_4, new Vector3(-0.0f, -0.7f, master.transform.position.z), Quaternion.Euler(50f, 0f, 0f));
                this.TriggerEvent(EventName.CharacterAttack, new CharacterAttackEventArgs() { pos = master.curPos, atk = master.CommonSkill_4_Damage });
                master.Hp -= (int)(master.Hp * 0.1f) == 0 ? 1 : (int)(master.Hp * 0.1f); // 自身减少10%生命值（至少减1点生命）
            }
            // 技能-全力一击
            else if (master.startCommonSkill_4)
            {
                this.TriggerEvent(EventName.CharacterAttack, new CharacterAttackEventArgs() { pos = master.curPos, atk = master.CommonSkill_4_Damage });
                master.Hp -= (int)(master.Hp * 0.1f) == 0 ? 1 : (int)(master.Hp * 0.1f); // 自身减少10%生命值（至少减1点生命）
            }
            //普通攻击
            if (!master.startCommonSkill_3 && !master.startCommonSkill_4)
            {
                //是否暴击
                // Debug.Log("暴击率：" + master.Crit);
                bool isCritical = Random.Range(0.0f, 100.0f) < master.Crit;
                if (isCritical)
                {
                    this.TriggerEvent(EventName.CharacterAttack, new CharacterAttackEventArgs() { pos = master.curPos, atk = (master.Atk + master.extraAtk) * 2 });
                }
                else
                {
                    this.TriggerEvent(EventName.CharacterAttack, new CharacterAttackEventArgs() { pos = master.curPos, atk = master.Atk + master.extraAtk });
                }
            }
            stateMachine.ChangeState(master.IdleState);
            if (master.startCommonSkill_3 && master.CommonSkill_3_count < 3)
            {
                master.couldAttack = true;
            }
            else if (master.CommonSkill_3_count == 3)
            {
                master.TriggerEvent(EventName.EndCommonSkill_3);
            }
            if (master.startCommonSkill_4)
            {
                master.TriggerEvent(EventName.EndCommonSkill_4);
            }
        }
    }
}
