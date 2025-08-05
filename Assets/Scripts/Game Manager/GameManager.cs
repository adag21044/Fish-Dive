using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private FishSpawner fishSpawner;
    [SerializeField] private NumberAnnouncer numberAnnouncer;

    
    private void Awake()
    {
        fishSpawner = FindFirstObjectByType<FishSpawner>();
        numberAnnouncer = FindFirstObjectByType<NumberAnnouncer>();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() // ✅ Burada bağla
    {
        if (fishSpawner != null && numberAnnouncer != null)
        {
            fishSpawner.OnFishSpawned += numberAnnouncer.StartAnnouncing;
        }
        else
        {
            Debug.LogError("GameManager: FishSpawner or NumberAnnouncer not assigned!");
        }
    }

    private void OnDisable()
    {
        if (fishSpawner != null && numberAnnouncer != null)
        {
            fishSpawner.OnFishSpawned -= numberAnnouncer.StartAnnouncing;
        }
    }


    public static void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    private void EndGame()
    {

    }

    private void LoadGame()
    {
        // Load game logic here
    }
}