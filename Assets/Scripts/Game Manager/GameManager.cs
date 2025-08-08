using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private FishSpawner fishSpawner;
    [SerializeField] private NumberAnnouncer numberAnnouncer;
    [SerializeField] private Countdown countdown;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (fishSpawner == null)
        {
            Debug.LogError("FishSpawner not assigned in GameManager!");  
        }

        if (numberAnnouncer == null)
        {
           Debug.LogError("NumberAnnouncer not assigned in GameManager!"); 
        }

        if (countdown == null)
        {
            Debug.LogError("Countdown not assigned in GameManager!");
        }
        
        if (countdown != null)
        {
            countdown.OnCountdownFinished += fishSpawner.SpawnFish; // ✅ fish spawn when countdown finishes
        }
        else
        {
            Debug.LogError("Countdown not assigned in GameManager!");
        }
    }

    private void Start()
    {
        if (fishSpawner != null )
        {
            fishSpawner.OnFishSpawned += OnFishSpawned;
        }
        else
        {
            Debug.LogError("FishSpawner or NumberAnnouncer not assigned!");
        }
    }

    private void OnFishSpawned()
    {
        Debug.Log("[GameManager] Fish spawn tamamlandı. Sayı duyuruluyor...");
        numberAnnouncer?.StartAnnouncing(); // 🔊 sayı duyurusu başlasın
    }

    private void OnDisable()
    {
        if (fishSpawner != null && numberAnnouncer != null)
        {
            fishSpawner.OnFishSpawned -= numberAnnouncer.StartAnnouncing;
        }

        if (countdown != null)
        {
            countdown.OnCountdownFinished -= fishSpawner.SpawnFish;
        }
    }

    public static void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    private void EndGame()
    {
        // implement end logic
    }
}
