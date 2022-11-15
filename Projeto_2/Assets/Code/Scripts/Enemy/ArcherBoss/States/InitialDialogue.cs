using UnityEngine;

public class InitialDialogue : IState
{
    public bool finished;
    private readonly ArcherBoss _archerBoss;
    private readonly GameObject _dialogueHolder;

    // Constructor
    public InitialDialogue(ArcherBoss archerBoss, GameObject dialogueHolder)
    {
        _archerBoss = archerBoss;
        _dialogueHolder = dialogueHolder;
    }

    public void Tick()
    {
        // If dialogue line has finished appearing, finished = true
        if (!_dialogueHolder.gameObject.activeSelf)
        {
            finished = true;
        }
    }

    // Setup
    public void OnEnter()
    {
        finished = false;
        _archerBoss.Dialogue("Initial dialogue");
    }

    // Clean up
    public void OnExit() { }
}
