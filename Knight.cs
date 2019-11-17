using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessPiece
{
    public override bool[,] PossibleMove()
    {
        bool[,] boolArray = new bool[8, 8];

        // Up left
        KnightMove(CurrentX - 1, CurrentY + 2, ref boolArray);
        // Up right
        KnightMove(CurrentX + 1, CurrentY + 2, ref boolArray);
        // Right up
        KnightMove(CurrentX + 2, CurrentY + 1, ref boolArray);
        // Right down
        KnightMove(CurrentX + 2, CurrentY -1, ref boolArray);
        // Down left
        KnightMove(CurrentX - 1, CurrentY - 2, ref boolArray);
        // Down right
        KnightMove(CurrentX + 1, CurrentY - 2, ref boolArray);
        // Left up
        KnightMove(CurrentX - 2, CurrentY + 1, ref boolArray);
        // Left down
        KnightMove(CurrentX - 2, CurrentY - 1, ref boolArray);

        return boolArray;
    }

    public override bool[,] ThreateningSquares()
    {
        bool[,] boolArray = new bool[8, 8];

        // Up left
        KnightMove(CurrentX - 1, CurrentY + 2, ref boolArray, true);
        // Up right
        KnightMove(CurrentX + 1, CurrentY + 2, ref boolArray, true);
        // Right up
        KnightMove(CurrentX + 2, CurrentY + 1, ref boolArray, true);
        // Right down
        KnightMove(CurrentX + 2, CurrentY - 1, ref boolArray, true);
        // Down left
        KnightMove(CurrentX - 1, CurrentY - 2, ref boolArray, true);
        // Down right
        KnightMove(CurrentX + 1, CurrentY - 2, ref boolArray, true);
        // Left up
        KnightMove(CurrentX - 2, CurrentY + 1, ref boolArray, true);
        // Left down
        KnightMove(CurrentX - 2, CurrentY - 1, ref boolArray, true);

        return boolArray;
    }

    public void KnightMove(int x, int y, ref bool[,] reference, bool checkThreats = false)
    {
        ChessPiece c;

        if(x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            c = Board.instance.chessPieces[x, y];
            if(c == null)
            {
                reference[x, y] = true;
            }else if (checkThreats)
            {
                reference[x, y] = true;
            }
            else
            {
                if(isWhite != c.isWhite)
                {
                    reference[x, y] = true;
                }
            }
        }
    }
}
