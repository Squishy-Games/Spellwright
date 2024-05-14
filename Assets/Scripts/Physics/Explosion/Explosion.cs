using UnityEngine;
using System.Collections;
using UnityEditor;

// Applies an explosion force to all nearby rigidbodies
public class Explosion : MonoBehaviour
{
    public float radius = 5;
    public float power = 5;
    public float upwardspower = 3f;
    private float correctedPower;
    private float explosioncorrectie = 100;

    void Start()
    {
        correctedPower = power * explosioncorrectie;
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(correctedPower, explosionPos, radius, upwardspower);
        }
        Destroy(gameObject);
    }
}