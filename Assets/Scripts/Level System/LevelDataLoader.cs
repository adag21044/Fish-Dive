using UnityEngine;

public class LevelDataLoader : MonoBehaviour
{
    private LevelDataSO levelData;
    private static bool isLevelOne = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadLevelData(1);
            isLevelOne = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadLevelData(2);
            isLevelOne = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadLevelData(3);
            isLevelOne = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            LoadLevelData(4);
            isLevelOne = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            LoadLevelData(5);
            isLevelOne = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            LoadLevelData(6);
            isLevelOne = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            LoadLevelData(7);
            isLevelOne = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            LoadLevelData(8);
            isLevelOne = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            LoadLevelData(9);
            isLevelOne = false;
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