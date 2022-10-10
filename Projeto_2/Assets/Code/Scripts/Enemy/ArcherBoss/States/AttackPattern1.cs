using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPattern1 : IState
{
    public float TimePassed;
    private readonly ArcherBoss _archerBoss;
    private readonly GameObject _attackPattern;
    private readonly float _currentHealth;

    // Constructor
    public AttackPattern1(ArcherBoss archerBoss, GameObject attackPattern, float currentHealth)
    {
        _archerBoss = archerBoss;
        _attackPattern = attackPattern;
        _currentHealth = currentHealth;
    }

    // Checks time passed since active
    public void Tick()
    {
        TimePassed += Time.deltaTime;
    }

    // Setup
    public void OnEnter()
    {
        TimePassed = 0f;
        _attackPattern.SetActive(true);
        GameObject bossPart = _archerBoss.SpawnBossPart(_archerBoss.GetComponent<BoxCollider2D>().bounds);
        bossPart.transform.SetParent(_archerBoss.transform);
    }

    // Clean up
    public void OnExit()
    {
        _archerBoss.DespawnBossAttack();
        _archerBoss.DespawnBossParts();
        _attackPattern.SetActive(false);
    }
}