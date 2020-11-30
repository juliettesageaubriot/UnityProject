using System;
using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    public class TriggerToUnityEvent : MonoBehaviour
    {
        [SerializeField] private LayerMask filter;
        [SerializeField] private UnityEvent<Collider2D> onTriggerEnter;
        [SerializeField] private UnityEvent<Collider2D> onTriggerExit;

        private void Start()
        {
            if (onTriggerEnter == null)
                onTriggerEnter = new UnityEvent<Collider2D>();
            if (onTriggerExit == null)
                onTriggerExit = new UnityEvent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (filter.value == (filter.value | (1 << other.gameObject.layer)))
                onTriggerEnter.Invoke(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (filter.value == (filter.value | (1 << other.gameObject.layer)))
                onTriggerExit.Invoke(other);
        }
    }
}
