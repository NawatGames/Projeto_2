using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random=UnityEngine.Random;

public class ArcherBoss : MonoBehaviour
{
    // Controls the boss stats and options
    [Header ("Boss stats and options")]
    [SerializeField] private float _maxHealth = 500f;
    [SerializeField] private List<GameObject> _bossPart = new List<GameObject>();
    [SerializeField] private Slider _slider;

    // Duration of attack phase
    [Header ("State options")]
    [SerializeField] private float _attackPhaseDuration = 3.0f;

    // Game objects that hold the dialogue and attacks
    [Header ("Holders")]
    [SerializeField] private GameObject _dialogueHolder;
    [SerializeField] private List<GameObject> _attackPattern;

    private StateMachine _stateMachine;
    public float currentHealth;
    
    // Setting state machine
    public void Awake()
    {
        currentHealth = _maxHealth;
        _slider.value = CalculateHealth();
        _stateMachine = new StateMachine(); // Instantiates the state machine

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //                                               Instantiates the states                                               //
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        var initialDialogue = new InitialDialogue(this, _dialogueHolder);
        var dialogue = new Dialogue(this, _dialogueHolder);
        var initialPhase = new AttackPhase(this, _attackPattern[0]);
        var phase1 = new AttackPhase(this, _attackPattern[1]);
        var phase2 = new AttackPhase(this, _attackPattern[2]);
        var phase3 = new AttackPhase(this, _attackPattern[3]);
        var phase4 = new AttackPhase(this, _attackPattern[4]);
        var bossDefeated = new BossDefeated(this);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //                                                     Transitions                                                     //
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        // Named paths transitions
        Path(initialDialogue, initialPhase, InitialDialogueFinished());
        Path(initialPhase, dialogue, InitialPhaseFinished());
        Path(dialogue, phase1, ToPhase1());
        Path(phase1, dialogue, Phase1Finished());
        Path(dialogue, phase2, ToPhase2());
        Path(phase2, dialogue, Phase2Finished());
        Path(dialogue, phase3, ToPhase3());
        Path(phase3, dialogue, Phase3Finished());
        Path(dialogue, phase4, ToPhase4());
        Path(phase4, dialogue, Phase4Finished());

        // Unnamed paths transitions
        _stateMachine.AddAnyTransition(bossDefeated, () => (currentHealth <= 0));

        // Sets initial state
        _stateMachine.SetState(initialDialogue);

        // Function to set transition
        void Path(IState previousState, IState nextState, Func<bool> condition) => _stateMachine.AddTransition(previousState, nextState, condition);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //                                                     Conditions                                                      //
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Func<bool> InitialDialogueFinished() => () => (initialDialogue.finished);
        Func<bool> ToPhase1() => () => (dialogue.finished && (CalculateHealth() >= 0.8));
        Func<bool> ToPhase2() => () => (dialogue.finished && (CalculateHealth() < 0.8) && (CalculateHealth() >= 0.5));
        Func<bool> ToPhase3() => () => (dialogue.finished && (CalculateHealth() < 0.5) && (CalculateHealth() >= 0.2));
        Func<bool> ToPhase4() => () => (dialogue.finished && (CalculateHealth() < 0.2));
        Func<bool> InitialPhaseFinished() => () => (initialPhase.TimePassed > _attackPhaseDuration);
        Func<bool> Phase1Finished() => () => (phase1.TimePassed > _attackPhaseDuration);
        Func<bool> Phase2Finished() => () => (phase2.TimePassed > _attackPhaseDuration);
        Func<bool> Phase3Finished() => () => (phase3.TimePassed > _attackPhaseDuration);
        Func<bool> Phase4Finished() => () => (phase4.TimePassed > _attackPhaseDuration);
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

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //                                                         Boss UI                                                         //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void Dialogue(string text)
    {
        _dialogueHolder.gameObject.SetActive(true);
        _dialogueHolder.GetComponent<InGameTextLine>().WriteTextLine(text);
    }
    
    // returns float to control healthbar slider
    public float CalculateHealth()
    {
        return currentHealth/_maxHealth;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //                                                Boss Attack Phase Control                                                //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Spawns attackable parts of the boss
    public GameObject SpawnBossPart(Bounds bounds, int n)
    {
        Vector2 pos = new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
        return Instantiate(_bossPart[n], pos, Quaternion.identity);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //                                                     Boss Clean Up                                                       //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
