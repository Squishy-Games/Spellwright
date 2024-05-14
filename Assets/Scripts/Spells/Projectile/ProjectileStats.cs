using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public struct ProjectileStats
{
    [Header("Projectile")] public Vector3 BaseSpeed;
    [SerializeField] private float size;
    public float Size
    {
        get { return size == 0 ? 1 : size; }
        set { size = value; }
    }
    public bool useGravity;

    [Header("Explosion")] public float power;
    public float radius;
    public float upwardsPower;
}