using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlanetSpawner : MonoBehaviour
{
    private GameObject planet;
    private List<GameObject> spawnPoints;

    private DifficultyScaler difficultyScaler;

    private void Awake()
    {
        difficultyScaler = GetComponent<DifficultyScaler>();

        planet = Resources.Load("Prefabs/Planet") as GameObject;
        spawnPoints = Utils.GetSpawnPoints();
    }

    private void Start()
    {
        StartCoroutine(SpawnProjectiles());
    }

    private IEnumerator SpawnProjectiles()
    {
        // Initial delay before the first planet
        yield return new WaitForSeconds(3.5f);

        while (true)
        {
            // Check if we should spawn multiple planets (chaos mode)
            if (difficultyScaler.ShouldSpawnMultiplePlanets())
            {
                SpawnMultiplePlanets();
            }
            else
            {
                // Normal single planet spawn
                Instantiate(planet, ChooseRandomPoint());
            }

            float currentDelay = difficultyScaler.GetCurrentSpawnDelay();

            // In impossible territory, add some random chaos to timing
            if (difficultyScaler.IsImpossibleTerritory())
            {
                float chaosModifier = Random.Range(0.5f, 1.5f);
                currentDelay *= chaosModifier;
            }

            yield return new WaitForSeconds(currentDelay);
        }
    }

    private void SpawnMultiplePlanets()
    {
        int spawnCount = difficultyScaler.GetMultiSpawnCount();

        for (int i = 0; i < spawnCount; i++)
        {
            // Spawn planets at different positions
            Transform spawnPoint = ChooseRandomPoint();

            // Add slight random offset to prevent overlap
            Vector3 offset = new Vector3(
                Random.Range(-0.5f, 0.5f),
                Random.Range(-0.2f, 0.2f),
                0
            );

            Vector3 spawnPos = spawnPoint.position + offset;
            Instantiate(planet, spawnPos, spawnPoint.rotation);
        }
    }

    private Transform ChooseRandomPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)].transform;
    }
}