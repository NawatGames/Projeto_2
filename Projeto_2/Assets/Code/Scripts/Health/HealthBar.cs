using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider _slider;
    
    public Gradient gradient;
    public Image img;
    public Health health;

    private void Update()
    {
        img.color = gradient.Evaluate(health.GetPercentage());
    }

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