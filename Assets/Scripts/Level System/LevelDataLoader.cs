using UnityEngine;

public class LevelDataLoader : MonoBehaviour
{
    private LevelDataSO levelData;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadLevelData(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadLevelData(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadLevelData(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            LoadLevelData(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            LoadLevelData(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            LoadLevelData(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            LoadLevelData(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            LoadLevelData(8);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            LoadLevelData(9);
        }
    }

    private void LoadLevelData(int i)
    {
        levelData = Resources.Load<LevelDataSO>("Level"+i);
        if (levelData != null)
        {
            Debug.Log($"Loaded Level Data: Level {levelData.level}, Optimum Questions: {levelData.optimumquestioncount}");
        }
        else
        {
            Debug.LogError("Failed to load Level Data.");
        }
    }
}