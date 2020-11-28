using System;
using DG.Tweening;
using UnityEngine;

namespace Global
{
    [CreateAssetMenu(fileName = "FuelUIAnimator", menuName = "ScriptableObjects/FuelUIAnimator", order = 30)]
    public class FuelUIAnimator : ScriptableObject
    {
        public delegate void AnimationEndDelegate();
        public event AnimationEndDelegate AnimationEndEvent;

        private RectTransform _fuelDestinationTransform;
        public RectTransform FuelDestinationTransform
        {
            set => _fuelDestinationTransform = value;
        }

        public void AnimatedFuelToStock(GameObject fuelSprite, float duration)
        {
            if (fuelSprite == null)
            {
                AnimationEndEvent?.Invoke();
                return;
            }
            var spriteTransform = (RectTransform) fuelSprite.transform;
            var tween = spriteTransform.DOMove(_fuelDestinationTransform.position, duration);
            spriteTransform.DOScale(_fuelDestinationTransform.localScale, duration);
            spriteTransform.DOSizeDelta(_fuelDestinationTransform.sizeDelta, duration);
            tween.onComplete = () => AnimationEndEvent?.Invoke();
        }
    }
}
