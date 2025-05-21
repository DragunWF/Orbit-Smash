using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlanetSpawner : MonoBehaviour
{
    private const string SPAWN_POINT_NAME = "SpawnPoint";

    private GameObject planet;
    private List<GameObject> spawnPoints;

    private void Awake()
    {
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
        float spawnDelay = 3.5f;
        yield return new WaitForSeconds(spawnDelay);

        int spawnCount = 0;
        while (true)
        {
            yield return new WaitForSeconds(1);
            Instantiate(planet, ChooseRandomPoint());
            spawnCount++;
        }
    }

    private Transform ChooseRandomPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)].transform;
    }
}
