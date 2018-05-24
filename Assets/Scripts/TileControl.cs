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

    private void OnMouseEnter()
    {
        //change tile color
        //Debug.Log("Moused over " + gameObject.name);
        sprite.color = mouseOverColor;
        isClickable = true;
    }

    private void OnMouseExit()
    {
        //Debug.Log("Mouse off " + gameObject.name);
        sprite.color = Color.white;
        isClickable = false;
    }

    private void OnMouseDown()
    {
        if (isClickable) { sprite.color = clickedColor; }
    }

    private void OnMouseUp()
    {
        sprite.color = mouseOverColor;
    }
}
