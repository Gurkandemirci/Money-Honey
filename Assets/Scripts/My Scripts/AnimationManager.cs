using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using System;
public class AnimationManager : MonoBehaviour
{
    public static AnimationManager instance;
    Action endFunctionAction;
    bool isAnimationEnded;



    public AnimationClip playerSadWalk;
    public AnimationClip playerNormalWalk;
    public AnimationClip playerModelWalk;
    public AnimationClip playerSadFinish;
    public AnimationClip playerNormalFinish;
    public AnimationClip playerVictoryFinish;


    public AnimationClip honeyIdle;
    public AnimationClip honeyWalkWithMoney;


 




    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    public float PlayAnim(AnimationClip clip, AnimancerComponent anim, float fade = 0.3f, float speed = 1, Action endAnimation = null)
    {
        var state = anim.Play(clip, fade);
        state.Speed = speed;

        isAnimationEnded = false;
        if (endAnimation != null)
        {
            endFunctionAction = endAnimation;
            state.Events.OnEnd = OnEndEvent;
        }
        return state.Duration / speed;
    }

    private void OnEndEvent()
    {
        if (!isAnimationEnded)
        {
            isAnimationEnded = true;
            endFunctionAction();
        }
    }
}
