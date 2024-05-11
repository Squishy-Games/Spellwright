using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class projectileHit : MonoBehaviour
{
    [SerializeField] private GameObject spawnOnHit;

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("test");
        Instantiate(spawnOnHit, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}