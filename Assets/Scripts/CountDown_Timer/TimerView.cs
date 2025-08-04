using UnityEngine;
using TMPro;

public class TimerView : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI timerText;

    public void RenderTimer(float seconds)
    {
        if (timerText != null)
        {
            timerText.text = seconds.ToString("F0");
        }
    }
}