using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to handle the parts of boss that appear for the player to attack
public class BossPart : MonoBehaviour
{
    [SerializeField] private float _currentHealth = 20f;
    [SerializeField] private string _attackTag = "Player";

    // Calculates damage when attacked
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == _attackTag)
        {
            _currentHealth--;
            this.GetComponentInParent<ArcherBoss>()._currentHealth--;
        }
    }

    // When boss part's health reaches 0, destroys the body part
    public void Update()
    {
        if (_currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
