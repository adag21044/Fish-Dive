using UnityEngine;

public class Timer : MonoBehaviour
{
    private float seconds;
    [SerializeField] private LevelDataLoader levelDataLoader;

    private void Awake()
    {
        Debug.Log($"[Timer] Awake called. levelDataLoader is null? {levelDataLoader == null}");
    } 


    public void StartTimer()
    {
        if (levelDataLoader == null)
        {
            Debug.LogError("Timer: levelDataLoader is NULL — Did you assign it in the Inspector?");
            return;
        }
        
        seconds = levelDataLoader.GetCurrentLevelData().gameDuration;
        if (levelDataLoader == null)
        {
            Debug.LogError("Timer: levelDataLoader is NULL");
        }
        Debug.Log($"Timer started with {seconds} seconds.");
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

    public float GetTimer()
    {
        Debug.Log($"Current Timer: {seconds} seconds");
        return seconds;
    }
}