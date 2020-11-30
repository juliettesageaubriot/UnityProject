using System;
using Global;
using UnityEngine;

namespace UI
{
    public class PickupAnimation : MonoBehaviour
    {
        [SerializeField] private FuelUIAnimator animator;
        [SerializeField] private float duration = 0.8f;


        public void MoveToStock()
        {
            animator.AnimatedFuelToStock(gameObject, duration);
        }
    }
}
