using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameState : MonoBehaviour
{
    private const int ADDITIONAL_SCORE_PER_DIFFICULTY_LEVEL = 5;

    private static int highScore = 0;
    private static bool isNewHighScore = false;
    private static int score = 0;

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

        gameManager.LoadGameOverMenu();
    }

    public static void ResetGameState()
    {
        isNewHighScore = false;
        score = 0;
    }

    #region Getter Methods

    public static int GetHighScore() => highScore;
    public static int GetScore() => score;
    public static bool IsNewHighScore() => isNewHighScore;

    #endregion
}
