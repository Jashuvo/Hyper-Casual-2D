using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManagerScript : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI gameScoreText,bestScoreText;
    public int gameScore;

    public static ScoreManagerScript instance;
    
    void Start()
    {
        if(instance == null)
        {
            instance = this;

        }
        gameScore = 0;
        bestScoreText.text = PlayerPrefs.GetInt("Best").ToString();
    }

    public void AddScore()
    {
        gameScore += 1;
        gameScoreText.text = gameScore.ToString();
        if(gameScore > PlayerPrefs.GetInt("Best", 0)){
            bestScoreText.text = gameScore.ToString();
            PlayerPrefs.SetInt("Best", gameScore);
        }
    }
}
