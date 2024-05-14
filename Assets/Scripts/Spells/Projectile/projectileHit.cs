using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Serialization;

public class projectileHit : MonoBehaviour
{
    [SerializeField] private float BaseSurviveTime;
    [SerializeField] private GameObject spawnOnHit;
    private Explosion biem;

    private void Start()
    {
        if (BaseSurviveTime == 0)
            return;

        biem = FindObjectOfType<Explosion>();
        Invoke("EndProjectile", BaseSurviveTime);
    }

    private void EndProjectile()
    {
        if (!gameObject.activeSelf) 
            return;
        
        Instantiate(spawnOnHit, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        EndProjectile();
    }
}