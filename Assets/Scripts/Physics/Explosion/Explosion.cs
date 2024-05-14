using UnityEngine;
using System.Collections;
using UnityEditor;

// Applies an explosion force to all nearby rigidbodies
public class Explosion : MonoBehaviour
{
    private float correctedPower;
    private float explosioncorrectie = 100;

    public ExplosionStruct fireball;
    
    void Start()
    {
        correctedPower = fireball.power * explosioncorrectie;
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, fireball.radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(correctedPower, explosionPos, fireball.radius, fireball.upwardsPower);
        }
        Destroy(gameObject);
    }
}