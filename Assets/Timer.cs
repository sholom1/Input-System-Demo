using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    // The actual timer value
    public float timer;
    // The value we will reset to
    public float initialValue;
    public TextMeshProUGUI timerText;

    // When the game starts use the countdown
    private void Start()
    {
        CountDown();
    }

    // Set's the timer to three
    public void CountDown()
    {
        timer = 3;
        UpdateText(timer);
    }

    // Reset the timer to the intial value
    public void Reset()
    {
        timer = initialValue;
        UpdateText(timer);
    }

    // Main timer logic
    public void Update()
    {
        // If the round has ended the timer should be paused
        if (GameManager.instance.phase == GamePhase.ending)
            return;

        // Otherwise reduce the timer by deltatime
        timer -= Time.deltaTime;
        UpdateText(timer);

        // When the timer reaches zero change the game phase
        if (timer < 0)
        {
            // If the round was starting reset and start the game
            if (GameManager.instance.phase == GamePhase.starting)
            {
                Reset();
                GameManager.instance.phase = GamePhase.started;
            }
            // Otherwise if the game is running and the timer reaches zero everyone loses
            else if (GameManager.instance.phase == GamePhase.started)
            {
                GameManager.instance.KillAll();
            }
        }
    }

    // Update the text in the formart "10.0s"
    public void UpdateText(float time)
    {
        timerText.text = $"{time.ToString("F1")}s";
    }
}
