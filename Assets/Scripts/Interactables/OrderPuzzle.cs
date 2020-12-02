using System;
using UnityEngine;
using UnityEngine.Events;

namespace Interactables
{
    public class OrderPuzzle : MonoBehaviour
    {
        [SerializeField] private OrderPuzzlePiece[] orderPuzzlePieces;
        [SerializeField] private UnityEvent onCompleteEvent;
        [SerializeField] private UnityEvent onFailedEvent;

        private void Awake()
        {
            if (onCompleteEvent == null)
                onCompleteEvent = new UnityEvent();
            if (onFailedEvent == null)
                onFailedEvent = new UnityEvent();
            foreach (var orderPuzzlePiece in orderPuzzlePieces)
                orderPuzzlePiece.SetPuzzle(this);
        }

        public void ActivatePuzzlePiece(OrderPuzzlePiece puzzlePiece)
        {
            var isCorrect = true;
        
            foreach (var piece in orderPuzzlePieces)
            {
                isCorrect &= piece.IsActivated;
                if (puzzlePiece == piece) break;
            }
        
            if (Array.IndexOf(orderPuzzlePieces, puzzlePiece) == orderPuzzlePieces.Length - 1 && isCorrect)
                Completed();
            if (!isCorrect) Failed();
        }

        private void Completed()
        {
            onCompleteEvent.Invoke();
        }

        private void Failed()
        {
            onFailedEvent.Invoke();
            foreach (var orderPuzzlePiece in orderPuzzlePieces)
                orderPuzzlePiece.Desactivate();
        }
    }
}
