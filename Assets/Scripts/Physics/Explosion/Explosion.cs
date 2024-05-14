using UnityEngine;
using System.Collections;
using UnityEditor;

// Applies an explosion force to all nearby rigidbodies
public class Explosion : MonoBehaviour
{
    private float _correctedPower;
    private float _explosioncorrectie = 100;

    public ExplosionStruct fireball;
    
    void Start()
    {
        _correctedPower = fireball.power * _explosioncorrectie;
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, fireball.radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(_correctedPower, explosionPos, fireball.radius, fireball.upwardsPower);
        }
        Destroy(gameObject);
    }
}