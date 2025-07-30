using UnityEngine;

public class Timer : MonoBehaviour
{
    private float seconds;
    [SerializeField] private LevelDataLoader levelDataLoader;

    public void StartTimer()
    {
        seconds = levelDataLoader.GetCurrentLevelData().gameDuration;
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