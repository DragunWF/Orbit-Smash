using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPowerup : MonoBehaviour
{
    private const int BASE_SCORE_GAIN = 150;

    [SerializeField] float speed = 3f;

    private AudioPlayer audioPlayer;
    private GameState gameState;
    private ParticlePlayer particlePlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        gameState = FindObjectOfType<GameState>();
        particlePlayer = FindObjectOfType<ParticlePlayer>();
    }

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        audioPlayer.PlayClickHit();
        particlePlayer.PlayStarExplosion(transform.position);
        gameState.UpdateScore(BASE_SCORE_GAIN);
        Destroy(gameObject);
    }
}
