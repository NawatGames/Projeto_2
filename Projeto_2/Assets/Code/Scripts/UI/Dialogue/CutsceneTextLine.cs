using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    // Basic cutscene card script that will control the text and image that will appear during the cutscene.
    public class CutsceneTextLine : DialogueBaseClass
    {
        private Text textHolder;
        private IEnumerator writeTextLine;
        private bool faded;
        private bool wait;
        private float counter;

        [Header ("Text Options")]
        [SerializeField] private string input; // Text to be displayed
        [SerializeField] private Color textColor; // Color of the text to be displayed
        [SerializeField] private Font textFont; // Font of the text to be displayed

        [Header ("Time Options")]
        [SerializeField] private float letterByLetterDelay; // Delay for letter by letter to appear

        [Header ("Cutscene Image")]
        [SerializeField] private Sprite cutsceneSprite; // Image to be displayed
        [SerializeField] private Image imageHolder; // gameObject to hold the image

        [Header ("Fading")]
        [SerializeField] private CanvasGroup canvasGroup; // gameObject parent of entire group to fade
        [SerializeField] private float fadeDuration; // Fade duration in seconds

        // Erases any text present on textHolder text and changes the image on imageHolder
        private void OnEnable()
        {
            canvasGroup.alpha = 0;
            faded = true;
            wait = false;

            textHolder = GetComponent<Text>();
            textHolder.text = "";

            imageHolder.sprite = cutsceneSprite;
            imageHolder.preserveAspect = true;

            StartCoroutine(Fade(canvasGroup, canvasGroup.alpha, faded?1:0, false));
            writeTextLine = WriteText(input, textHolder, textColor, textFont, letterByLetterDelay, fadeDuration);
            StartCoroutine(writeTextLine);
        }

        // Fades the canvas in or out
        private IEnumerator Fade(CanvasGroup canvasGroup, float start, float end, bool cutsceneEnd)
        {
            counter = 0f;

            while (counter < fadeDuration)
            {
                counter += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(start, end, counter/fadeDuration);

                yield return null;
            }

            faded = !faded;

            // Ends cutscene card after fading
            // Could use faded bool, but could result in bugs
            if (cutsceneEnd)
            {
                yield return new WaitForSeconds(fadeDuration);
                textLineFinished = true;
            }
        }

        // Ends the letter by letter animation for the text on player demand or continue the cutscene
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !wait)
            {
                if (textHolder.text != input)
                {
                    StopCoroutine(writeTextLine);
                    textHolder.text = input;
                    textHolder.color = textColor;
                    textHolder.font = textFont;
                }
                else
                {
                    StartCoroutine(Fade(canvasGroup, canvasGroup.alpha, faded?1:0, true));
                    wait = true;
                    
                    textHolder.text = "";
                }
            }
        }
    }
}
