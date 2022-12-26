using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InGameTextLine : MonoBehaviour
{
    private Text textHolder;
    private IEnumerator writeTextLine;
    private bool faded;
    private bool ending;
    private string _text;

    [Header ("Text Options")]
    [SerializeField] private Color textColor; // Color of the text to be displayed
    [SerializeField] private Font textFont; // Font of the text to be displayed

    [Header ("Time Options")]
    [SerializeField] private float letterByLetterDelay; // Delay for letter by letter to appear

    [Header ("Fading")]
    [SerializeField] private CanvasGroup canvasGroup; // gameObject parent of entire group to fade
    [SerializeField] private float fadeDuration; // Fade duration in seconds

    // Erases any text present on textHolder text and changes the image on imageHolder
    private void Awake()
    {
        canvasGroup.alpha = 0;
        faded = true;
        ending = false;

        textHolder = this.GetComponentInChildren<Text>();
        textHolder.text = "";
    }

    // Starts to write the text line every time it is activated
    public void WriteTextLine(string text)
    {
        _text = text;
        StartCoroutine(Fade(canvasGroup, canvasGroup.alpha, faded?1:0));
        writeTextLine = WriteText(_text, textHolder, textColor, textFont, letterByLetterDelay, fadeDuration);
        StartCoroutine(writeTextLine);
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    // Starts fading when input text is fully shown, and deactivates the object when faded, resetting the object properties
    private void Update()
    {
        if((textHolder.text == _text) && (!ending))
        {
            StartCoroutine(Fade(canvasGroup, canvasGroup.alpha, faded?1:0));
            ending = true;
        }
        if((ending) && (faded))
        {
            ending = false;
            textHolder.text = "";
            gameObject.SetActive(false);
        }
    }

    // Fades the canvas in or out
    private IEnumerator Fade(CanvasGroup canvasGroup, float start, float end)
    {
        float counter = 0f;

        while (counter < fadeDuration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter/fadeDuration);

            yield return null;
        }

        faded = !faded;
    }
    
    private IEnumerator WriteText(string input, Text textHolder, Color textColor, Font textFont, float delay, float fadeDuration)
    {
        yield return new WaitForSeconds(fadeDuration);

        textHolder.color = textColor;
        textHolder.font = textFont;

        for (int i = 0; i < input.Length; i++)
        {
            textHolder.text += input[i];
            yield return new WaitForSeconds(delay);
        }
    }
}
