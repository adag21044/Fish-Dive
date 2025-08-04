using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] public BubbleSO[] bubbleData; // Reference to the BubbleSO scriptable object
    [SerializeField] private GameObject bubblePrefab;
    public GameObject BubblePrefab => bubblePrefab; // getter
    [SerializeField] private int minNumber = 1;
    [SerializeField] private int maxNumber = 10;

    [Header("Spawn Area")]
    [SerializeField] private float xMin = -1.72f;
    [SerializeField] private float xMax = 1.72f;
    [SerializeField] private float yMin = -2.75f;
    [SerializeField] private float yMax = 3.9f;
    [SerializeField] private float minDistanceBetweenBubbles = 1.0f;
    [SerializeField] private float spawnRate = 10.0f; // Spawn interval in seconds
    private List<Vector2> existingBubblePositions = new List<Vector2>();
    public List<GameObject> spawnedBubbles = new List<GameObject>();

    private void Start()
    {
        SubscribeToFishSpawner();
        LoadLevelParameters();
        SpawnInitialBubblesFromLevelData();
    }

    // Only for testing purposes
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBubble();
        }
    }

    private void SubscribeToFishSpawner()
    {
        var fishSpawner = FindFirstObjectByType<FishSpawner>();
        if (fishSpawner != null)
        {
            fishSpawner.OnFishSpawned += BeginBubbleSpawning;
        }
        else
        {
            Debug.LogWarning("FishSpawner not found in the scene.");
        }
    }

    private void LoadLevelParameters()
    {
        var data = LevelManager.Instance.CurrentLevelData;
        minNumber = data.minnumber;
        maxNumber = data.maxnumber;
        spawnRate = data.spawninterval;
    }

    private void BeginBubbleSpawning()
    {
        if (spawnLoopTween != null && spawnLoopTween.IsActive())
            spawnLoopTween.Kill();

        // İlk baloncuğu hemen spawn’la (isterseniz bu satırı kaldırabilirsiniz)
        SpawnBubble();

        // Ardından döngüyü başlat
        StartSpawnLoop();
    }

    private void StartSpawnLoop()
    {
        Debug.Log($"Spawning bubble after {spawnRate} seconds");

        spawnLoopTween = DOVirtual.DelayedCall(spawnRate, () =>
        {
            SpawnBubble();
            StartSpawnLoop();
        }, false);
    }

    private void SpawnInitialBubbles(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnBubble();
        }
    }

    private Tween spawnLoopTween;

    private void SpawnBubblesSequentially(int count)
    {
        float delayBetweenEach = 0.4f; // balık boyu kadar zaman

        for (int i = 0; i < count; i++)
        {
            float delay = i * delayBetweenEach;

            DOVirtual.DelayedCall(delay, () =>
            {
                SpawnBubble();
            });
        }
    }

    private void SpawnInitialBubblesFromLevelData()
    {
        int initialCount = LevelManager.Instance.CurrentLevelData.bubblecount;
        SpawnInitialBubbles(initialCount);
    }

    private void OnDestroy()
    {
        if (spawnLoopTween != null && spawnLoopTween.IsActive())
            spawnLoopTween.Kill();
    }

    private void SpawnBubble()
    {
        if (bubbleData == null || bubblePrefab == null)
        {
            Debug.LogWarning("BubbleSO or Prefab is missing!");
            return;
        }

        float bubbleRadius = 1.1f;
        int maxAttempts = 20;
        Vector2 spawnPos = Vector2.zero;
        bool positionFound = false;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            float x = Random.Range(xMin + bubbleRadius, xMax - bubbleRadius);
            float y = Random.Range(yMin + bubbleRadius, yMax) - bubbleRadius;
            spawnPos = new Vector2(x, y);

            if (IsPositionSafe(spawnPos))
            {
                positionFound = true;
                break;
            }
        }

        if (!positionFound)
        {
            Debug.LogWarning("Could not find safe position for new bubble.");
            return;
        }

        int index = Random.Range(0, bubbleData.Length);
        BubbleSO selectedBubble = bubbleData[index];
        int randomNumber = Random.Range(minNumber, maxNumber + 1);

        GameObject bubble = Instantiate(bubblePrefab, spawnPos, Quaternion.identity);

        // Set the bubble's scale to zero initially
        bubble.transform.localScale = Vector3.zero;

        // Animate the bubble to its full size
        bubble.transform.DOScale(0.5f, 0.35f)
            .SetEase(Ease.InSine);

        BubbleNumberController bubbleNumberController = bubble.GetComponent<BubbleNumberController>();
        if (bubbleNumberController != null)
        {
            bubbleNumberController.Setup(selectedBubble.bubbleSprite, randomNumber);
        }
        BubbleController bubbleController = bubble.GetComponent<BubbleController>();
        if (bubbleController != null)
        {
            bubbleController.Initialize(spawnPos, this);
        }

        existingBubblePositions.Add(spawnPos);

        DOVirtual.DelayedCall(3f, () =>
        {
            FreePosition(spawnPos);
        });
        spawnedBubbles.Add(bubble);
        FindFirstObjectByType<DelayedCorrectBubbleSpawner>()?.NotifyBubbleSpawned();
    }

    private bool IsPositionSafe(Vector2 newPos)
    {
        foreach (var pos in existingBubblePositions)
        {
            if (Vector2.Distance(pos, newPos) < minDistanceBetweenBubbles)
                return false;
        }
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(
            new Vector3((xMin + xMax) / 2f, (yMin + yMax) / 2f, 0f),
            new Vector3(xMax - xMin, yMax - yMin, 0.1f)
        );
    }

    public void FreePosition(Vector2 position)
    {
        existingBubblePositions.Remove(position);
    }   
}