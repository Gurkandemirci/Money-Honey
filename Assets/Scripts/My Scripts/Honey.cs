using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honey : MonoBehaviour
{

    AnimancerComponent anim;

    public int salary;

    void Start()
    {
        anim = GetComponent<AnimancerComponent>();
        AnimationManager.instance.PlayAnim(AnimationManager.instance.honeyIdle, anim, 0.8f, 1);
    }

    



    public void PlayWalkWithMoneyAnim()
    {
        AnimationManager.instance.PlayAnim(AnimationManager.instance.honeyWalkWithMoney, anim, 0.8f, 1);
    }

}
