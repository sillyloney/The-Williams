using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int deliveredBoxes;
    public int totalBoxes;

    public float timeRemaining = 60f; 
    public bool gameEnded = false;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI resultText;

    public GameObject endPanel;
    public GameObject player;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateScore();

        if (endPanel != null)
            endPanel.SetActive(false);

        Time.timeScale = 0f; 
    }

    void Update()
    {
        if (gameEnded) return;

        timeRemaining -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timeRemaining <= 10)
        {
            timerText.color = Color.red;
        }
        else
        {
            timerText.color = Color.white;
        }

        if (timeRemaining <= 0)
        {
            LoseGame();
        }
    }

    public void BoxDelivered()
    {
        if (gameEnded) return;

        deliveredBoxes++;
        UpdateScore();

        if (deliveredBoxes >= totalBoxes)
        {
            WinGame();
        }
    }

    void UpdateScore()
    {
        scoreText.text ="Boxes "+ deliveredBoxes + " / " + totalBoxes;
    }

    void WinGame()
    {
        gameEnded = true;
        resultText.text = "DELIVERIES COMPLETED\n Parents are happy! All boxes are packed!";
        endPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StopPlayerMovement();
    }

    void LoseGame()
    {
        gameEnded = true;
        resultText.text = "DELIVERIES UNCOMPLETED\nParents are dissapointed! You didn’t finish in time!";
        endPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;

        StopPlayerMovement();
    }

    void StopPlayerMovement()
    {
        player.SetActive(false);
    }

    
}