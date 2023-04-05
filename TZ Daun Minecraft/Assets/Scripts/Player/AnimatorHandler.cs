using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorHandler : MonoBehaviour
{
    public Animator animator { get; private set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayTargetAnimation(string name, float time, bool isAttacking = false)
    {
        animator.CrossFade(name, time);
        animator.SetBool("IsAttacking", isAttacking);
    }
}
