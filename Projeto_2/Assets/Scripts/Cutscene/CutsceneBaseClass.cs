using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CutsceneSystem
{
    public class CutsceneBaseClass : MonoBehaviour
    {
        // Boolean to define whether the text has completely appeared or not.
        public bool finished { get; protected set; }

        // Function for writing letter by letter the inputted string with a delay on Text component
        // string input - String to be displayed on textHolder
        // Text textHolder - Text component where input will be written
        // Color textColor - Sets color for the text component
        // Font textFont - Sets font for the text component
        // float delay - Defines delay between each letter to appear
        protected IEnumerator WriteText(string input, Text textHolder, Color textColor, Font textFont, float delay)
        {
            textHolder.color = textColor;
            textHolder.font = textFont;

            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                yield return new WaitForSeconds(delay);
            }

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            finished = true;
        }
    }
}
