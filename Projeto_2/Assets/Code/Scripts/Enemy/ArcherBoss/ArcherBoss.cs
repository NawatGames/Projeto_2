using System;
using UnityEngine;
using UnityEngine.AI;

public class ArcherBoss : MonoBehaviour
{
    // Duration of attack phase
    [Header ("State options")]
    [SerializeField] private float _attackPhaseDuration = 3.0f;

    // Game objects that hold the dialogue and attacks
    [Header ("Holders")]
    [SerializeField] private GameObject dialogueHolder;
    [SerializeField] private GameObject attackPatternHolder;

    private StateMachine _stateMachine;
    
    public void Awake()
    {
        _stateMachine = new StateMachine(); // Instantiates the state machine

        // Instantiates the states
        var initialDialogue = new InitialDialogue(dialogueHolder);
        var attackPattern1 = new AttackPattern1(attackPatternHolder);
        var dialogue1 = new Dialogue1(dialogueHolder);

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

    private void Update() => _stateMachine.Tick(); // Calss Tick() function from current state
}
