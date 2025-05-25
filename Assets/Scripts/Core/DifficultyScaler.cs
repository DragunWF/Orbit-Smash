using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScaler : MonoBehaviour
{
    [SerializeField] private readonly float difficultyInterval = 6f; // seconds between difficulty increases

    private int difficultyLevel = 0;

    private float minPlanetSpeed = 0.5f;
    private float maxPlanetSpeed = 1f;

    private float timer = 0f;
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
        timer += Time.deltaTime;
        secondsSurvived += Time.deltaTime;

        if (timer >= difficultyInterval)
        {
            IncreaseDifficulty();
            timer = 0f;
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

    public float GetCurrentSpawnDelay()
    {
        const float MAX_SPAWN_DELAY = 3.0f;
        const float MIN_SPAWN_DELAY = 0.35f;

        // Base delay is 3.5s, decreases by 0.15s per difficulty level
        float delay = MAX_SPAWN_DELAY - (difficultyLevel * 0.2f);
        return Mathf.Clamp(delay, MIN_SPAWN_DELAY, MAX_SPAWN_DELAY); // prevents it from going too fast
    }
}
