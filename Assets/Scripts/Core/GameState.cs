using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameState : MonoBehaviour
{
    private const int ADDITIONAL_SCORE_PER_DIFFICULTY_LEVEL = 5;

    private static int highScore = 0;
    private static bool isNewHighScore = false;
    private int score = 0;

    private MainSceneUI mainSceneUi;
    private DifficultyScaler difficultyScaler;
    private GameManager gameManager;

    private void Awake()
    {
        mainSceneUi = GetComponent<MainSceneUI>();
        difficultyScaler = GetComponent<DifficultyScaler>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd + (ADDITIONAL_SCORE_PER_DIFFICULTY_LEVEL * difficultyScaler.GetDifficultyLevel());
        mainSceneUi.SetScoreText(score);
    }

    public void EndGame()
    {
        if (score > highScore)
        {
            highScore = score;
            isNewHighScore = true;
        }
        // TODO: Implement fade effect and high scores as well as scene transition to game over scene

        gameManager.LoadGameOverMenu();
    }

    public static int GetHighScore() => highScore;
}
