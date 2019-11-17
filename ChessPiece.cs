using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessPiece : MonoBehaviour
{
    public int CurrentX { set; get; }
    public int CurrentY { set; get; }
    public bool isWhite;

    public void SetPosition(int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }

    // Returns a array with possible moves for the piece
    // true means the piece can go there, otherwise its a illegal move
    public virtual bool[,] PossibleMove()
    {
        return new bool[8,8];
    }

    // Returns a array with squares the piece threatens, different logic from possible moves
    // true means the piece can go there, otherwise its a illegal move
    public virtual bool[,] ThreateningSquares()
    {
        return new bool[8,8];
    }
}
