using UnityEngine;

// Class to handle the parts of boss that appear for the player to attack
public class BossPart : MonoBehaviour
{
    [SerializeField] private float spawnInvincibilityTime = 0.2f;
    [SerializeField] private string destroyedDialogue;
    
    private float _spawnInvincibilityTime;
    private GameObject _archerBoss;
    private Health _health;

    public void Awake()
    {
        _spawnInvincibilityTime = spawnInvincibilityTime;
        _health = GetComponent<Health>();
        _archerBoss = GameObject.FindGameObjectWithTag("Boss");
    }

    private void OnEnable()
    {
        _health.OnDeath += BossPartBreak;
    }

    private void OnDisable()
    {
        _health.OnDeath -= BossPartBreak;
    }
    
    public void Update()
    {
        _spawnInvincibilityTime -= Time.deltaTime;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void BossPartBreak()
    {
        if (transform.parent == null || !(_spawnInvincibilityTime <= 0)) return;
        _archerBoss.GetComponent<Health>().RemoveHealth(_health.maxHealth);
        _archerBoss.GetComponent<ArcherBoss>().Dialogue(destroyedDialogue);
        Destroy(gameObject);
    }
}
