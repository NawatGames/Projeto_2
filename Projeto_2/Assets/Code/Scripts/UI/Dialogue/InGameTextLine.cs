using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class InGameTextLine : DialogueBaseClass
    {
        private Text textHolder;
        private IEnumerator writeTextLine;
        private bool faded;
        private bool ending;

        [Header ("Text Options")]
        [SerializeField] private string input; // Text to be displayed
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

            textHolder = GetComponent<Text>();
            textHolder.text = "";
        }

        // Starts to write the text line every time it is activated
        private void OnEnable()
        {
            StartCoroutine(Fade(canvasGroup, canvasGroup.alpha, faded?1:0));
            writeTextLine = WriteText(input, textHolder, textColor, textFont, letterByLetterDelay, fadeDuration);
            StartCoroutine(writeTextLine);
        }

        // Starts fading when input text is fully shown, and deactivates the object when faded, resetting the object properties
        private void Update()
        {
            if((textHolder.text == input) && (!ending))
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
    }
}
