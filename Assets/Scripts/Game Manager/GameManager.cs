using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Central orchestrator of the gameplay flow:
/// Scene draw -> Countdown -> Fish spawn -> Bubble spawns -> Number announce
/// Listens correct/wrong answer events, tracks timer, and decides win/lose.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Scene & Flow")]
    [SerializeField] private SceneController sceneController;
    [SerializeField] private Countdown countdown;
    [SerializeField] private TimerController timerController;

    [Header("Gameplay Systems")]
    [SerializeField] private FishSpawner fishSpawner;           // spawns fish (has OnFishSpawned)
    [SerializeField] private BubbleSpawner bubbleSpawner;       // auto-subscribes to fish spawn in its Start
    [SerializeField] private NumberAnnouncer numberAnnouncer;   // announces numbers periodically
    [SerializeField] private AnswerObserver answerObserver;     // emits correct/wrong events (after our small patch)
    [SerializeField] private HeartController heartController;   // optional: for remaining lives UI

    [Header("End UI (Optional)")]
    //[SerializeField] private GameObject winPanel;
    //[SerializeField] private GameObject losePanel;

    [Header("Config")]
    [SerializeField] private bool autoStart = true;             // if true, orchestrates automatically on Start()

    // State
    private bool isGameRunning = false;
    private int correctCount = 0;
    private int requiredCorrect = 0;
    private int wrongCount = 0; // Track wrong answers for retry logic

    [Header("Rules")]
    [SerializeField] private int wrongsForRetry = 3; // after 3 wrong answers, reload level

    private bool pendingRestart = false; // set before reloading so we can auto-start on the fresh scene
    private bool isWired = false;                             // guard to avoid double subscriptions



    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Initial binding on the very first scene
        RebindReferences();
        Wire();

        // Pull required target from LevelManager if available
        if (LevelManager.Instance != null && LevelManager.Instance.CurrentLevelData != null)
            requiredCorrect = LevelManager.Instance.CurrentLevelData.optimumquestioncount;
        else
            requiredCorrect = 5;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Unwire();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void RebindReferences()
    {
        if (!sceneController) sceneController   = FindFirstObjectByType<SceneController>(FindObjectsInactive.Include);
        if (!countdown)       countdown         = FindFirstObjectByType<Countdown>(FindObjectsInactive.Include);
        if (!timerController) timerController   = FindFirstObjectByType<TimerController>(FindObjectsInactive.Include);
        if (!fishSpawner)     fishSpawner       = FindFirstObjectByType<FishSpawner>(FindObjectsInactive.Include);
        if (!bubbleSpawner)   bubbleSpawner     = FindFirstObjectByType<BubbleSpawner>(FindObjectsInactive.Include);
        if (!numberAnnouncer) numberAnnouncer   = FindFirstObjectByType<NumberAnnouncer>(FindObjectsInactive.Include);
        if (!answerObserver)  answerObserver    = FindFirstObjectByType<AnswerObserver>(FindObjectsInactive.Include);
        if (!heartController) heartController   = FindFirstObjectByType<HeartController>(FindObjectsInactive.Include);

        // Refresh requiredCorrect on scene rebind if LevelManager is present
        if (LevelManager.Instance != null && LevelManager.Instance.CurrentLevelData != null)
            requiredCorrect = LevelManager.Instance.CurrentLevelData.optimumquestioncount;
    }

    private void Wire()
    {
        if (isWired) return; // guard against double subscription

        if (countdown && fishSpawner)
            countdown.OnCountdownFinished += fishSpawner.SpawnFish;

        if (fishSpawner)
            fishSpawner.OnFishSpawned += HandleFishSpawned;

        // Global gameplay events
        AnswerObserver.OnCorrectAnswer += HandleCorrectAnswer;
        AnswerObserver.OnWrongAnswer   += HandleWrongAnswer;

        isWired = true;
    }

    private void Unwire()
    {
        if (!isWired) return;

        if (countdown && fishSpawner)
            countdown.OnCountdownFinished -= fishSpawner.SpawnFish;

        if (fishSpawner)
            fishSpawner.OnFishSpawned -= HandleFishSpawned;

        AnswerObserver.OnCorrectAnswer -= HandleCorrectAnswer;
        AnswerObserver.OnWrongAnswer   -= HandleWrongAnswer;

        isWired = false;
    }

    private void Start()
    {
        if (autoStart)
        {
            StartGame();
        }
    }

    private void Update()
    {
        if (!isGameRunning || timerController == null) return;

        // Poll timer to decide time-up
        float t = timerController.GetTimer();
        if (t <= 0f)
        {
            // Time up -> decide win/lose based on requiredCorrect
            if (correctCount >= requiredCorrect) EndGameWin();
            else EndGameLose();
        }
    }

    // ===== Scene reload handling =====
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // After a reload, all scene objects are new, so re-find references and re-subscribe events
        Unwire();
        RebindReferences();
        Wire();

        // If we just retried, kick the flow again
        if (pendingRestart || autoStart)
        {
            pendingRestart = false;
            StartGame();
        }
    }

    // ========== Public API ==========

    public void StartGame()
    {
        Debug.Log("[GameManager] Starting game flow.");

        // Reset state
        isGameRunning = false;
        correctCount = 0;
        wrongCount = 0; 
        UpdateScoreUI();

        // Hide end panels if assigned
        //if (winPanel) winPanel.SetActive(false);
        //if (losePanel) losePanel.SetActive(false);

        // SceneController already draws scene in its OnEnable,
        // and SceneController triggers TimerController in its OnSceneDrawn.
        // Just ensure they are active.
        if (sceneController && !sceneController.gameObject.activeSelf)
            sceneController.gameObject.SetActive(true);

        // Countdown starts in its Start(), so ensure it's active as well.
        if (countdown && !countdown.gameObject.activeSelf)
            countdown.gameObject.SetActive(true);
    }

    public void RetryLevel()
    {
        Debug.Log("[GameManager] Retry level.");
        pendingRestart = true;
        Unwire(); // detach from old scene objects before unloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void LoadNextLevel()
    {
        if (LevelManager.Instance != null && LevelManager.Instance.CurrentLevelData != null)
        {
            int next = LevelManager.Instance.CurrentLevelData.level + 1;
            LevelManager.Instance.LoadLevel(next);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ========== Internals ==========

    private void HandleFishSpawned()
    {
        Debug.Log("[GameManager] Fish spawned. Start number announcing.");
        numberAnnouncer?.StartAnnouncing();

        // When fish is spawned and first announce begins,
        // bubbles start as well (BubbleSpawner subscribes FishSpawner in its Start).
        isGameRunning = true;
    }

    private void HandleCorrectAnswer(int newCount)
    {
        correctCount = newCount;
        UpdateScoreUI();

        // Early win: if player hits the target before time runs out
        if (correctCount >= requiredCorrect)
        {
            EndGameWin();
        }
    }

    private void HandleWrongAnswer()
    {
        // Nothing special here; hearts are already handled in AnswerObserver.
        // If you want, you can check remaining hearts to decide early lose.
        // Example (optional):
        // if (heartController && heartController.RemainingHearts <= 0) EndGameLose();

        wrongCount++;
        if (wrongCount >= wrongsForRetry)
        {
            Debug.Log($"[GameManager] Wrong answers reached {wrongCount}/{wrongsForRetry}. Retrying level...");
            RetryLevel();
        }
    }



    private void EndGameWin()
    {
        if (!isGameRunning) return;
        isGameRunning = false;
        Debug.Log("[GameManager] WIN");
        StopAllLoops();

        //if (winPanel) winPanel.SetActive(true);
        // Optionally auto-continue after a delay, or wait for UI button to call LoadNextLevel()
    }

    private void EndGameLose()
    {
        if (!isGameRunning) return;
        isGameRunning = false;
        Debug.Log("[GameManager] LOSE");
        StopAllLoops();

        //if (losePanel) losePanel.SetActive(true);
        // Optionally wait for Retry button to call RetryLevel()
    }

    private void StopAllLoops()
    {
        // Stop announcing (optional)
        // There is no Stop() in NumberAnnouncer; you can add one if needed.

        // Optionally: disable spawners or clear bubbles here.
        // BubbleSpawner already manages its tween loop in OnDestroy; 
        // if needed you can expose a public StopSpawning() in BubbleSpawner and call it here.
    }

    private void UpdateScoreUI()
    {
        // No-op for now. Plug your score UI here if needed.
    }

    public void LoseByHearts()
    {
        // Called when HeartController runs out of hearts
        EndGameLose();
    }
}
    