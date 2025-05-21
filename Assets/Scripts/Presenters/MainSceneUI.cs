using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public sealed class MainSceneUI : MonoBehaviour
{
    private const string SCORE_TEXT = "ScoreText";

    private TextMeshProUGUI scoreText;

    public void SetScoreText(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    private void Awake()
    {
        scoreText = GameObject.Find(SCORE_TEXT).GetComponent<TextMeshProUGUI>();
    }
}
