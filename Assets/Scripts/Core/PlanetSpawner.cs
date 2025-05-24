using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlanetSpawner : MonoBehaviour
{
    private const string SPAWN_POINT_NAME = "SpawnPoint";

    private GameObject planet;
    private List<GameObject> spawnPoints;

    private DifficultyScaler difficultyScaler;

    private void Awake()
    {
        difficultyScaler = GetComponent<DifficultyScaler>();

        planet = Resources.Load("Prefabs/Planet") as GameObject;
        spawnPoints = GetSpawnPoints();
    }

    private void Start()
    {
        StartCoroutine(SpawnProjectiles());
    }

    private void Update()
    {

    }

    private List<GameObject> GetSpawnPoints()
    {
        List<GameObject> output = new();

        int count = 1;
        while (true)
        {
            GameObject spawnPoint = GameObject.Find($"{SPAWN_POINT_NAME} ({count})");
            count++;

            if (spawnPoint == null)
            {
                break;
            }

            output.Add(spawnPoint);
        }

        return output;
    }

    private IEnumerator SpawnProjectiles()
    {
        // Initial delay before the first planet
        yield return new WaitForSeconds(3.5f);

        while (true)
        {
            Instantiate(planet, ChooseRandomPoint());
            float currentDelay = difficultyScaler.GetCurrentSpawnDelay();
            yield return new WaitForSeconds(currentDelay);
        }
    }


    private Transform ChooseRandomPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)].transform;
    }
}
