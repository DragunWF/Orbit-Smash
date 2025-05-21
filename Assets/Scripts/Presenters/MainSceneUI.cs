using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public sealed class MainSceneUI : MonoBehaviour
{
    private const string SCORE_TEXT = "ScoreText";
    private const string DIFFICULTY_TEXT = "DifficultyText";
    private const string TIME_SURVIVED_TEXT = "TimeSurvivedText";

    private TextMeshProUGUI scoreText, difficultyText, timeSurvivedText;

    private void Awake()
    {
        scoreText = GameObject.Find(SCORE_TEXT).GetComponent<TextMeshProUGUI>();
        difficultyText = GameObject.Find(DIFFICULTY_TEXT).GetComponent<TextMeshProUGUI>();
        timeSurvivedText = GameObject.Find(TIME_SURVIVED_TEXT).GetComponent<TextMeshProUGUI>();
    }

    public void SetScoreText(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    public void SetDifficultyText(int difficulty)
    {
        difficultyText.text = $"Difficulty Level: {difficulty}";
    }

    public void SetTimeSurvivedText(int seconds)
    {
        timeSurvivedText.text = $"Time Survived in Seconds: {seconds}";
    }
}
