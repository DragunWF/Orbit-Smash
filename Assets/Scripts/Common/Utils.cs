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
}
