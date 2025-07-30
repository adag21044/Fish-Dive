using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public LevelDataSO CurrentLevelData { get; private set; }

    [Header("Level Seçimi (1 ile başlar)")]
    [Range(1, 11)]
    public int levelToLoad = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadLevel(levelToLoad);
    }

    public void LoadLevel(int level)
    {
        CurrentLevelData = Resources.Load<LevelDataSO>($"Level{level}");

        if (CurrentLevelData != null)
        {
            Debug.Log($"Loaded Level {level} with speed: {CurrentLevelData.speed}");

            float gameBoardHeight = 6f;
            float fishHeight = 1.0f;
            CurrentLevelData.spawninterval = CurrentLevelData.speed * (fishHeight / gameBoardHeight);
        }
        else
        {
            Debug.LogError($"Failed to load Level {level}");
        }
    }
}
