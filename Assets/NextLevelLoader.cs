using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelLoader : MonoBehaviour
{
    [SerializeField]
    private string nextLevel;
    public Animator transition;

    public float transtionTime = 1f;

    [SerializeField]
    GameObject soundSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(LoadLevel(nextLevel));
        }
    }
    IEnumerator LoadLevel(string level)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transtionTime);

        DestroyImmediate(soundSystem,true);

        SceneManager.LoadScene(level);
    }
}
