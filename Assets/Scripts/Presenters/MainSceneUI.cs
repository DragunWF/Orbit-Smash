using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class MainSceneUI : MonoBehaviour
{
    [SerializeField] Sprite fullHeartSprite, emptyHeartSprite;

    private const string SCORE_TEXT = "ScoreText";
    private const string DIFFICULTY_TEXT = "DifficultyText";
    private const string TIME_SURVIVED_TEXT = "TimeSurvivedText";
    private const string HEART_IMAGE = "Heart"; // With a number appended: "Heart1", "Heart2", etc.

    private TextMeshProUGUI scoreText, difficultyText, timeSurvivedText;
    private Image heartImage1, heartImage2, heartImage3;

    private HealthSystem healthSystem;

    private void Awake()
    {
        scoreText = GameObject.Find(SCORE_TEXT).GetComponent<TextMeshProUGUI>();
        difficultyText = GameObject.Find(DIFFICULTY_TEXT).GetComponent<TextMeshProUGUI>();
        timeSurvivedText = GameObject.Find(TIME_SURVIVED_TEXT).GetComponent<TextMeshProUGUI>();

        heartImage1 = GameObject.Find($"{HEART_IMAGE}1").GetComponent<Image>();
        heartImage2 = GameObject.Find($"{HEART_IMAGE}2").GetComponent<Image>();
        heartImage3 = GameObject.Find($"{HEART_IMAGE}3").GetComponent<Image>();

        healthSystem = FindObjectOfType<HealthSystem>();
    }

    #region Setter Methods

    public void SetScoreText(int score)
    {
        scoreText.text = $"Score: {Utils.FormatNumber(score)}";
    }

    public void SetDifficultyText(int difficulty)
    {
        difficultyText.text = $"Difficulty Level: {difficulty}";
    }

    public void SetTimeSurvivedText(int seconds)
    {
        timeSurvivedText.text = $"Time Survived in Seconds: {Utils.FormatNumber(seconds)}";
    }

    public void SetHeartCount(int heartCount)
    {
        if (heartCount < 0 || heartCount > healthSystem.GetMaxHealth())
        {
            return;
        }

        Image[] heartImages = { heartImage1, heartImage2, heartImage3 };
        for (int i = 0, nth = 1; i < heartImages.Length; i++, nth++)
        {
            heartImages[i].sprite = nth <= heartCount ? fullHeartSprite : emptyHeartSprite;
        }
    }

    #endregion
}
