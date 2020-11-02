using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");

    public void OnMove(Vector2 direction)
    {
        animator.SetBool(IsMoving, true);
        animator.SetFloat(Horizontal, direction.x);
        animator.SetFloat(Vertical, direction.y);
    }

    public void OnStopMove()
    {
        animator.SetBool(IsMoving, false);
    }
}
