using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CutsceneSystem
{
    // Basic cutscene card script that will control the text and image that will appear during the cutscene.
    public class CutsceneTextLine : CutsceneBaseClass
    {
        private Text textHolder;
        private IEnumerator writeTextLine;
        private bool faded;

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
        private void Awake()
        {
            canvasGroup.alpha = 0;
            faded = true;

            textHolder = GetComponent<Text>();
            textHolder.text = "";

            imageHolder.sprite = cutsceneSprite;
            imageHolder.preserveAspect = true;
        }

        // Starts to write the text line
        private void Start()
        {
            StartCoroutine(Fade(canvasGroup, canvasGroup.alpha, faded?1:0, false));
            writeTextLine = WriteText(input, textHolder, textColor, textFont, letterByLetterDelay, canvasGroup, fadeDuration);
            StartCoroutine(writeTextLine);
        }

        // Fades the canvas in or out
        private IEnumerator Fade(CanvasGroup canvasGroup, float start, float end, bool cutsceneEnd)
        {
            float counter = 0f;

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
                cutsceneTextLineFinished = true;
            }
        }

        // Ends the letter by letter animation for the text on player demand or continue the cutscene
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (textHolder.text != input)
                {
                    StopCoroutine(writeTextLine);
                    textHolder.text = input;
                }
                else
                {
                    StartCoroutine(Fade(canvasGroup, canvasGroup.alpha, faded?1:0, true));
                }
            }
        }
    }
}
