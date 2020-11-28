using System;
using DG.Tweening;
using UnityEngine;

namespace Global
{
    [CreateAssetMenu(fileName = "FuelUIAnimator", menuName = "ScriptableObjects/FuelUIAnimator", order = 30)]
    public class FuelUIAnimator : ScriptableObject
    {
        private Tweener[] _tweens = {};
        private bool _isAnimating;
        public bool IsAnimating => _isAnimating;
        
        public delegate void AnimationEndDelegate();
        public event AnimationEndDelegate AnimationEndEvent;

        private RectTransform _fuelDestinationTransform;
        public RectTransform FuelDestinationTransform
        {
            set => _fuelDestinationTransform = value;
        }

        public void AnimatedFuelToStock(GameObject fuelSprite, float duration)
        {
            if (fuelSprite == null || fuelSprite.transform == null)
            {
                AnimationEndEvent?.Invoke();
                return;
            }
            
            _isAnimating = true;
            var spriteTransform = (RectTransform) fuelSprite.transform;
            var moveTween = spriteTransform.DOMove(_fuelDestinationTransform.position, duration);
            var scaleTween = spriteTransform.DOScale(_fuelDestinationTransform.localScale, duration);
            var sizeTween = spriteTransform.DOSizeDelta(_fuelDestinationTransform.sizeDelta, duration);
            _tweens = new Tweener[] {moveTween, scaleTween, sizeTween};
            moveTween.onComplete = () =>
            {
                AnimationEndEvent?.Invoke();
                _tweens = new Tweener[] { };
                _isAnimating = false;
            };
        }

        public void InterruptAnim()
        {
            foreach (var tweener in _tweens)
                tweener.Complete();
            AnimationEndEvent?.Invoke();
            _isAnimating = false;
        }
    }
}
