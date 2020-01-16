using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManagerScript : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI gameScoreText;
    int gameScore;

    public static ScoreManagerScript instance;
    
    void Start()
    {
        if(instance == null)
        {
            instance = this;

        }
        gameScore = 0;
    }

    public void AddScore()
    {
        gameScore += 1;
        gameScoreText.text = gameScore.ToString();
    }
}
