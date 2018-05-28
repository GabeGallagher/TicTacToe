using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Text title;

    bool isClickable = false;

    private void OnMouseEnter()
    {
        isClickable = true;
    }

    private void OnMouseExit()
    {
        isClickable = false;
    }

    private void OnMouseDown()
    {
        if (isClickable)
        {
            transform.parent.parent.Find("ButtonsParent_01").gameObject.SetActive(true);
            title.text = "Select a Board Size to Start Your Game!";
            transform.parent.gameObject.SetActive(false);
        }
    }
}
