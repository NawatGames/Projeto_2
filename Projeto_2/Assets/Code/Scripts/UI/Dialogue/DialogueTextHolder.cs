using System.Collections;
using UnityEngine;

namespace DialogueSystem
{
    // Script to be placed on Gameobject that will hold the Text gameobjects as children
    public class DialogueTextHolder : MonoBehaviour
    {
        // Starts to present all TextLines present as child of this gameObject.
        private void Awake()
        {
            StartCoroutine(textSequence());
        }

        // Function to call children gameObject that will play cutscenes cards on sequence by hierarchy.
        private IEnumerator textSequence()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Deactivate();
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<CutsceneTextLine>().textLineFinished);
            }
            gameObject.SetActive(false);
        }

        // Deactivate all children gameobjects of this gameObject.
        private void Deactivate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}