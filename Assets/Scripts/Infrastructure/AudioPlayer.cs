using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private Dictionary<string, AudioClip> clips = new();
    private Dictionary<string, float> volumes = new();

    private void Awake()
    {

    }

    public void PlayClickHit()
    {

    }

    public void PlayClickMiss()
    {

    }

    private void PlayClip(string fileName)
    {
        if (clips[fileName] != null)
        {
            Vector2 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clips[fileName], cameraPos, volumes[fileName]);
        }
    }
}
