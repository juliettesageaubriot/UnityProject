using System;
using DG.Tweening;
using UnityEngine;

namespace Interactables
{
    public class ScaleOnAwake : MonoBehaviour
    {
        [SerializeField] private float duration = 0.5f; 
        private void Awake()
        {
            var transformVar = transform;
            var defaultScale = transformVar.localScale;
            transformVar.localScale = Vector3.zero;
            transformVar.DOScale(defaultScale, duration);
        }
    }
}
