using UnityEngine;

public class FadeUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeSpeed = 1f;

    void Update()
    {
        if (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;
        }
    }
}