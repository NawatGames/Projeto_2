using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random=UnityEngine.Random;

public class ArcherBoss : MonoBehaviour
{
    // Controls the boss stats and options
    [Header ("Boss stats and options")]
    [SerializeField] private static float _maxHealth = 500f;
    [SerializeField] private List<GameObject> _bossPart = new List<GameObject>();
    [SerializeField] private Slider _slider;

    // Duration of attack phase
    [Header ("State options")]
    [SerializeField] private float _attackPhaseDuration = 3.0f;

    // Game objects that hold the dialogue and attacks
    [Header ("Holders")]
    [SerializeField] private GameObject _dialogueHolder;
    [SerializeField] private GameObject _attackPattern;

    private StateMachine _stateMachine;
    public float currentHealth;
    
    public void Awake()
    {
        currentHealth = _maxHealth;
        _slider.value = CalculateHealth();
        _stateMachine = new StateMachine(); // Instantiates the state machine

        // Instantiates the states
        var initialDialogue = new InitialDialogue(_dialogueHolder);
        var attackPattern1 = new AttackPattern1(this, _attackPattern, currentHealth);
        var dialogue1 = new Dialogue1(_dialogueHolder);
        var bossDefeated = new BossDefeated(this);

        // Sets transitions
        Path(initialDialogue, attackPattern1, InitialDialogueFinished());
        Path(attackPattern1, dialogue1, TimeHasPassed());
        Path(dialogue1, attackPattern1, Dialogue1Finished());

        _stateMachine.AddAnyTransition(bossDefeated, () => (currentHealth <= 0));

        // Sets initial state
        _stateMachine.SetState(initialDialogue);

        // Function to set transition
        void Path(IState previousState, IState nextState, Func<bool> condition) => _stateMachine.AddTransition(previousState, nextState, condition);

        // Condition functions
        Func<bool> InitialDialogueFinished() => () => (initialDialogue.finished);
        Func<bool> Dialogue1Finished() => () => (dialogue1.finished);
        Func<bool> TimeHasPassed() => () => (attackPattern1.TimePassed > _attackPhaseDuration);
    }

    // Calls Tick() function from statemachine, sets health.
    private void Update()
    {
        _stateMachine.Tick(); // Calls Tick() function from current state

        if (currentHealth > _maxHealth)
        {
            currentHealth = _maxHealth;
        }

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        _slider.value = CalculateHealth();
    }
    
    // returns float to control healthbar slider
    public float CalculateHealth()
    {
        return currentHealth/_maxHealth;
    }

    // Spawns attackable parts of the boss
    public GameObject SpawnBossPart(Bounds bounds, int n)
    {
        Vector2 pos = new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
        return Instantiate(_bossPart[n], pos, Quaternion.identity);
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
        GameObject[] bossParts;
        bossParts = GameObject.FindGameObjectsWithTag("BossPart");

        foreach(GameObject bossPart in bossParts)
        {
            Destroy(bossPart);
        }
    }
}
