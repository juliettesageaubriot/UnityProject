using UnityEngine;
using UnityEngine.Events;

namespace Interactables
{
    public class OrderPuzzlePiece : MonoBehaviour, IActionable
    {
        private SpriteRenderer _spriteRenderer;
        private OrderPuzzle _orderPuzzle;

        [SerializeField] private bool isActivated;
        [SerializeField] private UnityEvent onActivatedEvent;
        [SerializeField] private UnityEvent onDesactivatedEvent;

        public bool IsActivated
        {
            get => isActivated;
            private set {
                isActivated = value;
                TriggerEvent();
            }
        }

        public void SetPuzzle(OrderPuzzle orderPuzzle)
        {
            _orderPuzzle = orderPuzzle;
        }

        public void Desactivate()
        {
            IsActivated = false;
        }

        public void Action()
        {
            if (IsActivated) return;
            IsActivated = true;
            _orderPuzzle.ActivatePuzzlePiece(this);
        }

        public bool IsActionable()
        {
            return !IsActivated;
        }

        private void TriggerEvent()
        {
            if (isActivated)
                onActivatedEvent.Invoke();
            else
                onDesactivatedEvent.Invoke();
        }
        
        private void Start()
        {
            if (IsActivated) _orderPuzzle.ActivatePuzzlePiece(this);
            if (onActivatedEvent == null)
                onActivatedEvent = new UnityEvent();
            if (onDesactivatedEvent == null)
                onDesactivatedEvent = new UnityEvent();
        }
    }
}
