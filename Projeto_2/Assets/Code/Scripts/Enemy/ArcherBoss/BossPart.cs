using UnityEngine;
using UnityEngine.UI;

// Class to handle the parts of boss that appear for the player to attack
public class BossPart : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 20;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _spawnInvincibilityTime = 0.2f;
    [SerializeField] private string destroyedDialogue;

    [HideInInspector] public int currentHealth;
    [HideInInspector] private float spawnInvincibilityTime;

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
        spawnInvincibilityTime -= Time.deltaTime;

        if (currentHealth <= 0)
        {
            if (transform.parent != null)
            {
                this.GetComponentInParent<ArcherBoss>().Dialogue(destroyedDialogue);
            }
            Destroy(this.gameObject);
        }

        if (currentHealth > _maxHealth)
        {
            currentHealth = _maxHealth;
        }

        _slider.value = CalculateHealth();
    }

    // Applies damage to the boss part game object and its parent (Archer boss) if exists
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void OnDestroy()
    {
        if (transform.parent != null && currentHealth <= 0)
        {
            this.GetComponentInParent<ArcherBoss>().currentHealth -= _maxHealth;
        }
    }

    // Calculates health for slider.
    public float CalculateHealth()
    {
        return (float)currentHealth/(float)_maxHealth;
    }
}
