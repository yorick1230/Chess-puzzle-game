using Assets.scripts;
using Assets.scripts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public static Board instance { set; get; }
    private bool[,] allowedMoves { set; get; }
    public ChessPiece[,] chessPieces { set; get; }
    private ChessPiece selectedPiece;

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int puzzleCount = 0;
    private List<string> currentMoves;

    private int selectionX = -1;
    private int selectionY = -1;

    public List<GameObject> piecePrefabs;
    private List<GameObject> activePieces;
    private Quaternion orientation = Quaternion.Euler(0, 90, 0);

    public bool isWhiteTurn = true;
    public bool isPlayerWhite = true;

    private int puzzlesCompleted = 0;
    public Text puzzlesCompletedUi;
    public Text moveColor;

    private void Start()
    {
        instance = this;
        SetupChessPuzzle();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateSelection();
        ComputerMove();

        if (Input.GetMouseButtonDown(0))
        {
            if(selectionX>=0 && selectionY >= 0)
            {
                if(selectedPiece == null)
                {
                    // select the piece
                    SelectPiece(selectionX, selectionY);
                }
                else
                {
                    // move the piece
                    MovePiece(selectionX, selectionY);
                }
            }
        }
    }

    // Computer reads the list of moves and executes them when its their move
    private void ComputerMove()
    {
        // if computer's turn
        if(isPlayerWhite && !isWhiteTurn || !isPlayerWhite && isWhiteTurn)
        {
            if (currentMoves.Count > 0)
            {
                // convert coordinates to numbers and make computer move
                Coordinate coord = convertCoordinates(currentMoves.ElementAt(0));
                SelectPiece(coord.fromX, coord.fromY);
                MovePiece(coord.toX, coord.toY, true);
            }

            if (currentMoves.Count <= 0)
            {
                // advance to next puzzle
                puzzlesCompleted++;
                puzzlesCompletedUi = GameObject.Find("PuzzleScore").GetComponent<Text>();
                puzzlesCompletedUi.text = "Puzzles completed: " + puzzlesCompleted;

                puzzleCount++;
                clearField();
                SetupChessPuzzle();
                return;
            }
        }
    }

    // Selects a piece to move, returns void if no piece on given coordinates
    private void SelectPiece(int x, int y)
    {
        Debug.Log("Selected: x:" + x + " y:" + y);
        if (chessPieces[x, y] == null)
            return;

        if (chessPieces[x, y].isWhite != isWhiteTurn)
            return;

        allowedMoves = chessPieces[x, y].PossibleMove();
        selectedPiece = chessPieces[x, y];
        BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
    }

    // Moves a piece on the board (and captures if needed)
    private void MovePiece(int x, int y, bool computerMove = false)
    {
        if (allowedMoves[x,y])
        {
            ChessPiece c = chessPieces[x, y];
           
            if(computerMove || isCorrectMove(selectedPiece.CurrentX, selectedPiece.CurrentY, x, y))
            {
                if (c != null && c.isWhite != isWhiteTurn)
                {
                    // Capture a piece
                    activePieces.Remove(c.gameObject);
                    Destroy(c.gameObject);
                }

                chessPieces[selectedPiece.CurrentX, selectedPiece.CurrentY] = null;
                selectedPiece.transform.position = GetTileCenter(x, y);
                selectedPiece.SetPosition(x, y);
                chessPieces[x, y] = selectedPiece;
                isWhiteTurn = !isWhiteTurn;

                if(currentMoves.Count > 0)
                {
                    currentMoves.RemoveAt(0); // remove first move, since its already played on the board
                }
                if(!computerMove)
                    Debug.Log("Correct!");
            }else
            {
                if (!computerMove)
                    Debug.Log("Incorrect, try again!");
            }
        }
        BoardHighlights.Instance.Hidehighlights();
        selectedPiece = null;
    }

    // Updates the selection based on user mouse input
    private void UpdateSelection()
    {
        if (isPlayerWhite && !isWhiteTurn || !isPlayerWhite && isWhiteTurn)
            return;

        if (!Camera.main)
            return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }

    // Spawns a chesspiece
    private void SpawnChessPiece(int index, int x, int y) 
    {
        GameObject go = Instantiate(piecePrefabs[index], GetTileCenter(x,y), orientation) as GameObject;
        go.transform.SetParent(transform);
        chessPieces[x, y] = go.GetComponent<ChessPiece>();
        chessPieces[x, y].SetPosition(x, y);
        activePieces.Add(go);
    }

    // clears the field of chesspieces.
    private void clearField()
    {
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                ChessPiece c = chessPieces[i, j];
                if (c == null)
                    continue;
                Destroy(c.gameObject);
                c = null;
            }
        }
    }

    // sets up the board with the given puzzle
    private void SetupChessPuzzle()
    {
        activePieces = new List<GameObject>();
        chessPieces = new ChessPiece[8, 8];

        // Retrieve new puzzle
        HttpClient hc = new HttpClient();
        Puzzle ActivePuzzle = hc.retrieveRandomPuzzle();

        if(ActivePuzzle == null)
        {
            // TODO: Show error
            Debug.Log("Error: Geen verbinding met API");
            return;
        }

        FenParser.FenParser parser = new FenParser.FenParser(ActivePuzzle.FenBefore);
        isPlayerWhite = parser.BoardStateData.ActivePlayerColor.ToLower() == "white" ? false : true; // First turn is computer
        isWhiteTurn = parser.BoardStateData.ActivePlayerColor.ToLower() == "white" ? true : false;
        currentMoves = ActivePuzzle.ForcedLine;
        Debug.Log(String.Join(", ", currentMoves.ToArray()));
        currentMoves.Insert(0, ActivePuzzle.BlunderMove);
        

        moveColor = GameObject.Find("MoveColor").GetComponent<Text>();
        moveColor.text = isPlayerWhite ? "White to move" : "Black to move";

        for (int i = 0; i < 8; i++)
        {
            string[] currentRank = parser.BoardStateData.Ranks[7-i];
            for (int j = 0; j < 8; j++)
            {
                switch (currentRank[j].ToString())
                {
                    case "P":
                        // white pawn
                        SpawnChessPiece(5, j, i);
                        break;
                    case "N":
                        // white knight
                        SpawnChessPiece(4, j, i);
                        break;
                    case "B":
                        // white bishop
                        SpawnChessPiece(3, j, i);
                        break;
                    case "R":
                        // white rook
                        SpawnChessPiece(2, j, i);
                        break;
                    case "Q":
                        // white queen
                        SpawnChessPiece(1, j, i);
                        break;
                    case "K":
                        // white king
                        SpawnChessPiece(0, j, i);
                        break;
                    // BLACK PIECES
                    case "p":
                        // black pawn
                        SpawnChessPiece(11, j, i);
                        break;
                    case "n":
                        // black knight
                        SpawnChessPiece(10, j, i);
                        break;
                    case "b":
                        // black bishop
                        SpawnChessPiece(9, j, i);
                        break;
                    case "r":
                        // black rook
                        SpawnChessPiece(8, j, i);
                        break;
                    case "q":
                        // black queen
                        SpawnChessPiece(7, j, i);
                        break;
                    case "k":
                        // black king
                        SpawnChessPiece(6, j, i);
                        break;
                }
            }
        }
    }

    // Converts chess coordinates (e.g. Rxb4) to number coordinates used by the game (e.g. [1,2])
    private Coordinate convertCoordinates(string coord)
    {
        char[] columns = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
        int fromX, fromY, toX, toY;

        Debug.Log(coord);

        // Check if the move contains a 'x'
        if (coord.Substring(1, 1) == "x")
        {
            // piece to be captured
            toX = Array.IndexOf(columns, char.Parse(coord.Substring(2, 1)));
            toY = Int32.Parse(coord.Substring(3, 1) + "") - 1;

            // Check which piece is capturing
            if (columns.Contains(char.Parse(coord.Substring(0, 1))))
            {
                // not a major piece, so it is a pawn
                // pawn must be one row lower/higher than target piece
                fromX = Array.IndexOf(columns, char.Parse(coord.Substring(0, 1)));

                // Check what color target piece is
                ChessPiece pc = chessPieces[toX, toY];
                if(pc.isWhite)
                    fromY = toY + 1;
                else
                    fromY = toY - 1;
            }
            else
            {
                // major piece
                ChessPiece piece = this.getMajorPiece(coord.Substring(0, 1), toX, toY);
                fromX = piece.CurrentX;
                fromY = piece.CurrentY;
            }
        }
        else
        {
            // Check if first character is a major piece
            if(columns.Contains(char.Parse(coord.Substring(0, 1)))){
                // not a major piece, its a pawn
                toX = Array.IndexOf(columns, char.Parse(coord.Substring(0, 1)));
                toY = Int32.Parse(coord.Substring(1, 1) + "") - 1;

                fromX = toX;
                
                if (isWhiteTurn)
                    fromY = toY - 1;
                else
                    fromY = toY + 1; 

                // Check if pawn is moving 1 or 2 spaces
                if (chessPieces[fromX, fromY] == null)
                {
                    fromX = toX;
                    if (isWhiteTurn)
                        fromY = toY - 2;
                    else
                        fromY = toY + 2;
                }
            }else
            {
                // Check if the length is higher than normal for a single coordinate (e.g. Rdc8 instead of Rc8)
                if (coord.Length > 3 && coord[coord.Length-1] != '+' && coord[coord.Length - 1] != '#')
                {
                    int pieceX = Array.IndexOf(columns, char.Parse(coord.Substring(1, 1)));
                    toX = Array.IndexOf(columns, char.Parse(coord.Substring(2, 1)));
                    toY = Int32.Parse(coord.Substring(3, 1) + "") - 1;

                    ChessPiece piece = this.getMajorPiece(coord.Substring(0, 1), toX, toY, pieceX);

                    fromX = piece.CurrentX;
                    fromY = piece.CurrentY;
                }
                else
                {
                    toX = Array.IndexOf(columns, char.Parse(coord.Substring(1, 1)));
                    toY = Int32.Parse(coord.Substring(2, 1) + "") - 1;

                    ChessPiece piece = this.getMajorPiece(coord.Substring(0, 1), toX, toY);

                    fromX = piece.CurrentX;
                    fromY = piece.CurrentY;
                }
            }
        }

        return new Coordinate(fromX, fromY, toX, toY);
    }

    // Finds coordinates for a major piece, since chess notation doesnt give the location
    private ChessPiece getMajorPiece(string pieceType, int targetX, int targetY, int fromX = -1)
    {
        ChessPiece attacker = null, targetPiece = chessPieces[targetX, targetY];
        Dictionary<string, string> majorPieceDict = new Dictionary<string, string>();
        majorPieceDict.Add("N", "knight");
        majorPieceDict.Add("K", "king");
        majorPieceDict.Add("Q", "queen");
        majorPieceDict.Add("B", "bishop");
        majorPieceDict.Add("R", "rook");
        
        List<ChessPiece> majorPieces = findChessPiecesWithType(majorPieceDict[pieceType]);
        foreach (ChessPiece piece in majorPieces)
        {
            if (piece.PossibleMove()[targetX, targetY])
            {
                if (isWhiteTurn == piece.isWhite)
                {
                    if (fromX == -1) // no need to check if attacker's coords match fromX
                    {
                        attacker = piece;
                        break;
                    }else
                    {
                        if(attacker.CurrentX == fromX)
                        {
                            attacker = piece;
                            break;
                        }
                    }
                }
            }
        }
        return attacker;
    }

    // Finds all chess pieces with given type
    private List<ChessPiece> findChessPiecesWithType(String type)
    {
        List<ChessPiece> pieces = new List<ChessPiece>();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (chessPieces[i, j] != null && chessPieces[i, j].GetType().ToString().ToLower() == type)
                    pieces.Add(chessPieces[i, j]);
            }
        }
        return pieces;
    }


    // Checks if the users move is 'correct' (according to the puzzle, not legal move).
    private bool isCorrectMove(int fromX, int fromY, int toX, int toY)
    {
        // convert coordinates to numbers
        Coordinate coord = convertCoordinates(currentMoves[0]); // get correct coordinates

        // compare coordinates
        if (coord.fromX == fromX && coord.fromY == fromY &&
            coord.toX == toX && coord.toY == toY)
        {
            return true;
        }
        return false;

    }

    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        return origin;
    }
}
