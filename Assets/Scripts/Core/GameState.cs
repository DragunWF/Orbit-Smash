using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameState : MonoBehaviour
{
    private int score = 0;
    private MainSceneUI mainSceneUi;

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        Debug.Log(score);
        mainSceneUi.SetScoreText(score);
    }

    private void Awake()
    {
        mainSceneUi = GetComponent<MainSceneUI>();
    }
}
