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

        public void OnMove()
        {
            animator.SetBool(IsMoving, true);
            animator.SetFloat(Horizontal, positionData.Direction.x);
            animator.SetFloat(Vertical, positionData.Direction.y);
        }

        public void OnStopMove()
        {
            animator.SetBool(IsMoving, false);
        }

        public void OnCantMove()
        {
            animator.SetFloat(Horizontal, positionData.Direction.x);
            animator.SetFloat(Vertical, positionData.Direction.y);
        }
    }
}
