using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider _slider;
    public Gradient gradient;

    public Health health;

    private void OnEnable()
    {
        _slider = GetComponent<Slider>();
        health.OnHealthChange += UpdateHealthBar;
    }

    private void OnDisable()
    {
        health.OnHealthChange -= UpdateHealthBar;
    }

    private void UpdateHealthBar(float value)
    {
        _slider.value = health.GetPercentage();
    }
}