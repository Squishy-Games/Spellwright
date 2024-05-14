using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Serialization;

public class ProjectileHit : MonoBehaviour
{
    private float BaseSurviveTime;
    private ExplosionManager _explosionManager;

    private void Start()
    {
        _explosionManager = FindObjectOfType<ExplosionManager>();
        if (BaseSurviveTime == 0)
            return;
        Invoke("EndProjectile", BaseSurviveTime);
    }

    private void EndProjectile()
    {
        if (!gameObject.activeSelf)
            return;
        _explosionManager.CreateExplosion(transform.position,
            FindObjectOfType<ProjectilespellManager>().projectilespells[gameObject].ExplosionStruct);

        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        EndProjectile();
    }
}