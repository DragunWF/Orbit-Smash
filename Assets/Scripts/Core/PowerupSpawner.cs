using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    private const int minSpawnTime = 30;
    private const int maxSpawnTime = 60;

    private int currentSpawnTime;
    private List<GameObject> spawnPoints;
    private GameObject[] powerups;

    private void Awake()
    {
        spawnPoints = Utils.GetSpawnPoints();
        powerups = new GameObject[] {
            Resources.Load<GameObject>("Prefabs/StarPowerup")
        };
        // TODO: Implement Resources.Load<GameObject>("Prefabs/GalaxyPowerup")
    }

    private void Start()
    {
        SetCurrentSpawnTime();
        StartCoroutine(SpawnPowerups());
    }

    private void SetCurrentSpawnTime()
    {
        currentSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    private IEnumerator SpawnPowerups()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentSpawnTime);
            SetCurrentSpawnTime();
            Instantiate(ChooseRandomPowerup(), ChooseRandomPoint());
        }
    }

    private GameObject ChooseRandomPowerup()
    {
        return powerups[Random.Range(0, powerups.Length)];
    }

    private Transform ChooseRandomPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)].transform;
    }
}
