using System.Collections;
using UnityEngine;

namespace CutsceneSystem
{
    public class CutsceneTextHolder : MonoBehaviour
    {
        // Starts to present all TextLines present as child of this gameObject.
        private void Awake()
        {
            StartCoroutine(textSequence());
        }

        // Function to write the text lines.
        private IEnumerator textSequence()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Deactivate();
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<CutsceneTextLine>().finished);
            }
            gameObject.SetActive(false);
        }

        // Erases text line for the next text line to appear.
        private void Deactivate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}