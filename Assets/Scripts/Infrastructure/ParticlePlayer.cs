using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ParticlePlayer : MonoBehaviour
{
    private const string EXPLOSION = "Explosion";

    private readonly Dictionary<string, GameObject> particles = new();
    private const float destroyDelay = 7.5f;

    #region Particle Methods

    public void PlayExplosion(Vector2 position)
    {
        SpawnParticle(EXPLOSION, position);
    }

    #endregion

    private void Awake()
    {
        particles.Add(EXPLOSION, Resources.Load("Prefabs/Explosion Effect") as GameObject);
    }

    private void SpawnParticle(string effectName, Vector2 position)
    {
        GameObject particle = Instantiate(particles[effectName], position,
                                          Quaternion.identity);
        Destroy(particle, destroyDelay);
    }
}
