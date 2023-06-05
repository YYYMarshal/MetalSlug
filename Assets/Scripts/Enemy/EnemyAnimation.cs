using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayWalkAnimation()
    {
        animator.SetBool(AnimatorParameters.Walk, true);
    }
    public void PlayIdleAnimation()
    {
        animator.SetBool(AnimatorParameters.Walk, false);
    }
    public void PlayKillAnimation()
    {
        animator.SetTrigger(AnimatorParameters.Kill);
    }
    public void PlayDieAnimation()
    {
        animator.SetTrigger(AnimatorParameters.Die);
    }
}
