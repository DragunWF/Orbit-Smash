using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private const int MAX_HEALTH = 3;

    private int currentHealth = MAX_HEALTH;

    private MainSceneUI mainSceneUI;
    private GameState gameState;
    private CameraShake cameraShake;

    private void Awake()
    {
        mainSceneUI = FindObjectOfType<MainSceneUI>();
        gameState = FindObjectOfType<GameState>();
        cameraShake = FindObjectOfType<CameraShake>();
    }

    public void DamageHealth()
    {
        currentHealth--;
        mainSceneUI.SetHeartCount(currentHealth);
        cameraShake.TriggerShake();

        if (currentHealth <= 0)
        {
            gameState.EndGame();
            return;
        }
    }

    public int GetMaxHealth() => MAX_HEALTH;
}
