using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    public float maxHealth;

    public event Action<float> OnHealthChange;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
        OnHealthChange?.Invoke(currentHealth);
    }

    public void RemoveHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth - value, 0, maxHealth);
        OnHealthChange?.Invoke(currentHealth);
    }
    
    public float GetHealth()
    {
        return currentHealth;
    }

    public float GetPercentage()
    {
        return currentHealth / maxHealth;
    }
}