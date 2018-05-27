using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public Text turnText;

    public List<GameObject> pieces;

    public GameObject playerOnePiece, playerTwoPiece;

    GameObject buttons;

    int[,] board = {  { 0, 0, 0 },
                      { 0, 0, 0 },
                      { 0, 0, 0 }
                    };

    bool playerOneTurn = true;

    bool gameOver = false;

    private void Start()
    {
        int pieceIndex = Random.Range(0, pieces.Count);
        playerOnePiece = pieces[pieceIndex];
        List<GameObject> newSelection = new List<GameObject>();
        for (int i = 0; i < pieces.Count; i++)
        {
            if (i != pieceIndex) { newSelection.Add(pieces[i]); }
        }
        playerTwoPiece = newSelection[Random.Range(0, newSelection.Count)];

        buttons = GameObject.Find("ButtonsObject");
        if (buttons == null) { Debug.Log("Unable to find reference to ButtonsObject"); }
        else { buttons.SetActive(false); }
    }

    void CheckCount(int count)
    {
        if ((count == 3 && !playerOneTurn) || (count == -3 && playerOneTurn))
        {
            gameOver = true;

            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).GetComponent<TileControl>().isPlaceable = false;
            }

            buttons.SetActive(false);

            if (playerOneTurn) { turnText.text = "Player 2 Wins!"; }
            else { turnText.text = "Player 1 Wins!"; }
        }
    }

    void CheckWin(int[] location)
    {
        int count = 0;

        //check row
        for (int i = 0; i < 3; i++) { count += board[location[0], i]; }

        CheckCount(count);

        count = 0;

        //check column
        for (int i = 0; i < 3; i++) { count += board[i, location[1]]; }

        CheckCount(count);
        
        count = 0;

        //if most recent piece was placed on a tile that could be a diagonal winner
        if ((location[0] == 0 && location[1] == 2)
            || (location[0] == 2 && location[1] == 0)
            || (location[0] == location[1]))
        {
            //check diag
            for (int i = 0; i < 3; i++) { count += board[i, i]; }

            CheckCount(count);
            
            count = 0;

            //check other diag
            int j = 2;

            for (int i = 0; i < 3; i++)
            {
                count += board[i, j];
                j--;
            }

            CheckCount(count); 
        }
    }

    public void Place(GameObject tile)
    {
        TileControl tileControl = tile.GetComponent<TileControl>();
        if (playerOneTurn)
        {
            GameObject piece = Instantiate(playerOnePiece, transform.position, Quaternion.identity)
                as GameObject;
            piece.transform.parent = tile.transform;
            piece.transform.localPosition = Vector2.zero;
            playerOneTurn = false;
            tileControl.isPlaceable = false;
            tileControl.GetSprite().color = Color.white;
            board[tileControl.location[0], tileControl.location[1]] = 1;
        }
        else
        {
            GameObject piece = Instantiate(playerTwoPiece, transform.position, Quaternion.identity)
                as GameObject;
            piece.transform.parent = tile.transform;
            piece.transform.localPosition = Vector2.zero;
            playerOneTurn = true;
            tileControl.isPlaceable = false;
            tileControl.GetSprite().color = Color.white;
            board[tileControl.location[0], tileControl.location[1]] = -1;
        }
        CheckWin(tileControl.location);
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
