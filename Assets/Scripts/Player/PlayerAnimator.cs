using System;
using UnityEngine;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private PlayerPositionData positionData;

        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");

        private void OnEnable()
        {
            positionData.DirectionChangeEvent += UpdateDirection;
        }
        private void OnDisable()
        {
            positionData.DirectionChangeEvent -= UpdateDirection;
        }

        public void UpdateDirection(Vector2 direction)
        {
            animator.SetFloat(Horizontal, direction.x);
            animator.SetFloat(Vertical, direction.y);
        }

        public void OnMove()
        {
            animator.SetBool(IsMoving, true);
        }

        public void OnStopMove()
        {
            animator.SetBool(IsMoving, false);
        }
    }
}
