using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }
    private float seconds;
    private LevelDataSO currentLevelData;
    [SerializeField] private TimerView timerView;
    private bool isRunning = false;

    private void Awake()
    {
        Debug.Log("[Timer] Awake called.");
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // varsa fazlasını yok et
            return;
        }

        Instance = this;
        // Initialize with current level data if already loaded
        if (LevelManager.Instance != null && LevelManager.Instance.CurrentLevelData != null)
        {
            currentLevelData = LevelManager.Instance.CurrentLevelData;
        }

        LevelManager.OnLevelLoaded += HandleLevelLoaded;
    }

    private void OnDestroy()
    {
        LevelManager.OnLevelLoaded -= HandleLevelLoaded;
    }

    private void HandleLevelLoaded(LevelDataSO data)
    {
        currentLevelData = data;
    }

    private void Update()
    {
        if (timerView != null)
        {
            timerView.RenderTimer(GetTimer());
        }
    }

    public void StartTimer()
    {
        if (isRunning) return; 

        if (currentLevelData == null)
        {
            Debug.LogError("Timer: currentLevelData is NULL — Make sure a level is loaded before starting the timer.");
            return;
        }

        seconds = currentLevelData.gameDuration;
        
        isRunning = true;

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

    public void SetTimerActive()
    {
        Debug.Log($"Setting Timer active: true");
        timerView.timerText.gameObject.SetActive(true);
        gameObject.SetActive(true);
    } 
}