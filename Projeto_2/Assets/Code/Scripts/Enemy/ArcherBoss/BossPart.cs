using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class to handle the parts of boss that appear for the player to attack
public class BossPart : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 20f;
    [SerializeField] private string _attackTag = "Player";
    [SerializeField] private Slider _slider;

    private float _currentHealth;

    // Calculates damage when attacked
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == _attackTag)
        {
            _currentHealth--;
            this.GetComponentInParent<ArcherBoss>()._currentHealth--;
        }
    }

    // Sets health to maxhealth and adjusts the healthbar slider
    public void Awake()
    {
        _currentHealth = _maxHealth;
        _slider.value = CalculateHealth();
    }

    // When boss part's health reaches 0, destroys the body part
    // Adjusts the health bar accordingly
    public void Update()
    {
        _slider.value = CalculateHealth();

        if (_currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    public float CalculateHealth()
    {
        return _currentHealth/_maxHealth;
    }
}
