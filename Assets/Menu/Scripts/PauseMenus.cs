using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenus : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    string mainMenu;

    [SerializeField]
    GameObject options;

    [SerializeField]
    GameObject pause;

    [SerializeField]
    GameObject gameOver;

    bool gameIsPaused;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                if(options.GetComponent<Canvas>().enabled == true)
                {
                    Return();
                }
                else
                {
                    Resume();
                }
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void Resume()
    {
        pause.GetComponent<Canvas>().enabled = false;
        gameIsPaused = false;
        Time.timeScale = 1f;
    }
    public void PauseGame()
    {
        pause.GetComponent<Canvas>().enabled = true;
        gameIsPaused = true;
        Time.timeScale = 0f;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void Options()
    {
        options.GetComponent<Canvas>().enabled = true;
        pause.GetComponent<Canvas>().enabled = false;
    }

    public void Return()
    {
        options.GetComponent<Canvas>().enabled = false;
        pause.GetComponent<Canvas>().enabled = true;

    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        gameOver.GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0f;
    }
}
