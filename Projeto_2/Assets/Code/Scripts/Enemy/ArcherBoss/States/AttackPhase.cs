using UnityEngine;

public class AttackPhase : IState
{
    public float TimePassed;
    private readonly ArcherBoss _archerBoss;
    private readonly GameObject _attackPattern;
    private readonly int _bossPartIndex;
    private GameObject bossPart;

    // Constructor
    public AttackPhase(ArcherBoss archerBoss, GameObject attackPattern, int bossPartIndex)
    {
        _archerBoss = archerBoss;
        _attackPattern = attackPattern;
        _bossPartIndex = bossPartIndex;
    }

    // Checks time passed since active
    // If a boss part was destroyed, spawns another
    public void Tick()
    {
        TimePassed += Time.deltaTime;
        if (bossPart == null)
        {
            bossPart = _archerBoss.SpawnBossPart(_archerBoss.GetComponent<BoxCollider2D>().bounds, _bossPartIndex);
            bossPart.transform.SetParent(_archerBoss.transform);
            TimePassed -= 2f;
        }
    }

    // Setup
    public void OnEnter()
    {
        TimePassed = 0f;
        _attackPattern.SetActive(true);
        bossPart = _archerBoss.SpawnBossPart(_archerBoss.GetComponent<BoxCollider2D>().bounds, _bossPartIndex);
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
