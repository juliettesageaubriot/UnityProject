using System;
using Global;
using UnityEngine;

namespace UI
{
    public class PickupAnimation : MonoBehaviour
    {
        [SerializeField] private FuelUIAnimator animator;
        [SerializeField] private float duration = 0.8f;

        private bool _shouldPlay = true;
        
        private void OnEnable()
        {
            animator.AnimationEndEvent += HandleFuelAnimationEnd;
        }
        private void OnDisable()
        {
            animator.AnimationEndEvent -= HandleFuelAnimationEnd;
        }

        private void HandleFuelAnimationEnd()
        {
            _shouldPlay = false;
        }

        public void MoveToStock()
        {
            if(_shouldPlay)
                animator.AnimatedFuelToStock(gameObject, duration);
        }
    }
}
