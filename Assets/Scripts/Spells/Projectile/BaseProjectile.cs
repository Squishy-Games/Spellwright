using System;
using Unity.VisualScripting;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    public Vector3 m_Speed;

    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        m_Rigidbody.MovePosition(transform.position + m_Speed * Time.deltaTime);
    }
}