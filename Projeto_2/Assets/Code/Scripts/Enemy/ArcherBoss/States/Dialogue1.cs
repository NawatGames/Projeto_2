using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue1 : IState
{
    public bool finished;
    private readonly GameObject _dialogueHolder;

    // Constructor
    public Dialogue1(GameObject dialogueHolder)
    {
        _dialogueHolder = dialogueHolder;
    }

    public void Tick()
    {
        // If dialogue line has finished appearing, finished = true
        if (!_dialogueHolder.transform.GetChild(1).gameObject.activeSelf)
        {
            finished = true;
        }
    }

    // Setup
    public void OnEnter()
    {
        finished = false;
        _dialogueHolder.transform.GetChild(1).gameObject.SetActive(true);
    }

    // Clean up
    public void OnExit() { }
}
