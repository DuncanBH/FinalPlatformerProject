using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenus : MonoBehaviour
{
    [SerializeField]
    string firstLevel;

    [SerializeField]
    GameObject options;

    [SerializeField]
    GameObject mainMenu;

    [SerializeField]
    GameObject levelSelect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene(firstLevel);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
        options.GetComponent<Canvas>().enabled = true;
        mainMenu.GetComponent<Canvas>().enabled = false;
    }

    public void LevelSelect()
    {
        levelSelect.GetComponent<Canvas>().enabled = true;
        mainMenu.GetComponent<Canvas>().enabled = false;
    }
    public void Return()
    {
        levelSelect.GetComponent<Canvas>().enabled = false;
        options.GetComponent<Canvas>().enabled = false;
        mainMenu.GetComponent<Canvas>().enabled = true;
    }
}
