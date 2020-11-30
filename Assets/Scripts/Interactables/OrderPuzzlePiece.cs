using UnityEngine;

namespace Interactables
{
    public class OrderPuzzlePiece : MonoBehaviour, IActionable
    {
        private SpriteRenderer _spriteRenderer;
        private OrderPuzzle _orderPuzzle;
    
        [SerializeField] private Color activatedColor;
        [SerializeField] private Color desactivatedColor;
    
        [SerializeField] private bool isActivated;

        public bool IsActivated
        {
            get => isActivated;
            private set {
                isActivated = value;
                VisualUpdate();
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

        private void VisualUpdate()
        {
            _spriteRenderer.color = IsActivated ? activatedColor : desactivatedColor;
        }
    
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            VisualUpdate();
            if (IsActivated) _orderPuzzle.ActivatePuzzlePiece(this);
        }
    }
}
