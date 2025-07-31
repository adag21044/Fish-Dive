using UnityEngine;

public class LevelDataLoader : MonoBehaviour
{
    public LevelDatabase levelDataBase;
    private LevelDataSO currentLevelData;
    public static bool isLevelOne = false;

    private void Start()
    {
        LoadLevelData(1); // default to level 1
        isLevelOne = true;
    }


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

    public void LoadLevelData(int levelIndex)
    {
        // Dizi 0’dan başladığı için -1
        if (levelIndex - 1 >= 0 && levelIndex - 1 < levelDataBase.levels.Length)
        {
            currentLevelData = levelDataBase.levels[levelIndex - 1];
            Debug.Log($"Loaded Level {currentLevelData.level}, Opt Q: {currentLevelData.optimumquestioncount}");
        }
        else
        {
            Debug.LogError("LevelDatabase içinde bu index yok");
        }
    }

    public LevelDataSO GetCurrentLevelData() => currentLevelData;

    public int GetLevelIndex() => currentLevelData != null ? currentLevelData.level : 0;
    public int GetGameDuration => currentLevelData != null ? (int)currentLevelData.gameDuration : 0;
}