using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Door : Entity
{
    private string[] type1 = { "men1_idle", "men1_open"};
    private string[] type2 = { "men3_idle", "men3_open" };
    private string[] type3 = { "men2_idle", "men2_open" };

    private SkeletonAnimation skeletonAnimation;
    protected override void Awake()
    {
        base.Awake();
        EventManager.Instance.AddListener(EventName.CharacterAttack, UnderAttack);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventManager.Instance.RemoveListener(EventName.CharacterAttack, UnderAttack);
    }

    public override void Init(int _id, int _level, int _characterId)
    {
        base.Init(_id, _level, _characterId);

        skeletonAnimation = GetComponent<SkeletonAnimation>();

        if (level > 20)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, type3[0], true);
        }
        else if (level > 10)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, type2[0], true);
        }
        else
        {
            skeletonAnimation.AnimationState.SetAnimation(0, type1[0], true);
        }
    }

    private void UnderAttack(object sender, EventArgs e)
    {
        CharacterAttackEventArgs args = e as CharacterAttackEventArgs;
        if (args.pos != id) return;

        if (level > 20)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, type3[1], false);
        }
        else if (level > 10)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, type2[1], false);
        }
        else
        {
            skeletonAnimation.AnimationState.SetAnimation(0, type1[1], false);
        }
        StartCoroutine(Pass());
    }

    IEnumerator Pass()
    {
        yield return new WaitForSeconds(1);
        this.TriggerEvent(EventName.Pass);
    }
}
