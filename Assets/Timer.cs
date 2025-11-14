using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timer;
    public float initialValue;
    public TextMeshProUGUI timerText;

    private void Start()
    {
        CountDown();
    }

    public void CountDown()
    {
        timer = 3;
        UpdateText(timer);
    }

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

    public void UpdateText(float time)
    {
        timerText.text = $"{timer.ToString("F1")}s";
    }
}
