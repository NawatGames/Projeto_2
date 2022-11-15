using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class to handle the parts of boss that appear for the player to attack
public class BossPart : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 20;
    //[SerializeField] private string _attackTag = "PlayerWeapon";
    [SerializeField] private Slider _slider;
    [SerializeField] private float _spawnInvincibilityTime = 0.2f;

    private int currentHealth;
    private float spawnInvincibilityTime;

    // Calculates damage when attacked

    // Sets health to maxhealth and adjusts the healthbar slider
    public void Awake()
    {
        currentHealth = _maxHealth;
        _slider.value = CalculateHealth();
        spawnInvincibilityTime = _spawnInvincibilityTime;
    }

    // When boss part's health reaches 0, destroys the body part
    // Adjusts the health bar accordingly
    public void Update()
    {
        _slider.value = CalculateHealth();
        spawnInvincibilityTime -= Time.deltaTime;

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }

        if (currentHealth > _maxHealth)
        {
            currentHealth = _maxHealth;
        }
    }

    public float CalculateHealth()
    {
        return currentHealth/_maxHealth;
    }
}
