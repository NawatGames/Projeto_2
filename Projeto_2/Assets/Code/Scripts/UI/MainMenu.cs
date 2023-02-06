using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject firstButton;
    [SerializeField] EventSystem eventSystem;
    private bool isKeyboard = false;

    // Loads next scene set in build
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Closes the application
    public void ExitGame()
    {
        Debug.Log("Quitted game!");
        Application.Quit();
    }

    // Bug correction since Unity always highlight first selected of eventSystem
    public void Awake()
    {
        eventSystem.SetSelectedGameObject(null);
    }

    // Sets navigation via mouse or keyboard
    public void Update()
    {
        if ((Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0))
        {
            eventSystem.SetSelectedGameObject(null);
            isKeyboard = false;
        }
        else if (Input.anyKeyDown && !isKeyboard && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
        {
            eventSystem.SetSelectedGameObject(firstButton);
            isKeyboard = true;
        }
    }
}
