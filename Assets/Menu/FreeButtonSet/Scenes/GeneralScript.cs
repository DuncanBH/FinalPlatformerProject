using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
