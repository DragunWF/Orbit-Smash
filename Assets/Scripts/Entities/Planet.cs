using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Planet : MonoBehaviour
{
    private const string PLANET_DESPAWN_TAG = "PlanetDespawnArea";
    private const int BASE_SCORE_GAIN = 5;

    private const float BASE_DIFFICULTY_INCREASE_IN_SPEED = 0.25f;
    private const float BASE_MIN_SPEED = 0.5f;
    private const float BASE_MAX_SPEED = 3f;

    [SerializeField] float speed = 1f;

    private GameState gameState;
    private DifficultyScaler difficultyScaler;

    private void Awake()
    {
        difficultyScaler = GameObject.Find(Constants.SCRIPTS_GAME_OBJECT).GetComponent<DifficultyScaler>();
    }

    private void Start()
    {
        gameState = GameObject.Find(Constants.SCRIPTS_GAME_OBJECT).GetComponent<GameState>();

        float speedIncrease = difficultyScaler.GetDifficultyLevel() * BASE_DIFFICULTY_INCREASE_IN_SPEED;
        speed = Random.Range(BASE_MIN_SPEED + speedIncrease, BASE_MAX_SPEED + speedIncrease);
    }

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        gameState.UpdateScore(BASE_SCORE_GAIN);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(PLANET_DESPAWN_TAG))
        {
            Destroy(gameObject);
        }
    }
}
