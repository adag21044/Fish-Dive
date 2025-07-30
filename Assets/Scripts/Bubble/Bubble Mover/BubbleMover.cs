using UnityEngine;

public class BubbleMover : MonoBehaviour
{
    [SerializeField] private LevelDatabase levelData;
    [SerializeField] private float speed;
    [SerializeField] private LevelDataLoader levelDataLoader;

    private void Awake()
    {
        if (levelDataLoader == null)
        levelDataLoader = FindFirstObjectByType<LevelDataLoader>();

        if (levelDataLoader == null)
        {
            Debug.LogError("LevelDataLoader bulunamadı!");
            return;
        }

        speed = levelData.levels[levelDataLoader.GetLevelIndex()].speed;
        Debug.Log($"Loaded level data for level {levelDataLoader.GetLevelIndex()} with speed: {speed}");
        Debug.Log($"Bubble speed set to: {speed}");
    }

    private void Update()
    {
        MoveBubble(Vector3.up, speed);
    }

    // TODO: Create safe destination distance between bubbles

    private void MoveBubble(Vector3 direction, float speed)
    {
        // Move the bubble in the specified direction at the given speed
        transform.position += direction * speed * Time.deltaTime;
    }
}