using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseProjectile : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private SphereCollider _collider;
    [FormerlySerializedAs("m_Speed")] public Vector3 BaseSpeed;

    void Start()
    {
        _collider = GetComponent<SphereCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        LookForWarnings();
    }

    void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + BaseSpeed * Time.fixedDeltaTime);
    }


    void LookForWarnings()
    {
        if (_collider != null)
            if (_collider.radius < BaseSpeed.magnitude * Time.fixedDeltaTime / 2)
                Debug.LogWarning("speed of " + gameObject.name +
                                 " is higher then the collider radius. meaning it can move through colliders",
                    gameObject.transform.GetComponent<BaseProjectile>());
    }
}