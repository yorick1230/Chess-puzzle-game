using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece
{
    public override bool[,] PossibleMove()
    {
        bool[,] boolArray = new bool[8, 8];
        ChessPiece c, c2;

        // white move
        if (isWhite)
        {
            // diagonal left & right
            if(CurrentX != 0 && CurrentY != 7)
            {
                c = Board.instance.chessPieces[CurrentX - 1, CurrentY + 1];
                if(c != null && !c.isWhite)
                {
                    boolArray[CurrentX - 1, CurrentY + 1] = true;
                }
            }
            if (CurrentX != 7 && CurrentY != 7)
            {
                c = Board.instance.chessPieces[CurrentX + 1, CurrentY + 1];
                if (c != null && !c.isWhite)
                {
                    boolArray[CurrentX + 1, CurrentY + 1] = true;
                }
            }

            // forward
            if (CurrentY != 7)
            {
                c = Board.instance.chessPieces[CurrentX, CurrentY + 1];
                if(c == null)
                {
                    boolArray[CurrentX, CurrentY + 1] = true;
                }
            }
            // forward on first move
            if (CurrentY == 1)
            {
                c = Board.instance.chessPieces[CurrentX, CurrentY + 1];
                c2 = Board.instance.chessPieces[CurrentX, CurrentY + 2];
                if (c == null && c2 == null)
                {
                    boolArray[CurrentX, CurrentY + 2] = true;
                }
            }
        }else
        {
            // Black move
            // diagonal left & right
            if (CurrentX != 0 && CurrentY != 0)
            {
                c = Board.instance.chessPieces[CurrentX - 1, CurrentY - 1];
                if (c != null && c.isWhite)
                {
                    boolArray[CurrentX - 1, CurrentY - 1] = true;
                }
            }

            if (CurrentX != 7 && CurrentY != 7)
            {
                c = Board.instance.chessPieces[CurrentX + 1, CurrentY - 1];
                if (c != null && c.isWhite)
                {
                    boolArray[CurrentX + 1, CurrentY - 1] = true;
                }
            }

            // forward
            if (CurrentY != 0)
            {
                c = Board.instance.chessPieces[CurrentX, CurrentY - 1];
                if (c == null)
                {
                    boolArray[CurrentX, CurrentY - 1] = true;
                }
            }
            // forward on first move
            if (CurrentY == 6)
            {
                c = Board.instance.chessPieces[CurrentX, CurrentY - 1];
                c2 = Board.instance.chessPieces[CurrentX, CurrentY - 2];
                if (c == null && c2 == null)
                {
                    boolArray[CurrentX, CurrentY - 2] = true;
                }
            }
        }
        return boolArray;
    }

    public override bool[,] ThreateningSquares()
    {
        bool[,] boolArray = new bool[8, 8];

        // white move
        if (isWhite)
        {
            // diagonal left & right
            if (CurrentX != 0 && CurrentY != 7)
            {
                boolArray[CurrentX - 1, CurrentY + 1] = true;
            }
            if (CurrentX != 7 && CurrentY != 7)
            {
                boolArray[CurrentX + 1, CurrentY + 1] = true;
            }
        }
        else
        {
            // Black move
            // diagonal left & right
            if (CurrentX != 0 && CurrentY != 0)
            {
                boolArray[CurrentX - 1, CurrentY - 1] = true;
            }

            if (CurrentX != 7 && CurrentY != 7)
            {
                boolArray[CurrentX + 1, CurrentY - 1] = true;
            }
        }
        return boolArray;
    }
}
