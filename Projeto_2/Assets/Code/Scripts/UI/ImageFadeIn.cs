using UnityEngine;
using UnityEngine.UI;

public class ImageFadeIn : MonoBehaviour
{
    private Image _image;
    private float _fadeAlpha;

    // Start is called before the first frame update
    void Start()
    {
        _fadeAlpha = 0f;
        _image = GetComponent<Image>();
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
}
