using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class TextRenderFilter : MonoBehaviour
{
    void Start ()
    {
        GetComponent<Text>().font.material.mainTexture.filterMode = FilterMode.Point;
    }
}