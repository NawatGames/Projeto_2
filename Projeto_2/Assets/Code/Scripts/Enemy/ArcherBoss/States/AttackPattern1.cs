using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPattern1 : IState
{
    public float TimePassed;
    private readonly GameObject _attackPatternHolder;

    // Constructor
    public AttackPattern1(GameObject attackPatternHolder)
    {
        _attackPatternHolder = attackPatternHolder;
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
        _attackPatternHolder.transform.GetChild(0).gameObject.SetActive(true);
    }

    // Clean up
    public void OnExit()
    {
        _attackPatternHolder.transform.GetChild(0).gameObject.SetActive(false);
    }
}
