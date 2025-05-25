using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartMenuUI : MonoBehaviour
{
    private const string HIGH_SCORE_TEXT = "HighScoreText";

    private TextMeshProUGUI highScoreText;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        highScoreText = GameObject.Find(HIGH_SCORE_TEXT).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        highScoreText.text = $"High Score: {Utils.FormatNumber(GameState.GetHighScore())}";
    }

    public void OnStartButtonClicked()
    {
        gameManager.LoadGameScene();
    }
}
