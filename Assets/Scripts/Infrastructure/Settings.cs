using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    private static bool isMusicOn = true;

    public static void SetIsMusicOn(bool value) => isMusicOn = value;
    public static bool IsMusicOn() => isMusicOn;
}
