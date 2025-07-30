using UnityEngine;

public class Timer : MonoBehaviour
{
    private int seconds;

    private void Start()
    {
        seconds = 10;
        InvokeRepeating(nameof(UpdateTimer), 1f, 1f);
    }

    private void UpdateTimer()
    {
        if (seconds > 0)
        {
            seconds--;
            Debug.Log($"Timer: {seconds} seconds");
        }
    }
}