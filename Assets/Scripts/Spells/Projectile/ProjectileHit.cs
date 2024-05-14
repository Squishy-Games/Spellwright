using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    private float _baseSurviveTime;
    private ExplosionManager _explosionManager;

    private void Start()
    {
        _explosionManager = FindObjectOfType<ExplosionManager>();
        if (_baseSurviveTime == 0)
            return;
        Invoke("EndProjectile", _baseSurviveTime);
    }

    private void EndProjectile()
    {
        if (!gameObject.activeSelf)
            return;
        _explosionManager.CreateExplosion(transform.position,
            FindObjectOfType<ProjectilespellManager>().projectilespells[gameObject].ExplosionStruct);

        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        EndProjectile();
    }
}