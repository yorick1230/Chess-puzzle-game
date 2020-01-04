using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPiece
{
    public override bool[,] PossibleMove()
    {
        bool[,] boolArray = new bool[8, 8];

        ChessPiece c;
        int i, j;

        // Top Left
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i--;
            j++;
            if (i < 0 || j >= 8)
                break;

            c = Board.instance.chessPieces[i, j];
            if(c == null)
            {
                boolArray[i, j] = true;
            }else
            {
                if(isWhite != c.isWhite)
                {
                    boolArray[i, j] = true;
                }
                break;
            }
        }

        // Top Right
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i++;
            j++;
            if (i >= 8 || j >= 8)
                break;

            c = Board.instance.chessPieces[i, j];
            if (c == null)
            {
                boolArray[i, j] = true;
            }
            else
            {
                if (isWhite != c.isWhite)
                {
                    boolArray[i, j] = true;
                }
                break;
            }
        }

        // Down Left
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i--;
            j--;
            if (i < 0 || j < 0)
                break;

            c = Board.instance.chessPieces[i, j];
            if (c == null)
            {
                boolArray[i, j] = true;
            }
            else
            {
                if (isWhite != c.isWhite)
                {
                    boolArray[i, j] = true;
                }
                break;
            }
        }

        // Down Right
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i++;
            j--;
            if (i >= 8 || j < 0)
                break;

            c = Board.instance.chessPieces[i, j];
            if (c == null)
            {
                boolArray[i, j] = true;
            }
            else
            {
                if (isWhite != c.isWhite)
                {
                    boolArray[i, j] = true;
                }
                break;
            }
        }

        return boolArray;
    }

    public override bool[,] ThreateningSquares()
    {
        bool[,] boolArray = new bool[8, 8];

        ChessPiece c;
        int i, j;

        // Top Left
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i--;
            j++;
            if (i < 0 || j >= 8)
                break;

            c = Board.instance.chessPieces[i, j];
            if (c == null)
            {
                boolArray[i, j] = true;
            }
            else if (c.GetType() == typeof(King))
            {
                boolArray[i, j] = true;
            }
            else
            {
                boolArray[i, j] = true;
                break;
            }
        }

        // Top Right
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i++;
            j++;
            if (i >= 8 || j >= 8)
                break;

            c = Board.instance.chessPieces[i, j];
            if (c == null)
            {
                boolArray[i, j] = true;
            }
            else if (c.GetType() == typeof(King))
            {
                boolArray[i, j] = true;
            }
            else
            {
                boolArray[i, j] = true;
                break;
            }
        }

        // Down Left
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i--;
            j--;
            if (i < 0 || j < 0)
                break;

            c = Board.instance.chessPieces[i, j];
            if (c == null)
            {
                boolArray[i, j] = true;
            }
            else if (c.GetType() == typeof(King))
            {
                boolArray[i, j] = true;
            }
            else
            {
                boolArray[i, j] = true;
                break;
            }
        }

        // Down Right
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i++;
            j--;
            if (i >= 8 || j < 0)
                break;

            c = Board.instance.chessPieces[i, j];
            if (c == null)
            {
                boolArray[i, j] = true;
            }
            else if (c.GetType() == typeof(King))
            {
                boolArray[i, j] = true;
            }
            else
            {
                boolArray[i, j] = true;
                break;
            }
        }

        return boolArray;
    }
}
