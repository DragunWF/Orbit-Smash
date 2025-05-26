using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        // Find components with null checks
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogWarning("GameManager not found in scene!");
        }

        audioPlayer = FindObjectOfType<AudioPlayer>();
        if (audioPlayer == null)
        {
            Debug.LogWarning("AudioPlayer not found in scene!");
        }

        // Find UI elements with null checks
        GameObject highScoreObj = GameObject.Find(HIGH_SCORE_TEXT);
        if (highScoreObj != null)
        {
            highScoreText = highScoreObj.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError($"GameObject with name '{HIGH_SCORE_TEXT}' not found!");
        }

        GameObject musicToggleObj = GameObject.Find(MUSIC_TOGGLE);
        if (musicToggleObj != null)
        {
            musicToggle = musicToggleObj.GetComponent<Toggle>();
        }
        else
        {
            Debug.LogError($"GameObject with name '{MUSIC_TOGGLE}' not found!");
        }
    }

    private void Start()
    {
        if (highScoreText != null)
        {
            highScoreText.text = $"High Score: {Utils.FormatNumber(GameState.GetHighScore())}";
        }

        if (musicToggle != null)
        {
            musicToggle.isOn = Settings.IsMusicOn();
        }
    }

    public void OnStartButtonClicked()
    {
        Debug.Log("Start button clicked!"); // Debug log to verify button click

        // Play audio if available
        if (audioPlayer != null)
        {
            audioPlayer.PlayClickHit();
        }

        // Load scene - with fallback if GameManager is missing
        if (gameManager != null)
        {
            gameManager.LoadGameScene();
        }
        else
        {
            // Fallback: Load scene directly
            Debug.LogWarning("GameManager is null, loading scene directly");
            LoadGameSceneFallback();
        }
    }

    private void LoadGameSceneFallback()
    {
        // Replace "GameScene" with your actual game scene name
        string gameSceneName = "GameScene"; // Adjust this to your scene name

        try
        {
            SceneManager.LoadScene(gameSceneName);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load scene '{gameSceneName}': {e.Message}");

            // Try loading by build index as last resort
            try
            {
                SceneManager.LoadScene(1); // Assuming game scene is at index 1
            }
            catch (System.Exception e2)
            {
                Debug.LogError($"Failed to load scene by index: {e2.Message}");
            }
        }
    }

    public void OnMusicToggleChanged()
    {
        if (audioPlayer != null)
        {
            audioPlayer.PlayClickHit();
        }

        if (musicToggle != null)
        {
            Settings.SetIsMusicOn(musicToggle.isOn);
        }
    }
}