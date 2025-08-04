using UnityEngine;
using TMPro;

public class TimerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Timer timer;

    private void Update()
    {
        if (timer != null)
        {
            RenderTimer(timer.GetTimer());
        }
    }

    private void RenderTimer(float seconds)
    {
        if (timerText != null)
        {
            timerText.text = seconds.ToString("F0");
        }
    }
}   