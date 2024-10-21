using UnityEngine;
using UnityEngine.UI;

public class PlayerClass : CharacterBaseClass
{
    private PlayerHealthBar _healthBar;
    
    // Start is called before the first frame update
    protected void Start()
    {
        health = maxHealth;
        _healthBar = FindObjectOfType<PlayerHealthBar>();
        _healthBar.setHealth(maxHealth);
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;
        _healthBar.UpdateHealth(health);
    }

    public override void Heal(float healHealth)
    {
        health += healHealth;
        _healthBar.UpdateHealth(healHealth);
    }
}
