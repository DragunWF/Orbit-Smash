using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Utils : MonoBehaviour
{
    public static string FormatNumber(int number)
    {
        if (number < 1000)
        {
            return number.ToString();
        }
        return number.ToString("N0");
    }

    public static List<GameObject> GetSpawnPoints()
    {
        List<GameObject> output = new();

        int count = 1;
        while (true)
        {
            GameObject spawnPoint = GameObject.Find($"{Constants.SPAWN_POINT_NAME} ({count})");
            count++;

            if (spawnPoint == null)
            {
                break;
            }

            output.Add(spawnPoint);
        }

        return output;
    }
}
