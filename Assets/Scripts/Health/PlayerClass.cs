using UnityEngine;
using UnityEngine.UI;

public class PlayerClass : CharacterBaseClass
{
    private PlayerHealthBar _healthBar;

    void Start()
    {
        health = maxHealth;
        _healthBar = FindObjectOfType<PlayerHealthBar>();
        _healthBar.SetHealth(maxHealth);
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;

        health = Mathf.Clamp(health, 0f, maxHealth);

        _healthBar.UpdateHealth(health);
    }

    public override void Heal(float healAmount)
    {
        health += healAmount;
        health = Mathf.Clamp(health, 0f, maxHealth);
        _healthBar.UpdateHealth(health);
    }
}