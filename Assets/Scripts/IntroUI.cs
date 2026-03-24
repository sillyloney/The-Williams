using UnityEngine;
using UnityEngine.InputSystem;

public class IntroUI : MonoBehaviour
{
    public GameObject introPanel;
    public GameObject player;
    public CanvasGroup canvasGroup;

    public float fadeSpeed = 2f;

    bool started = false;

    void Start()
    {
        introPanel.SetActive(true);

        canvasGroup.alpha = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        player.SetActive(false);
    }

    void Update()
    {
        // fade in
        if (!started && canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.unscaledDeltaTime * fadeSpeed;
        }

        // press any key
        if (!started && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            started = true;
        }

        // fade out
        if (started)
        {
            canvasGroup.alpha -= Time.unscaledDeltaTime * fadeSpeed;

            if (canvasGroup.alpha <= 0)
            {
                introPanel.SetActive(false);

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                player.SetActive(true);
                Time.timeScale = 1f;

                enabled = false;
            }
        }
    }
}