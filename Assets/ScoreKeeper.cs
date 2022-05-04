using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public static float Score { get; set; }

    [SerializeField]
    Text scoreText;

    void Update()
    {
        scoreText.text = "Score: " + Score;
    }

    public void AddScore(float points)
    {
        Score += points;
    }
}
