using System.Collections;
using System.Collections.Generic;
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
    }

    // Clean up
    public void OnExit() { }
}