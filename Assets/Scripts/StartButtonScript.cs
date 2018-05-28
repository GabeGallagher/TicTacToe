using UnityEngine;
using UnityEngine.UI;

public class StartButtonScript : MonoBehaviour
{
    public Text title;

    public GameObject buttonsParent, buttonsParent_01;

    public void EnableNewButtons()
    {
        buttonsParent_01.SetActive(true);
        title.text = "Select a Board Size to Start Your Game!";
        buttonsParent.SetActive(false);
    }
}
