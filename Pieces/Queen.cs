using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessPiece
{
    public override bool[,] PossibleMove()
    {
        bool[,] boolArray = new bool[8, 8];

        ChessPiece c;
        int i, j;

        // Right
        i = CurrentX;
        while (true)
        {
            i++;
            if (i >= 8)
                break;
            c = Board.instance.chessPieces[i, CurrentY];
            if (c == null)
                boolArray[i, CurrentY] = true;
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
                boolArray[i, CurrentY] = true;
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
                boolArray[CurrentX, i] = true;
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
                boolArray[CurrentX, i] = true;
            else
            {
                if (c.isWhite != isWhite)
                    boolArray[CurrentX, i] = true;
                break;
            }
        }

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

        // Diagonal

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
            else
            {
                if (isWhite != c.isWhite)
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
            } else if (c.GetType() == typeof(King)) {
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
            if (c == null) { 
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

        // Diagonal

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
