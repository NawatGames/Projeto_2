using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random=UnityEngine.Random;

public class ArcherBoss : MonoBehaviour
{
    // Controls the boss stats
    [Header ("Boss stats")]
    [SerializeField] private static float _maxHealth = 500f;
    [SerializeField] public GameObject _bossPart;
    public float _currentHealth = _maxHealth;

    // Duration of attack phase
    [Header ("State options")]
    [SerializeField] private float _attackPhaseDuration = 3.0f;

    // Game objects that hold the dialogue and attacks
    [Header ("Holders")]
    [SerializeField] private GameObject _dialogueHolder;
    [SerializeField] private GameObject _attackPattern;

    private StateMachine _stateMachine;
    
    public void Awake()
    {
        _stateMachine = new StateMachine(); // Instantiates the state machine

        // Instantiates the states
        var initialDialogue = new InitialDialogue(_dialogueHolder);
        var attackPattern1 = new AttackPattern1(this, _attackPattern, _currentHealth);
        var dialogue1 = new Dialogue1(_dialogueHolder);

        // Sets transitions
        Path(initialDialogue, attackPattern1, InitialDialogueFinished());
        Path(attackPattern1, dialogue1, TimeHasPassed());
        Path(dialogue1, attackPattern1, Dialogue1Finished());

        // Sets initial state
        _stateMachine.SetState(initialDialogue);

        // Function to set transition
        void Path(IState previousState, IState nextState, Func<bool> condition) => _stateMachine.AddTransition(previousState, nextState, condition);

        // Condition functions
        Func<bool> InitialDialogueFinished() => () => (initialDialogue.finished);
        Func<bool> Dialogue1Finished() => () => (dialogue1.finished);
        Func<bool> TimeHasPassed() => () => (attackPattern1.TimePassed > _attackPhaseDuration);
    }

    private void Update() => _stateMachine.Tick(); // Calls Tick() function from current state

    // Spawns attackable parts of the boss
    public GameObject SpawnBossPart(Bounds bounds)
    {
        Vector2 pos = new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
        return Instantiate(_bossPart, pos, Quaternion.identity);
    }

    // Despawns the boss' attacks
    public void DespawnBossAttack()
    {
        GameObject[] _bossAttacks;
        _bossAttacks = GameObject.FindGameObjectsWithTag("BossAttack");

        foreach(GameObject attack in _bossAttacks)
        {
            Destroy(attack);
        }
    }

    // Despawns the boss' attackable parts
    public void DespawnBossParts()
    {
        GameObject[] _bossParts;
        _bossParts = GameObject.FindGameObjectsWithTag("BossPart");

        foreach(GameObject bossPart in _bossParts)
        {
            Destroy(bossPart);
        }
    }
}
