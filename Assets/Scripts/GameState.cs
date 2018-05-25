using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public Text turnText;

    public List<GameObject> pieces;

    public GameObject playerOnePiece, playerTwoPiece;

    bool playerOneTurn = true;

    private void Start()
    {
        if (playerOneTurn) { turnText.text = "Player 1 Turn"; }
        int pieceIndex = Random.Range(0, pieces.Count);
        playerOnePiece = pieces[pieceIndex];
        List<GameObject> newSelection = new List<GameObject>();
        for (int i = 0; i < pieces.Count; ++i)
        {
            if (i != pieceIndex) { newSelection.Add(pieces[i]); }
        }
        playerTwoPiece = newSelection[Random.Range(0, newSelection.Count)];
    }

    public void Place(GameObject tile)
    {
        if (playerOneTurn)
        {
            GameObject piece = Instantiate(playerOnePiece, transform.position, Quaternion.identity)
                as GameObject;
            piece.transform.parent = tile.transform;
            piece.transform.localPosition = Vector2.zero;
        }
    }
}
