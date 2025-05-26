using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuUI : MonoBehaviour
{
    private const string HIGH_SCORE_TEXT = "HighScoreText";
    private const string MUSIC_TOGGLE = "MusicToggle";

    private TextMeshProUGUI highScoreText;
    private Toggle musicToggle;

    private GameManager gameManager;
    private AudioPlayer audioPlayer;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();

        highScoreText = GameObject.Find(HIGH_SCORE_TEXT).GetComponent<TextMeshProUGUI>();
        musicToggle = GameObject.Find(MUSIC_TOGGLE).GetComponent<Toggle>();
    }

    private void Start()
    {
        highScoreText.text = $"High Score: {Utils.FormatNumber(GameState.GetHighScore())}";
        musicToggle.isOn = Settings.IsMusicOn();
    }

    public void OnStartButtonClicked()
    {
        audioPlayer.PlayClickHit();
        gameManager.LoadGameScene();
    }

    public void OnMusicToggleChanged()
    {
        audioPlayer.PlayClickHit();
        Settings.SetIsMusicOn(musicToggle.isOn);
    }
}
