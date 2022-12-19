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
        // initial Dialogue -> Represents the first text line the boss will say to the player
        // dialogue -> Text line to be shown from time to time during a phase
        // phase -> Represents the moment when the boss starts attacking the player
        // transition -> State to change to next phase

        var initialDialogue = new Dialogue(this, _dialogueHolder, "O que uma criatura asquerosa como você acha que pode fazer contra mim?");

        var dialogue1 = new Dialogue(this, _dialogueHolder, "Phase 1!");
        var dialogue2 = new Dialogue(this, _dialogueHolder, "Que insistência...");
        var dialogue3 = new Dialogue(this, _dialogueHolder, "Phase 3!");
        var dialogue4 = new Dialogue(this, _dialogueHolder, "MORRA, MORRA, MORRA!!");

        var phase1 = new AttackPhase(this, _attackPattern[0], 0);
        var phase2 = new AttackPhase(this, _attackPattern[1], 1);
        var phase3 = new AttackPhase(this, _attackPattern[2], 2);
        var phase4 = new AttackPhase(this, _attackPattern[3], 3);

        var transitionToPhase2 = new Dialogue(this, _dialogueHolder, "Ei, no rosto não! Acabei de sair do salão!");
        var transitionToPhase3 = new Dialogue(this, _dialogueHolder, "To Phase 3!");
        var transitionToPhase4 = new Dialogue(this, _dialogueHolder, "Você vai pagar por isso!");

        var bossDefeated = new BossDefeated(this);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //                                                     Transitions                                                     //
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        // Named paths transitions
        Path(initialDialogue, phase1, InitialDialogueFinished());

        Path(phase1, dialogue1, Attack1Finished());
        Path(dialogue1, phase1, Dialogue1Finished());
        Path(phase1, transitionToPhase2, Transition1());
        Path(transitionToPhase2, phase2, EndOfTransition1());

        Path(phase2, dialogue2, Attack2Finished());
        Path(dialogue2, phase2, Dialogue2Finished());
        Path(phase2, transitionToPhase3, Transition2());
        Path(transitionToPhase3, phase3, EndOfTransition2());
        
        Path(phase3, dialogue3, Attack3Finished());
        Path(dialogue3, phase3, Dialogue3Finished());
        Path(phase3, transitionToPhase4, Transition3());
        Path(transitionToPhase4, phase4, EndOfTransition3());
        
        Path(phase4, dialogue4, Attack4Finished());
        Path(dialogue4, phase4, Dialogue4Finished());

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

        Func<bool> Dialogue1Finished() => () => (dialogue1.finished);
        Func<bool> Dialogue2Finished() => () => (dialogue2.finished);
        Func<bool> Dialogue3Finished() => () => (dialogue3.finished);
        Func<bool> Dialogue4Finished() => () => (dialogue4.finished);

        Func<bool> Attack1Finished() => () => (phase1.TimePassed > _attackPhaseDuration && CalculateHealth() >= 0.8);
        Func<bool> Attack2Finished() => () => (phase2.TimePassed > _attackPhaseDuration && CalculateHealth() >= 0.5);
        Func<bool> Attack3Finished() => () => (phase3.TimePassed > _attackPhaseDuration && CalculateHealth() >= 0.2);
        Func<bool> Attack4Finished() => () => (phase4.TimePassed > _attackPhaseDuration);

        Func<bool> Transition1() => () => (phase1.TimePassed > _attackPhaseDuration && CalculateHealth() < 0.8);
        Func<bool> Transition2() => () => (phase1.TimePassed > _attackPhaseDuration && CalculateHealth() < 0.5);
        Func<bool> Transition3() => () => (phase1.TimePassed > _attackPhaseDuration && CalculateHealth() < 0.2);

        Func<bool> EndOfTransition1() => () => (transitionToPhase2.finished);
        Func<bool> EndOfTransition2() => () => (transitionToPhase3.finished);
        Func<bool> EndOfTransition3() => () => (transitionToPhase4.finished);
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

    // Function to activate Boss canvas for dialogue and write text on it
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

    // Spawns attackable parts of the boss on random places within given Bounds
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
