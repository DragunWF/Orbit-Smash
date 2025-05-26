using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScaler : MonoBehaviour
{
    [Header("Difficulty Settings")]
    [SerializeField] private readonly float difficultyInterval = 7f; // Slightly longer progression window
    [SerializeField] private readonly int insaneDifficultyStart = 15; // Delayed insanity phase

    [Header("Speed Settings")]
    [SerializeField] private float minPlanetSpeed = 0.5f;
    [SerializeField] private float maxPlanetSpeed = 1f;
    [SerializeField] private readonly float speedMultiplier = 1.2f; // Less aggressive exponential growth

    [Header("Spawn Settings")]
    [SerializeField] private readonly float maxSpawnDelay = 3.5f;
    [SerializeField] private readonly float minSpawnDelay = 0.85f; // Less severe minimum
    [SerializeField] private readonly float spawnReductionRate = 0.1f; // Gentler reduction per level

    private int difficultyLevel = 0;
    private float timer = 0f;
    private float secondsSurvived = 0f;

    private MainSceneUI mainSceneUI;

    private void Awake()
    {
        mainSceneUI = FindObjectOfType<MainSceneUI>();
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
            minPlanetSpeed += 0.2f;
            maxPlanetSpeed += 0.3f;
        }
        else if (difficultyLevel <= insaneDifficultyStart)
        {
            minPlanetSpeed += 0.3f;
            maxPlanetSpeed += 0.4f;
        }
        else
        {
            minPlanetSpeed *= speedMultiplier;
            maxPlanetSpeed *= speedMultiplier;

            if (difficultyLevel > 18)
            {
                minPlanetSpeed += UnityEngine.Random.Range(0.2f, 1f);
                maxPlanetSpeed += UnityEngine.Random.Range(0.5f, 1.5f);
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
            delay = maxSpawnDelay - (difficultyLevel * 0.15f);
        }
        else if (difficultyLevel <= insaneDifficultyStart)
        {
            delay = 2.25f - (difficultyLevel * spawnReductionRate);
        }
        else
        {
            float exponent = (difficultyLevel - insaneDifficultyStart) * 0.4f;
            delay = 1f / Mathf.Pow(1.8f, exponent);

            if (difficultyLevel > 22)
            {
                delay = minSpawnDelay / (difficultyLevel - 18);
            }
        }

        return Mathf.Clamp(delay, minSpawnDelay, maxSpawnDelay);
    }

    public bool ShouldSpawnMultiplePlanets()
    {
        return difficultyLevel > 10 && UnityEngine.Random.Range(0f, 1f) < 0.25f;
    }

    public int GetMultiSpawnCount()
    {
        if (difficultyLevel > 20)
        {
            return UnityEngine.Random.Range(2, 3);
        }
        else if (difficultyLevel > 16)
        {
            return UnityEngine.Random.Range(1, 2);
        }

        return 1;
    }

    public bool IsImpossibleTerritory()
    {
        return difficultyLevel > 20;
    }

    public float GetDifficultyProgress()
    {
        return difficultyLevel / 10f;
    }
}
