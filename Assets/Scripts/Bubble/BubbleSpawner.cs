using UnityEngine;
using System.Collections.Generic;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] private BubbleSO[] bubbleData; // Reference to the BubbleSO scriptable object
    [SerializeField] private GameObject bubblePrefab; // Prefab for the bubble

    [SerializeField] private int minNumber = 1;
    [SerializeField] private int maxNumber = 10;

    [Header("Spawn Area")]
    [SerializeField] private float xMin = -2.3f;
    [SerializeField] private float xMax = 2.3f;
    [SerializeField] private float yMin = -3.3f;
    [SerializeField] private float yMax = 4.0f;
    [SerializeField] private float minDistanceBetweenBubbles = 1.0f;
    private List<Vector3> existingBubblePositions = new List<Vector3>();

    // Only for testing purposes
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBubble();
        }
    }


    private void SpawnBubble()
    {
        if (bubbleData == null || bubblePrefab == null)
        {
            Debug.LogWarning("BubbleSO or Prefab is missing!");
            return;
        }

        int maxAttempts = 20;
        Vector3 spawnPos = Vector3.zero;
        bool positionFound = false;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            float x = Random.Range(xMin, xMax);
            float y = Random.Range(yMin, yMax);
            spawnPos = new Vector3(x, y, 0f);

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
        bubble.transform.localScale = Vector3.one * 0.5f;

        BubbleNumberController bubbleNumberController = bubble.GetComponent<BubbleNumberController>();
        if (bubbleNumberController != null)
        {
            bubbleNumberController.Setup(selectedBubble.bubbleSprite, randomNumber);
        }

        existingBubblePositions.Add(spawnPos);
    }

    private bool IsPositionSafe(Vector3 newPos)
    {
        foreach (var pos in existingBubblePositions)
        {
            if (Vector3.Distance(pos, newPos) < minDistanceBetweenBubbles)
                return false;
        }
        return true;
    }
    
}