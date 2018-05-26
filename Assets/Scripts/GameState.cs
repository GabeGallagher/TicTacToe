using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public Text turnText;

    public List<GameObject> pieces;

    public GameObject playerOnePiece, playerTwoPiece;

    int[,] board = {  { 0, 0, 0 },
                      { 0, 0, 0 },
                      { 0, 0, 0 }
                    };

    bool playerOneTurn = true;

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
            board[tileControl.location[0], tileControl.location[1]] = 2;
        }
    }

    private void Update()
    {
        if (playerOneTurn) { turnText.text = "Player 1 Turn"; }
        else { turnText.text = "Player 2 Turn"; }
    }
}
