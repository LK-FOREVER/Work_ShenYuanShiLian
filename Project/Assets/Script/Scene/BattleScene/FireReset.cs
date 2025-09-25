using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class FireReset : MonoBehaviour, IResetable
{
    public string defaultAnimation = "show_1"; // 你的fire初始动画名

    public void OnReset()
    {
        var skeleton = GetComponent<SkeletonAnimation>();
        if (skeleton != null)
        {
            // skeleton.skeleton.SetColor(Color.white);
            // skeleton.transform.localScale = Vector3.one;
            DG.Tweening.DOTween.Kill(this.gameObject);

            // Spine动画重置
            var state = skeleton.AnimationState;
            if (state != null)
            {
                state.ClearTracks(); // 清空所有动画队列
                state.SetAnimation(0, defaultAnimation, true); // 播放初始动画
                state.TimeScale = 1f; // 恢复正常速度
            }
            skeleton.skeleton.SetToSetupPose(); // 重置骨骼到初始姿势
        }

        // var sr = GetComponent<SpriteRenderer>();
        // if (sr != null)
        // {
        //     sr.color = Color.white;
        //     sr.sortingOrder = 0;
        // }
    }
}
