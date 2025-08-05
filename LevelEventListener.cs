using UnityEngine;
using System;

public class LevelEventListener : MonoBehaviour
{
    [Header("Event Callbacks")]
    public UnityEngine.Events.UnityEvent<LevelDataSO> OnLevelLoadedEvent;
    public UnityEngine.Events.UnityEvent<int> OnLevelChangedEvent;
    public UnityEngine.Events.UnityEvent<bool> OnLevelOneStatusChangedEvent;

    private void OnEnable()
    {
        LevelManager.OnLevelLoaded += HandleLevelLoaded;
        LevelManager.OnLevelChanged += HandleLevelChanged;
        LevelManager.OnLevelOneStatusChanged += HandleLevelOneStatusChanged;
    }

    private void OnDisable()
    {
        LevelManager.OnLevelLoaded -= HandleLevelLoaded;
        LevelManager.OnLevelChanged -= HandleLevelChanged;
        LevelManager.OnLevelOneStatusChanged -= HandleLevelOneStatusChanged;
    }

    private void HandleLevelLoaded(LevelDataSO levelData)
    {
        OnLevelLoadedEvent?.Invoke(levelData);
    }

    private void HandleLevelChanged(int levelNumber)
    {
        OnLevelChangedEvent?.Invoke(levelNumber);
    }

    private void HandleLevelOneStatusChanged(bool isLevelOne)
    {
        OnLevelOneStatusChangedEvent?.Invoke(isLevelOne);
    }
}