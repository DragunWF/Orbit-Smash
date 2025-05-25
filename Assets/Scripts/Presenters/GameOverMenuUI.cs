using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverMenuUI : MonoBehaviour
{
    private const string HIGH_SCORE_TEXT = "HighScoreText";
    private const string SCORE_TEXT = "ScoreText";
    private const string NEW_HIGH_SCORE_TEXT = "NewHighScoreText";

    private TextMeshProUGUI highScoreText;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI newHighScoreText;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        highScoreText = GameObject.Find(HIGH_SCORE_TEXT).GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find(SCORE_TEXT).GetComponent<TextMeshProUGUI>();
        newHighScoreText = GameObject.Find(NEW_HIGH_SCORE_TEXT).GetComponent<TextMeshProUGUI>();

        newHighScoreText.gameObject.SetActive(false);
    }

    private void Start()
    {
        highScoreText.text = $"High Score: {Utils.FormatNumber(GameState.GetHighScore())}";
        scoreText.text = $"Score: {Utils.FormatNumber(GameState.GetScore())}";
        newHighScoreText.gameObject.SetActive(GameState.IsNewHighScore());

        GameState.ResetGameState();
    }

    public void OnRetryButtonClicked()
    {
        gameManager.LoadGameScene();
    }

    public void OnStartMenuButtonClicked()
    {
        gameManager.LoadStartMenu();
    }
}
