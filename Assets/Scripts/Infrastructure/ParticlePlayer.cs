using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ParticlePlayer : MonoBehaviour
{
    private const string EXPLOSION_ONE = "Explosion1";
    private const string EXPLOSION_TWO = "Explosion2";
    private const string EXPLOSION_THREE = "Explosion3";
    private const string SPECIAL_EXPLOSION = "SpecialExplosion";

    private readonly Dictionary<string, GameObject> particles = new();
    private const float destroyDelay = 7.5f;

    #region Particle Methods

    public void PlayExplosion(Vector2 position)
    {
        string[] explosionEffectNames = { EXPLOSION_ONE, EXPLOSION_TWO, EXPLOSION_THREE };
        SpawnParticle(explosionEffectNames[Random.Range(0, explosionEffectNames.Length)], position);
    }

    public void PlayStarExplosion(Vector2 position)
    {
        SpawnParticle(SPECIAL_EXPLOSION, position);
    }

    #endregion

    private void Awake()
    {
        particles.Add(EXPLOSION_ONE, Resources.Load<GameObject>("Prefabs/ExplosionEffect1"));
        particles.Add(EXPLOSION_TWO, Resources.Load<GameObject>("Prefabs/ExplosionEffect2"));
        particles.Add(EXPLOSION_THREE, Resources.Load<GameObject>("Prefabs/ExplosionEffect3"));
        particles.Add(SPECIAL_EXPLOSION, Resources.Load<GameObject>("Prefabs/SpecialExplosionEffect"));
    }

    private void SpawnParticle(string effectName, Vector2 position)
    {
        GameObject particle = Instantiate(particles[effectName], position,
                                          Quaternion.identity);
        Destroy(particle, destroyDelay);
    }
}
