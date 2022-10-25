using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    public PlayerMovement player;

    public void SetStaminaPlayer(float stamina)
    {
        slider.maxValue = stamina;
        slider.value = stamina;
    }

    public void SetStamina(float stamina)
    {
        if(stamina <= 0f)
        {
            slider.value = slider.maxValue;
        }
        else
        {
            slider.value = stamina;
        }
    }

}
