using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
    public Text title;

    public GameObject buttonsParent, buttonsParent_01;

    bool isClickable = false;

    public void EnableNewButtons()
    {
        buttonsParent_01.SetActive(true);
        title.text = "Select a Board Size to Start Your Game!";
        buttonsParent.SetActive(false);
    }
}
