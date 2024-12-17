using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Image fillImage;

    [Tooltip("Adjust this value to control the lerp speed.")]
    [SerializeField] private float lerpSpeed = 2f;

    private float currentHealth;
    private float targetHealth;


    public void SetHealth(float maxHealth)
    {
        healthBar.maxValue = maxHealth;
        currentHealth = maxHealth;
        targetHealth = maxHealth;
        healthBar.value = maxHealth;
    }

    public void UpdateHealth(float newTargetHealth)
    {
        targetHealth = Mathf.Clamp(newTargetHealth, 0, healthBar.maxValue);
    }

    private void Update()
    {
        DepleteHealth();
        StartCoroutine(CheckHealth());
    }

    private void DepleteHealth()
    {
        // Smoothly interpolate the current health towards the target health
        if (Mathf.Abs(currentHealth - targetHealth) > 0.01f) // Threshold to stop near target
        {
            currentHealth = Mathf.Lerp(currentHealth, targetHealth, Time.deltaTime * lerpSpeed);
            healthBar.value = currentHealth;
        }
        else
        {
            currentHealth = targetHealth; // Snap to target to avoid small discrepancies
            healthBar.value = targetHealth;
        }
    }
    
    private IEnumerator CheckHealth()
    {
        // Ensure the fill image is completely empty when health is zero
        if (targetHealth <= 0)
        {
            yield return new WaitForSeconds(1);
            healthBar.value = 0; // Force slider to zero
            fillImage.fillAmount = 0; // Ensure fill image is cleared
            fillImage.enabled = false; // Optional: Hide the fill image entirely
        }
        else
        {
            fillImage.enabled = true; // Show the fill image when health is above zero
        }
    }
}