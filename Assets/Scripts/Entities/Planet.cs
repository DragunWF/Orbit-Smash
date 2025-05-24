using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Planet : MonoBehaviour
{
    private const string PLANET_DESPAWN_TAG = "PlanetDespawnArea";
    private const int BASE_SCORE_GAIN = 5;

    [SerializeField] float speed = 1f;

    private GameState gameState;

    private void Start()
    {
        gameState = GameObject.Find(Constants.SCRIPTS_GAME_OBJECT).GetComponent<GameState>();
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
