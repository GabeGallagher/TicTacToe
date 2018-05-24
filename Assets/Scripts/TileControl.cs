using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileControl : MonoBehaviour
{
    public Color color;

    SpriteRenderer sprite;

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
        sprite.color = color;
    }

    private void OnMouseExit()
    {
        //Debug.Log("Mouse off " + gameObject.name);
        sprite.color = Color.white;
    }
}
