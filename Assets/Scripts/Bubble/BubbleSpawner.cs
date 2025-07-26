using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] private BubbleSO[] bubbleData; // Reference to the BubbleSO scriptable object
    [SerializeField] private GameObject bubblePrefab; // Prefab for the bubble

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

        int index = Random.Range(0, bubbleData.Length);
        BubbleSO selectedBubble = bubbleData[index];

        // Instantiate bubble prefab
        GameObject bubble = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
        SpriteRenderer renderer = bubble.GetComponent<SpriteRenderer>();

        if (renderer != null)
        {
            renderer.sprite = selectedBubble.bubbleSprite;
        }
        else
        {
            Debug.LogWarning("Bubble prefab does not have a SpriteRenderer component.");
        }

        Debug.Log($"Bubble spawned at {transform.position} with sprite: {selectedBubble.bubbleSprite.name}");
    }
}