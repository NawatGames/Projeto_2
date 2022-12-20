using System;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] private float currentStamina;
    public float maxStamina;

    public event Action<float> OnStaminaChanged;

    private void OnEnable()
    {
        currentStamina = maxStamina;
    }

    public void AddStamina(float value)
    {
        currentStamina = Mathf.Clamp(currentStamina + value, 0, maxStamina);
        OnStaminaChanged?.Invoke(currentStamina);
    }

    public void RemoveStamina(float value)
    {
        currentStamina = Mathf.Clamp(currentStamina - value, 0, maxStamina);
        OnStaminaChanged?.Invoke(currentStamina);
    }
    
    public float GetStamina()
    {
        return currentStamina;
    }

    public float GetPercentage()
    {
        return currentStamina / maxStamina;
    }
}