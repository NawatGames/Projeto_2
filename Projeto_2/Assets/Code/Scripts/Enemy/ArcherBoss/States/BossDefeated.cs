using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossDefeated : IState
{
    private readonly ArcherBoss _archerBoss;
    private readonly Image _transitionPanelCanvas;
    private float _fadeAlpha;

    // Constructor
    public BossDefeated(ArcherBoss archerBoss, Image transitionPanelCanvas)
    {
        _archerBoss = archerBoss;
        _transitionPanelCanvas = transitionPanelCanvas;
        _fadeAlpha = 0;
    }

    public void Tick()
    {
        _transitionPanelCanvas.color = new Color(255, 255, 255, _fadeAlpha += Time.deltaTime);
        if (_fadeAlpha >= 1f)
        {
            SceneManager.LoadScene(3);
        }
    }

    // Setup
    public void OnEnter()
    {
        _archerBoss.CleanUp();
    }

    // Clean up
    public void OnExit()
    {
    }
}
