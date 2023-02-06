using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : IState
{
    private readonly ArcherBoss _archerBoss;
    private readonly GameObject _player;
    private readonly Image _transitionPanelCanvas;
    private float _fadeAlpha;

    // Constructor
    public GameOver(ArcherBoss archerBoss, GameObject player, Image transitionPanelCanvas)
    {
        _archerBoss = archerBoss;
        _player = player;
        _transitionPanelCanvas = transitionPanelCanvas;
        _fadeAlpha = 0;
    }

    public void Tick()
    {
        _transitionPanelCanvas.color = new Color(0, 0, 0, (_fadeAlpha += Time.deltaTime)/3);
        if (_fadeAlpha >= 3f)
        {
            _player.GetComponent<PlayerMovement>().enabled = true;
            SceneManager.LoadScene(4);
        }
    }

    // Setup
    public void OnEnter()
    {
        _archerBoss.CleanUp();
        _archerBoss.Dialogue("HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA HA");
        _player.GetComponent<PlayerMovement>().enabled = false;
    }

    // Clean up
    public void OnExit()
    {
    }
}
