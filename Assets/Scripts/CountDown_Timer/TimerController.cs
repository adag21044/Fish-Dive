using UnityEngine;

public class TimerController : MonoBehaviour
{
    public static TimerController Instance { get; private set; }
    private float seconds;

    [SerializeField] private TimerView timerView;
    private bool isRunning = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // varsa fazlasını yok et
            return;
        }
        Instance = this;
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

        if (LevelDataLoader.Instance == null)
        {
            Debug.LogError("TimerController: LevelDataLoader.Instance is NULL");
            return;
        }

        if (LevelDataLoader.Instance.GetCurrentLevelData() == null)
        {
            Debug.LogError("TimerController: LevelDataLoader.Instance.GetCurrentLevelData() is NULL");
            return;
        }

        seconds = LevelDataLoader.Instance.GetCurrentLevelData().gameDuration;
        
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