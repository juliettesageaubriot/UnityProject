using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderPuzzle : MonoBehaviour
{
    [SerializeField] private OrderPuzzlePiece[] orderPuzzlePieces;

    private void Start()
    {
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
        Debug.Log("Complete");
    }

    private void Failed()
    {
        Debug.Log("Failed");
        foreach (var orderPuzzlePiece in orderPuzzlePieces)
            orderPuzzlePiece.Desactivate();
    }
}
