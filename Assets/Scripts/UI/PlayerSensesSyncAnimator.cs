using Player;
using UnityEngine;

namespace UI
{
    public class PlayerSensesSyncAnimator : MonoBehaviour
    {
        [SerializeField] private PlayerSensesData sensesData;
        [SerializeField] private Animator animator;
        
        private void OnEnable() {
            sensesData.SenseChangeEvent += SyncAnimator;
        }
        private void OnDisable() {
            sensesData.SenseChangeEvent -= SyncAnimator;
        }

        private void SyncAnimator(SensesState state)
        {
            animator.SetFloat("IsBlind", state == SensesState.Blind ? 1f : 0f);
        }
    }
}