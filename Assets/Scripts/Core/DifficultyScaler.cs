using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScaler : MonoBehaviour
{
    private int difficultyLevel = 0;

    private float minPlanetSpeed = 0.5f;
    private float maxPlanetSpeed = 1f;

    [SerializeField] private float difficultyInterval = 10f; // seconds between difficulty increases
    private float secondsSurvived = 0f;

    private MainSceneUI mainSceneUI;
    private GameState gameState;

    private void Start()
    {
        GameObject scriptsGameObj = GameObject.Find(Constants.SCRIPTS_GAME_OBJECT);

        mainSceneUI = scriptsGameObj.GetComponent<MainSceneUI>();
        gameState = scriptsGameObj.GetComponent<GameState>();
    }

    private void Update()
    {
        secondsSurvived += Time.deltaTime;

        if (secondsSurvived >= difficultyInterval)
        {
            IncreaseDifficulty();
            secondsSurvived = 0f;
        }

        mainSceneUI.SetTimeSurvivedText((int)Math.Floor(secondsSurvived));
    }

    private void IncreaseDifficulty()
    {
        difficultyLevel++;

        minPlanetSpeed += 0.2f;
        maxPlanetSpeed += 0.3f;

        mainSceneUI.SetDifficultyText(difficultyLevel);
    }

    public float GetRandomPlanetSpeed()
    {
        return UnityEngine.Random.Range(minPlanetSpeed, maxPlanetSpeed);
    }

    public int GetDifficultyLevel()
    {
        return difficultyLevel;
    }
}
