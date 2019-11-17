using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece
{

    public override bool[,] PossibleMove()
    {
        bool[,] boolArray = new bool[8, 8];

        KingMove(CurrentX + 1, CurrentY, ref boolArray); // up
        KingMove(CurrentX - 1, CurrentY, ref boolArray); // down
        KingMove(CurrentX, CurrentY - 1, ref boolArray); // left
        KingMove(CurrentX, CurrentY + 1, ref boolArray); // right
        KingMove(CurrentX + 1, CurrentY - 1, ref boolArray); // up left
        KingMove(CurrentX - 1, CurrentY - 1, ref boolArray); // down left
        KingMove(CurrentX + 1, CurrentY + 1, ref boolArray); // up right
        KingMove(CurrentX - 1, CurrentY + 1, ref boolArray); // down right

        return boolArray;
    }

    public void KingMove(int x, int y, ref bool[,] reference, bool checkThreats = true)
    {
        ChessPiece c;
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            c = Board.instance.chessPieces[x, y];
            if (c == null)
            {
                // check if the square puts the king in check
                if (checkThreats && isSquareThreatened(x, y))
                    reference[x, y] = false;
                else if(checkThreats)
                {
                    reference[x, y] = true;
                }
            }
            else if (isWhite != c.isWhite) // capture possible?
            {
                if (isSquareThreatened(x, y))
                {
                    reference[x, y] = false;
                }else
                {
                    reference[x, y] = true;
                }
            }
        }
    }

    public bool isSquareThreatened(int x, int y)
    {
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                ChessPiece p = Board.instance.chessPieces[i, j]; // check if piece is of same color as the king
                if(p != null && p.isWhite != this.isWhite)
                {
                    // check if opponent piece could reach king on next move
                    bool [,] controlledSquares = p.ThreateningSquares();

                    if (controlledSquares[x, y])
                    {
                        // threatened square
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public override bool[,] ThreateningSquares()
    {
        bool[,] boolArray = new bool[8, 8];

        if (CurrentX >= 0 && CurrentX < 7 && CurrentY >= 0 && CurrentY < 8)
            boolArray[CurrentX+1, CurrentY] = true;

        if (CurrentX > 0 && CurrentX < 8 && CurrentY >= 0 && CurrentY < 8)
            boolArray[CurrentX-1, CurrentY] = true;

        if (CurrentX >= 0 && CurrentX < 8 && CurrentY >= 0 && CurrentY < 7)
            boolArray[CurrentX, CurrentY+1] = true;

        if (CurrentX >= 0 && CurrentX < 8 && CurrentY > 0 && CurrentY < 8)
            boolArray[CurrentX, CurrentY-1] = true;

        if (CurrentX >= 0 && CurrentX < 7 && CurrentY >= 0 && CurrentY < 7)
            boolArray[CurrentX+1, CurrentY+1] = true;

        if (CurrentX >= 0 && CurrentX < 7 && CurrentY > 0 && CurrentY < 8)
            boolArray[CurrentX+1, CurrentY-1] = true;

        if (CurrentX > 0 && CurrentX < 8 && CurrentY >= 0 && CurrentY < 7)
            boolArray[CurrentX-1, CurrentY+1] = true;

        if (CurrentX > 0 && CurrentX < 8 && CurrentY > 0 && CurrentY < 8)
            boolArray[CurrentX-1, CurrentY-1] = true;

        return boolArray;
    }
}
