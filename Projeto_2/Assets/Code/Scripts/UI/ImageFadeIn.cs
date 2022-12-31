using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ImageFadeIn : MonoBehaviour
{
    private Image _image;
    private float _fadeAlpha;
    [SerializeField] float secondsToWait = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _fadeAlpha = 0f;
        _image = GetComponent<Image>();
        StartCoroutine(ReturnToMenu());
    }

    // Update is called once per frame
    void Update()
    {
        _image.color = new Color(255, 255, 255, _fadeAlpha += Time.deltaTime);
        if (_fadeAlpha >= 1f)
        {
            Debug.Log("Show button");
        }
    }

    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(secondsToWait);
        SceneManager.LoadScene(0);
    }
}
