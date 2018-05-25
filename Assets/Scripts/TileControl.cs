using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileControl : MonoBehaviour
{
    public Color mouseOverColor, clickedColor;

    SpriteRenderer sprite;

    bool isClickable = false;

    private void Start()
    {
        sprite = gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>();
        if (sprite == null)
        {
            Debug.Log("Unable to reference " + gameObject.name + "'s sprite");
        }
    }

    //change color on hover
    private void OnMouseEnter()
    {
        sprite.color = mouseOverColor;
        isClickable = true;
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
        sprite.color = mouseOverColor;
    }
}
