using UnityEngine;
using System;

public class LevelDataLoader : MonoBehaviour
{
    private LevelDataSO currentLevelData;
    public static bool isLevelOne = false;
    
    // Keep the old event for backward compatibility if other systems depend on it
    public static event Action OnLevelDataChanged;

    private void OnEnable()
    {
        // Subscribe to LevelManager events
        LevelManager.OnLevelLoaded += HandleLevelLoaded;
        LevelManager.OnLevelOneStatusChanged += HandleLevelOneStatus;
    }

    private void OnDisable()
    {
        // Unsubscribe from LevelManager events
        LevelManager.OnLevelLoaded -= HandleLevelLoaded;
        LevelManager.OnLevelOneStatusChanged -= HandleLevelOneStatus;
    }

    private void HandleLevelLoaded(LevelDataSO levelData)
    {
        currentLevelData = levelData;
        Debug.Log($"LevelDataLoader: Level {currentLevelData.level} loaded, Opt Q: {currentLevelData.optimumquestioncount}");
        
        // Trigger the old event for backward compatibility
        OnLevelDataChanged?.Invoke();
    }

    private void HandleLevelOneStatus(bool levelOneStatus)
    {
        isLevelOne = levelOneStatus;
    }

    // Keep these methods for backward compatibility
    public LevelDataSO GetCurrentLevelData() => currentLevelData;
    public int GetLevelIndex() => currentLevelData != null ? currentLevelData.level : 0;
    public int GetGameDuration => currentLevelData != null ? (int)currentLevelData.gameDuration : 0;
}