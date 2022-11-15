using UnityEngine;

public class AttackPhase : IState
{
    public float TimePassed;
    private readonly ArcherBoss _archerBoss;
    private readonly GameObject _attackPattern;
    private GameObject bossPart;

    // Constructor
    public AttackPhase(ArcherBoss archerBoss, GameObject attackPattern)
    {
        _archerBoss = archerBoss;
        _attackPattern = attackPattern;
    }

    // Checks time passed since active
    // If a boss part was destroyed, spawns another
    public void Tick()
    {
        TimePassed += Time.deltaTime;
        if (bossPart == null)
        {
            bossPart = _archerBoss.SpawnBossPart(_archerBoss.GetComponent<BoxCollider2D>().bounds, 0);
            bossPart.transform.SetParent(_archerBoss.transform);
            TimePassed -= 5f;
        }
    }

    // Setup
    public void OnEnter()
    {
        TimePassed = 0f;
        _attackPattern.SetActive(true);
        bossPart = _archerBoss.SpawnBossPart(_archerBoss.GetComponent<BoxCollider2D>().bounds, 0);
        bossPart.transform.SetParent(_archerBoss.transform);
    }

    // Clean up
    public void OnExit()
    {
        TimePassed = 0f;
        _archerBoss.DespawnBossAttack();
        _archerBoss.DespawnBossParts();
        _attackPattern.SetActive(false);
    }
}

