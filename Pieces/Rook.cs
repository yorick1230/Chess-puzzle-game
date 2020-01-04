using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessPiece
{
    public override bool[,] PossibleMove()
    {
        bool[,] boolArray = new bool[8, 8];

        ChessPiece c;
        int i;

        // Right
        i = CurrentX;
        while (true)
        {
            i++;
            if (i >= 8)
                break;
            c = Board.instance.chessPieces[i, CurrentY];
            if (c == null)
            {
                boolArray[i, CurrentY] = true;
            }
            else
            {
                if (c.isWhite != isWhite)
                    boolArray[i, CurrentY] = true;
                break;
            }
        }

        // Left
        i = CurrentX;
        while (true)
        {
            i--;
            if (i < 0)
                break;
            c = Board.instance.chessPieces[i, CurrentY];
            if (c == null)
            {
                boolArray[i, CurrentY] = true;
            }
            else
            {
                if (c.isWhite != isWhite)
                    boolArray[i, CurrentY] = true;
                break;
            }
        }

        // Up
        i = CurrentY;
        while (true)
        {
            i++;
            if (i >= 8)
                break;
            c = Board.instance.chessPieces[CurrentX, i];
            if (c == null)
            {
                boolArray[CurrentX, i] = true;
            }
            else
            {
                if (c.isWhite != isWhite)
                    boolArray[CurrentX, i] = true;
                break;
            }
        }

        // Down
        i = CurrentY;
        while (true)
        {
            i--;
            if (i < 0)
                break;
            c = Board.instance.chessPieces[CurrentX, i];
            if (c == null)
            {
                boolArray[CurrentX, i] = true;
            }
            else
            {
                if (c.isWhite != isWhite)
                    boolArray[CurrentX, i] = true;
                break;
            }
        }
       
        return boolArray;
    }

    public override bool[,] ThreateningSquares()
    {
        bool[,] boolArray = new bool[8, 8];

        ChessPiece c;
        int i;

        // Right
        i = CurrentX;
        while (true)
        {
            i++;
            if (i >= 8)
                break;
            c = Board.instance.chessPieces[i, CurrentY];
            if (c == null)
            {
                boolArray[i, CurrentY] = true;
            } else if(c.GetType() == typeof(King))
            {
                boolArray[i, CurrentY] = true;
            }
            else
            {
                boolArray[i, CurrentY] = true;
                break;
            }
        }

        // Left
        i = CurrentX;
        while (true)
        {
            i--;
            if (i < 0)
                break;
            c = Board.instance.chessPieces[i, CurrentY];
            if (c == null)
            {
                boolArray[i, CurrentY] = true;
            }
            else if (c.GetType() == typeof(King))
            {
                boolArray[i, CurrentY] = true;
            }
            else
            {
                boolArray[i, CurrentY] = true;
                break;
            }
        }

        // Up
        i = CurrentY;
        while (true)
        {
            i++;
            if (i >= 8)
                break;
            c = Board.instance.chessPieces[CurrentX, i];
            if (c == null)
            {
                boolArray[CurrentX, i] = true;
            }
            else if (c.GetType() == typeof(King))
            {
                boolArray[CurrentX, i] = true;
            }
            else
            {
                boolArray[CurrentX, i] = true;
                break;
            }
        }

        // Down
        i = CurrentY;
        while (true)
        {
            i--;
            if (i < 0)
                break;
            c = Board.instance.chessPieces[CurrentX, i];
            if (c == null)
            {
                boolArray[CurrentX, i] = true;
            }
            else if (c.GetType() == typeof(King))
            {
                boolArray[CurrentX, i] = true;
            }
            else
            {
                boolArray[CurrentX, i] = true;
                break;
            }
        }

        return boolArray;
    }
}
