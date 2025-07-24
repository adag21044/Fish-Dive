using System.Collections.Generic;
using UnityEngine;

public class LevelDataTester : MonoBehaviour
{
    void Start()
    {
        List<LevelData> levels = FileHandler.ReadListFromResources<LevelData>("level_data");

        if (levels.Count == 0)
        {
            Debug.LogError("No level data found or failed to load JSON.");
            return;
        }

        foreach (var level in levels)
        {
            Debug.Log($"Level: {level._level}, Questions: {level.optimumquestioncount}, " +
                      $"Min: {level.minnumber}, Max: {level.maxnumber}, " +
                      $"SpawnCount: {level.spawncount}, Speed: {level.speed}");
        }
    }
}
