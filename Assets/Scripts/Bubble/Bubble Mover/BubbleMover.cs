using UnityEngine;

public class BubbleMover : MonoBehaviour
{
    [SerializeField] private LevelDatabase levelData;
    [SerializeField] private float speed;

    private void Awake()
    {
        if (LevelDataLoader.Instance == null)
        {
            Debug.LogError("LevelDataLoader instance not found. Please ensure it is present in the scene.");
            return;
        }

        SetSpeed(levelData.levels[LevelDataLoader.Instance.GetLevelIndex()].speed);

        Debug.Log($"Loaded level data for level {LevelDataLoader.Instance.GetLevelIndex()} with speed: {speed}");
        Debug.Log($"Bubble speed set to: {speed}");
    }

    private void Update()
    {
        MoveBubble(Vector3.up, speed);
    }

    private void MoveBubble(Vector3 direction, float speed)
    {
        // Move the bubble in the specified direction at the given speed
        transform.position += direction * speed * Time.deltaTime;
    }

    private void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void OnEnable()
    {
        LevelDataLoader.OnLevelDataChanged += UpdateSpeedFromLevelData;
    }

    private void OnDisable()
    {
        LevelDataLoader.OnLevelDataChanged -= UpdateSpeedFromLevelData;
    }

    private void UpdateSpeedFromLevelData()
    {
        speed = levelData.levels[LevelDataLoader.Instance.GetLevelIndex()].speed;
        Debug.Log($"[BubbleMover] Speed updated from level data: {speed}");
    }
}