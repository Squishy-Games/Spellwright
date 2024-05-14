using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ExplosionManager : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;

    public void CreateExplosion(Vector3 spawnpoint, ExplosionStruct newExplosion)
    {
        GameObject currentexplosion = Instantiate(explosionPrefab, spawnpoint, transform.rotation);
        currentexplosion.GetComponent<Explosion>().fireball = newExplosion;
    }
}