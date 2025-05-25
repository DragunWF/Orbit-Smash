using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScaler : MonoBehaviour
{
    [Header("Difficulty Settings")]
    [SerializeField] private readonly float difficultyInterval = 4f; // Faster progression
    [SerializeField] private readonly int insaneDifficultyStart = 10; // When it gets truly crazy

    [Header("Speed Settings")]
    [SerializeField] private float minPlanetSpeed = 0.5f;
    [SerializeField] private float maxPlanetSpeed = 1f;
    [SerializeField] private readonly float speedMultiplier = 1.5f; // Exponential speed growth

    [Header("Spawn Settings")]
    [SerializeField] private readonly float maxSpawnDelay = 3.0f;
    [SerializeField] private readonly float minSpawnDelay = 0.1f; // Insanely fast spawning
    [SerializeField] private readonly float spawnReductionRate = 0.3f; // Aggressive spawn reduction

    private int difficultyLevel = 0;
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

        if (difficultyLevel <= 5)
        {
            // Early game: reasonable increases
            minPlanetSpeed += 0.3f;
            maxPlanetSpeed += 0.4f;
        }
        else if (difficultyLevel <= insaneDifficultyStart)
        {
            // Mid game: aggressive increases
            minPlanetSpeed += 0.5f;
            maxPlanetSpeed += 0.7f;
        }
        else
        {
            // Late game: INSANE exponential growth
            minPlanetSpeed *= speedMultiplier;
            maxPlanetSpeed *= speedMultiplier;

            // Bonus: Add random chaos at high levels
            if (difficultyLevel > 15)
            {
                minPlanetSpeed += UnityEngine.Random.Range(0.5f, 2f);
                maxPlanetSpeed += UnityEngine.Random.Range(1f, 3f);
            }
        }

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
        float delay;

        if (difficultyLevel <= 5)
        {
            // Early: gentle reduction
            delay = maxSpawnDelay - (difficultyLevel * 0.25f);
        }
        else if (difficultyLevel <= insaneDifficultyStart)
        {
            // Mid: aggressive reduction
            delay = 2f - (difficultyLevel * spawnReductionRate);
        }
        else
        {
            // Late: exponentially faster spawning
            float exponent = (difficultyLevel - insaneDifficultyStart) * 0.5f;
            delay = 1f / Mathf.Pow(2f, exponent);

            // Ultra-late game: multiple planets per frame territory
            if (difficultyLevel > 20)
            {
                delay = minSpawnDelay / (difficultyLevel - 15);
            }
        }

        return Mathf.Clamp(delay, minSpawnDelay, maxSpawnDelay);
    }

    // Bonus methods for extreme difficulty
    public bool ShouldSpawnMultiplePlanets()
    {
        return difficultyLevel > 12 && UnityEngine.Random.Range(0f, 1f) < 0.3f;
    }

    public int GetMultiSpawnCount()
    {
        if (difficultyLevel > 18)
            return UnityEngine.Random.Range(2, 5); // 2-4 planets at once
        else if (difficultyLevel > 15)
            return UnityEngine.Random.Range(2, 3); // 2-3 planets at once
        else
            return 2; // Just 2 planets
    }

    public bool IsImpossibleTerritory()
    {
        return difficultyLevel > 15;
    }

    public float GetDifficultyProgress()
    {
        // No cap - this goes to infinity!
        return difficultyLevel / 10f;
    }
}