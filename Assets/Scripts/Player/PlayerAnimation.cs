using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAnimators
{
    private Animator up;
    private Animator down;

    public Animator Up { get => up; set => up = value; }
    public Animator Down { get => down; set => down = value; }
}
public class PlayerAnimation : MonoBehaviour
{
    private readonly PlayerAnimators animators = new();

    private void Awake()
    {
        animators.Up = transform.Find("Up").GetComponent<Animator>();
        animators.Down = transform.Find("Down").GetComponent<Animator>();
    }
    public void PlayIdleAnimation()
    {
        animators.Up.SetBool(AnimatorParameters.Walk, false);
        animators.Down.SetBool(AnimatorParameters.Walk, false);
        animators.Up.SetBool(AnimatorParameters.Shoot, false);
        animators.Up.SetBool(AnimatorParameters.ShootUp, false);
    }
    public void PlayWalkAnimation()
    {
        animators.Up.SetBool(AnimatorParameters.Walk, true);
        animators.Down.SetBool(AnimatorParameters.Walk, true);
    }
    public void PlayJumpAnimation()
    {
        animators.Up.SetTrigger(AnimatorParameters.Jump);
        animators.Down.SetTrigger(AnimatorParameters.Jump);
    }
    public void PlayAttackAnimation()
    {
        animators.Up.SetTrigger(AnimatorParameters.Attack);
    }
    public void PlayThrowAnimation()
    {
        animators.Up.SetTrigger(AnimatorParameters.Throw);
    }
    public void PlayShootAnimation()
    {
        animators.Up.SetBool(AnimatorParameters.Shoot, true);
    }
    public void PlayShootUpAnimation()
    {
        animators.Up.SetBool(AnimatorParameters.ShootUp, true);
    }
    public void PlayDieAnimation()
    {
        animators.Up.gameObject.SetActive(false);
        animators.Down.SetTrigger(AnimatorParameters.Die);
    }
    public void PlayResumeAnimation()
    {
        animators.Up.gameObject.SetActive(true);
        animators.Down.gameObject.SetActive(true);
        animators.Down.SetTrigger(AnimatorParameters.Idle);
    }
}
