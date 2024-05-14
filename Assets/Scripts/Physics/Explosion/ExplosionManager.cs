using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ExplosionManager : MonoBehaviour
{
    [FormerlySerializedAs("exsplosionPrefab")] [SerializeField] private GameObject explosionPrefab;
    public ExplosionStruct ConvertProjectileStatsToExplosionStruct(ProjectileStats projectileStats)
    {
        ExplosionStruct explosionStruct = new ExplosionStruct();

        explosionStruct.power = projectileStats.power;
        explosionStruct.radius = projectileStats.radius;
        explosionStruct.upwardsPower = projectileStats.upwardsPower;

        return explosionStruct;
    }

    public void CreateExplosion(Vector3 spawnpoint, ExplosionStruct newExplosion)
    {
        GameObject currentexplosion = Instantiate(explosionPrefab, spawnpoint, transform.rotation);
        currentexplosion.GetComponent<Explosion>().fireball = newExplosion;
    }
}