using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CutsceneSystem
{
    public class CutsceneTextLine : CutsceneBaseClass
    {
        private Text textHolder;
        private IEnumerator writeTextLine;

        [Header ("Text Options")]
        [SerializeField] private string input;
        [SerializeField] private Color textColor;
        [SerializeField] private Font textFont;

        [Header ("Time Options")]
        [SerializeField] private float delay;

        [Header ("Background Image")]
        [SerializeField] private Sprite backgroundSprite;
        [SerializeField] private Image imageHolder;

        // Erases any text present on textHolder text and changes the image on imageHolder
        private void Awake()
        {
            textHolder = GetComponent<Text>();
            textHolder.text = "";

            imageHolder.sprite = backgroundSprite;
            imageHolder.preserveAspect = true;
        }

        // Starts to write the text line
        private void Start()
        {
            writeTextLine = WriteText(input, textHolder, textColor, textFont, delay);
            StartCoroutine(writeTextLine);
        }

        // Ends the letter by letter animation for the text and shows the line already finished
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
                    finished = true;
                }
            }
        }
    }
}
