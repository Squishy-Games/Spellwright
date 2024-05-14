using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseProjectile : MonoBehaviour
{
    public ProjectileStats BaseProjectileStats;
    private Rigidbody _rigidbody;
    private SphereCollider _collider;

    void Start()
    {
        _collider = GetComponent<SphereCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        FindObjectOfType<ProjectilespellManager>().projectilespells.Add(gameObject, BaseProjectileStats);
        
        applystats();
        LookForWarnings();
    }

    void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + BaseProjectileStats.BaseSpeed * Time.fixedDeltaTime);
    }


    private void LookForWarnings()
    {
        if (_collider != null)
            if (_collider.radius < BaseProjectileStats.BaseSpeed.magnitude * Time.fixedDeltaTime / 2)
                Debug.LogWarning("speed of " + gameObject.name +
                                 " is higher then the collider radius. meaning it can move through colliders",
                    gameObject.transform.GetComponent<BaseProjectile>());
    }

    private void applystats()
    {
        _rigidbody.useGravity = BaseProjectileStats.useGravity;
        transform.localScale =
            new Vector3(BaseProjectileStats.Size, BaseProjectileStats.Size, BaseProjectileStats.Size);
    }
}