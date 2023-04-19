using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText, highScoreText;

    private int score = 0, highScore = 0;
    private string highScoreKey = "Highscore";

    // Singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        score = 0;
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        highScoreText.text = "HIGH: " + highScore.ToString();
    }

    private void Update()
    {
        scoreText.text = "SCORE: " + score.ToString();
        highScoreText.text = "HIGH: " + highScore.ToString();
    }

    public void AddScore(int points)
    {
        score += points;
    }

    public void EndScore()
    {
        // If new highscore, save as player pref
        if (score > highScore)
        {
            PlayerPrefs.SetInt(highScoreKey, score);
            PlayerPrefs.Save();
        }
        // Set current / high scores
        score = 0;
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
    }
}
