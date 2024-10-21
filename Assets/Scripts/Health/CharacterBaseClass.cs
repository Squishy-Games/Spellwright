using UnityEngine;
using UnityEngine.Events;

public abstract class CharacterBaseClass : MonoBehaviour
{

    public float health;
    public float maxHealth;


    public virtual void TakeDamage(int damage)
    {
        health -= damage;
    }

    public virtual void Heal(float healHealth)
    {
        health += healHealth;
    }

}
