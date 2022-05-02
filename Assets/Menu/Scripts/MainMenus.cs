using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenus : MonoBehaviour
{
    [SerializeField]
    string firstLevel;
    [SerializeField]
    string secondLevel;
    [SerializeField]
    string thirdLevel;

    [SerializeField]
    GameObject options;
    [SerializeField]
    GameObject mainMenu;
    [SerializeField]
    GameObject levelSelect;

    public Animator transition;
    public float transtionTime = 1f;

    public void StartGame()
    {
        StartCoroutine(LoadLevel(firstLevel));
    }
    public void StartLevel2()
    {
        StartCoroutine(LoadLevel(secondLevel));
    }
    public void StartLevel3()
    {
        StartCoroutine(LoadLevel(thirdLevel));
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

    public void Start()
    {
        StartCoroutine(AppearMenu());
    }

    IEnumerator LoadLevel(string level)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transtionTime);

        SceneManager.LoadScene(level);
    }

    IEnumerator AppearMenu()
    {
        yield return new WaitForSeconds(20f);
        mainMenu.SetActive(true);
    }
}
