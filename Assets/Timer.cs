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

    public void Update()
    {
        if (GameManager.instance.phase == GamePhase.ending)
            return;
        timer -= Time.deltaTime;
        UpdateText(timer);
        if (timer < 0)
        {
            if (GameManager.instance.phase == GamePhase.starting)
            {
                Reset();
                GameManager.instance.phase = GamePhase.started;
            }
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
