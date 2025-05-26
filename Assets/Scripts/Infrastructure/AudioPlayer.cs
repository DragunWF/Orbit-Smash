using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip clickHitSoundClip;
    [SerializeField] AudioClip clickMissSoundClip;
    [SerializeField] AudioClip damageSoundClip;

    private const float defaultBackgroundMusicVolume = 0.085f;
    private AudioSource backgroundMusicSource;

    #region Play Clip Methods

    public void PlayClickHit() => PlayClip(clickHitSoundClip, 5f);
    public void PlayClickMiss() => PlayClip(clickMissSoundClip, 6f);
    public void PlayDamage() => PlayClip(damageSoundClip, 0.85f);

    #endregion

    private void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector2 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }

    private void Awake()
    {
        backgroundMusicSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.volume = Settings.IsMusicOn() ? defaultBackgroundMusicVolume : 0;
        }
    }
}
