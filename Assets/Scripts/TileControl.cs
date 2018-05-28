using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileControl : MonoBehaviour
{
    public Color mouseOverColor, clickedColor;

    public int[] location;

    SpriteRenderer sprite;

    GameState gameState;

    bool isClickable = false;

    public bool isPlaceable = true;

    private void Start()
    {
        gameState = gameObject.transform.parent.GetComponent<GameState>();
        sprite = gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>();
        if (sprite == null)
        {
            Debug.Log("Unable to reference " + gameObject.name + "'s sprite");
        }
    }

    public SpriteRenderer GetSprite()
    {
        return sprite;
    }

    //change color on hover
    private void OnMouseEnter()
    {
        if (isPlaceable)
        {
            sprite.color = mouseOverColor;
            isClickable = true;
        }
    }

    //revert color
    private void OnMouseExit()
    {
        sprite.color = Color.white;
        isClickable = false;
    }

    private void OnMouseDown()
    {
        if (isClickable) { sprite.color = clickedColor; }
    }

    //place object
    private void OnMouseUp()
    {
        if (isPlaceable)
        {
            sprite.color = mouseOverColor;
            gameState.Place(gameObject); 
        }
    }
}
