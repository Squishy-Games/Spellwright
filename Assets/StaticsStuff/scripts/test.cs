using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class test : MonoBehaviour
{
    [SerializeField] private int radius;
    
    public void RemoveOverlap()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1,LayerMask.NameToLayer("Greens"));
        Debug.Log(hitColliders.Length);
        foreach (Collider hitCollider in hitColliders)
        {
            Debug.Log(hitCollider.name);
            Destroy(hitCollider.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
