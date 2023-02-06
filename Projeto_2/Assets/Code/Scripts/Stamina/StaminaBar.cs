using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    private Slider _slider;
    public Stamina stamina;

    private void OnEnable()
    {
        _slider = GetComponent<Slider>();
        stamina.OnStaminaChanged += UpdateStaminaBar;
    }

    private void OnDisable()
    {
        stamina.OnStaminaChanged -= UpdateStaminaBar;
    }

    private void UpdateStaminaBar(float value)
    {
        _slider.value = stamina.GetPercentage();
    }
}