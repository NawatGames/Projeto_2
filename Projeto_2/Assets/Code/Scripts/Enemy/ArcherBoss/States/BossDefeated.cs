using UnityEngine;

public class BossDefeated : IState
{
    private readonly ArcherBoss _archerBoss;

    // Constructor
    public BossDefeated(ArcherBoss archerBoss)
    {
        _archerBoss = archerBoss;
    }

    public void Tick() {}

    // Setup
    public void OnEnter()
    {
        Debug.Log("Game Cleared!!");
        _archerBoss.DespawnBossAttack();
        _archerBoss.DespawnBossParts();
        _archerBoss.transform.Find("AttackPatternHolder").gameObject.SetActive(false);
    }

    // Clean up
    public void OnExit()
    {
    }
}
