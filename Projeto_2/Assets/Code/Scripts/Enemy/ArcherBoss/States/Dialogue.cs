using UnityEngine;

public class Dialogue : IState
{
    public bool finished;
    private readonly ArcherBoss _archerBoss;
    private readonly GameObject _dialogueHolder;
    private readonly string _dialogueText;

    // Constructor
    public Dialogue(ArcherBoss archerBoss, GameObject dialogueHolder, string dialogueText)
    {
        _archerBoss = archerBoss;
        _dialogueHolder = dialogueHolder;
        _dialogueText = dialogueText;
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
        _archerBoss.Dialogue(_dialogueText);
    }

    // Clean up
    public void OnExit()
    {
        finished = false;
    }
}
