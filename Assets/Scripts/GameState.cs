using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//simple class which records the moves from the game
public class MoveRecord
{
    public MoveRecord(int[] loc, bool player)
    {
        location = loc;
        playerOne = player;
    }

    int[] location;
    bool playerOne;
}

//class used as container for MoveRecords and making them iterable through doubly linked list
public class Node
{
    public Node(MoveRecord move) { m = move; }

    MoveRecord m;
    Node prev;
    Node next;

    public void SetPrev(Node node) { prev = node; Debug.Log(prev); }
    public Node GetPrev() { return prev; }

    public void SetNext(Node node) { next = node; }
}

//Doubly linked list which allows traversal through moveset as if it was a replay
public class MoveRecorder
{
    Node head;
    Node tail;

    public void Turn(MoveRecord move, Node previousMove)
    {
        Node turn = new Node(move);

        if (head == null) { head = turn; }

        if (previousMove == null) { turn.SetPrev(head); }
        else { turn.SetPrev(previousMove); }
        previousMove.SetNext(turn);
        turn.SetNext(null);
        tail = turn;
    }
}

public class GameState : MonoBehaviour
{
    public Text turnText;

    public List<GameObject> pieces;

    public GameObject playerOnePiece, playerTwoPiece;

    //board is size x size
    public int size;

    GameObject buttons;

    MoveRecorder moveRecorder;

    Node previousMove;
    
    int[,] board;

    int turnNumber = 0;

    bool playerOneTurn = true;

    bool gameOver = false;

    private void Start()
    {
        //initialize board
        board = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++) { board[i, j] = 0; }
        }

        //randomly assign player pieces
        int pieceIndex = Random.Range(0, pieces.Count);
        playerOnePiece = pieces[pieceIndex];
        List<GameObject> newSelection = new List<GameObject>();
        for (int i = 0; i < pieces.Count; i++)
        {
            if (i != pieceIndex) { newSelection.Add(pieces[i]); }
        }
        playerTwoPiece = newSelection[Random.Range(0, newSelection.Count)];

        //Functionality for buttons that appear when the game is over
        buttons = GameObject.Find("ButtonsObject");
        if (buttons == null) { Debug.Log("Unable to find reference to ButtonsObject"); }
        else { buttons.SetActive(false); }

        //Setting up the move recorder
        //since board is zero indexed, { -1, -1 } is functionally the empty board move
        MoveRecord initialBoardState = new MoveRecord(new int[] { -1, -1 }, false);
        moveRecorder = new MoveRecorder();
        moveRecorder.Turn(initialBoardState, null);
    }

    void FinishGame()
    {
        gameOver = true;

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<TileControl>().SetIsPlaceable(false);
        }

        buttons.SetActive(true);

        if (playerOneTurn) { turnText.text = "Player 2 Wins!"; }
        else { turnText.text = "Player 1 Wins!"; }
    }
    
    void CheckCount(int count)
    {
        if ((count == size && !playerOneTurn) || (count == -1 * size && playerOneTurn)) { FinishGame(); }
    }
    
    void CheckWin(int[] location)
    {
        int count = 0;

        //check row
        for (int i = 0; i < size; i++) { count += board[location[0], i]; }

        CheckCount(count);

        count = 0;

        //check column
        for (int i = 0; i < size; i++) { count += board[i, location[1]]; }

        CheckCount(count);
        
        count = 0;

        //if most recent piece was placed on a tile that could be a diagonal winner
        if ((location[0] == 0 && location[1] == size - 1)
            || (location[0] == size - 1 && location[1] == 0)
            || (location[0] == location[1]))
        {
            //check diag
            for (int i = 0; i < size; i++) { count += board[i, i]; }

            CheckCount(count);
            
            count = 0;

            //check other diag
            int j = size - 1;

            for (int i = 0; i < size; i++)
            {
                count += board[i, j];
                j--;
            }

            CheckCount(count); 
        }
        
        //Handle tie
        if(turnNumber == size * size)
        {
            FinishGame();
            turnText.text = "Tie Game!";
        }
    }

    //place pieces on board
    public void InstantiatePiece(GameObject tile, GameObject piecePrefab, int scoreInt)
    {
        TileControl tileControl = tile.GetComponent<TileControl>();

        GameObject piece = Instantiate(piecePrefab, transform.position, Quaternion.identity)
            as GameObject;
        piece.transform.parent = tile.transform;
        piece.transform.localPosition = Vector2.zero;
        playerOneTurn = false;
        tileControl.SetIsPlaceable(false);
        tileControl.GetSprite().color = Color.white;
        board[tileControl.location[0], tileControl.location[1]] = scoreInt;

        MoveRecord turn = new MoveRecord(tileControl.location, playerOneTurn);
        moveRecorder.Turn(turn, previousMove);
        if (turnNumber >= (size * 2) - 1) { CheckWin(tileControl.location); }
    }

    //Change piece based on whose turn it is
    public void Place(GameObject tile)
    {
        turnNumber++;

        if (playerOneTurn)
        {
            GameObject piecePrefab = playerOnePiece;
            InstantiatePiece(tile, piecePrefab, 1);
        }
        else
        {
            GameObject piecePrefab = playerTwoPiece;
            InstantiatePiece(tile, piecePrefab, -1);
        }
    }

    private void Update()
    {
        if (!gameOver)
        {
            if (playerOneTurn) { turnText.text = "Player 1 Turn"; }
            else { turnText.text = "Player 2 Turn"; } 
        }
    }
}
