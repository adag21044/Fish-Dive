using UnityEngine;

public class BubbleMover : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Awake()
    {
        // Initialize speed with the current level data if already loaded
        if (LevelManager.Instance != null && LevelManager.Instance.CurrentLevelData != null)
        {
            speed = LevelManager.Instance.CurrentLevelData.speed;
        }
    }

    private void Update()
    {
        MoveBubble(Vector3.up, speed);
    }

    private void MoveBubble(Vector3 direction, float currentSpeed)
    {
        // Move the bubble in the specified direction at the given speed
        transform.position += direction * currentSpeed * Time.deltaTime;
    }

    private void OnEnable()
    {
        LevelManager.OnLevelLoaded += UpdateSpeedFromLevelData;
    }

    private void OnDisable()
    {
        LevelManager.OnLevelLoaded -= UpdateSpeedFromLevelData;
    }

    private void UpdateSpeedFromLevelData(LevelDataSO data)
    {
        speed = data.speed;
        Debug.Log($"[BubbleMover] Speed updated from level data: {speed}");
    }
}