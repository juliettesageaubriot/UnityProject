using System;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Interactables
{
    public abstract class Pickable : MonoBehaviour
    {
        [SerializeField] private SingleUnityLayer playerLayer;
        [SerializeField] private UnityEvent onPickupEvent;

        protected virtual void Start()
        {
            if (onPickupEvent == null)
                onPickupEvent = new UnityEvent();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == playerLayer.LayerIndex)
            {
                onPickupEvent.Invoke();
                Pick();
            }
        }

        protected abstract void Pick();
    }
}
