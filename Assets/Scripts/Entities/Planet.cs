using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Planet : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    private const string PLANET_DESPAWN_TAG = "PlanetDespawnArea";
    private const int BASE_SCORE_GAIN = 5;

    private const float BASE_DIFFICULTY_INCREASE_IN_SPEED = 0.1f;
    private const float BASE_MIN_SPEED = 0.5f;
    private const float BASE_MAX_SPEED = 3f;

    private GameState gameState;
    private DifficultyScaler difficultyScaler;
    private ParticlePlayer particlePlayer;
    private HealthSystem healthSystem;
    private AudioPlayer audioPlayer;

    private Animator planetAnimator;
    private RuntimeAnimatorController[] planetAnimatorControllers;

    private void Awake()
    {
        planetAnimator = GetComponent<Animator>();

        difficultyScaler = FindObjectOfType<DifficultyScaler>();
        particlePlayer = FindObjectOfType<ParticlePlayer>();
        gameState = FindObjectOfType<GameState>();
        healthSystem = FindObjectOfType<HealthSystem>();
        audioPlayer = FindObjectOfType<AudioPlayer>();

        RandomizePlanetSprite();
        RandomizePlanetSpeed();
    }

    private void RandomizePlanetSprite()
    {
        planetAnimatorControllers = new RuntimeAnimatorController[] {
            Resources.Load<RuntimeAnimatorController>("Animations/Barren Animator"),
            Resources.Load<RuntimeAnimatorController>("Animations/Gas Giant Yellow Animator"),
            Resources.Load<RuntimeAnimatorController>("Animations/Ice World Animator"),
            Resources.Load<RuntimeAnimatorController>("Animations/Islands Animator"),
            Resources.Load<RuntimeAnimatorController>("Animations/Lava World Animator"),
            Resources.Load<RuntimeAnimatorController>("Animations/Terran Dry Animator"),
            Resources.Load<RuntimeAnimatorController>("Animations/Terran Wet Animator")
        };

        planetAnimator.runtimeAnimatorController = planetAnimatorControllers[Random.Range(0, planetAnimatorControllers.Length)];
    }

    private void RandomizePlanetSpeed()
    {
        float speedIncrease = difficultyScaler.GetDifficultyLevel() * BASE_DIFFICULTY_INCREASE_IN_SPEED;
        speed = Random.Range(BASE_MIN_SPEED + speedIncrease, BASE_MAX_SPEED + speedIncrease);
    }

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        audioPlayer.PlayClickHit();
        particlePlayer.PlayExplosion(transform.position);
        gameState.UpdateScore(BASE_SCORE_GAIN);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(PLANET_DESPAWN_TAG))
        {
            OnPlanetBypass();
        }
    }

    private void OnPlanetBypass()
    {
        audioPlayer.PlayDamage();
        healthSystem.DamageHealth();
        Destroy(gameObject);
    }
}
