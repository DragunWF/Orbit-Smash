using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameState : MonoBehaviour
{
    private const int ADDITIONAL_SCORE_PER_DIFFICULTY_LEVEL = 5;

    private int score = 0;
    private MainSceneUI mainSceneUi;
    private DifficultyScaler difficultyScaler;

    private void Awake()
    {
        mainSceneUi = GetComponent<MainSceneUI>();
        difficultyScaler = GetComponent<DifficultyScaler>();
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd + (ADDITIONAL_SCORE_PER_DIFFICULTY_LEVEL * difficultyScaler.GetDifficultyLevel());
        Debug.Log(score);
        mainSceneUi.SetScoreText(score);
    }

    public void EndGame()
    {
        // TODO: Implement fade effect and high scores as well as scene transition to game over scene
    }
}
