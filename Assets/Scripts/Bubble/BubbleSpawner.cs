using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] private BubbleSO[] bubbleData; // Reference to the BubbleSO scriptable object
    [SerializeField] private GameObject bubblePrefab; // Prefab for the bubble

    [SerializeField] private int minNumber = 1;
    [SerializeField] private int maxNumber = 10;

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

        int randomNumber = Random.Range(minNumber, maxNumber + 1);

        // Instantiate bubble prefab
        GameObject bubble = Instantiate(bubblePrefab, transform.position, Quaternion.identity);

        // Set scale to 0.5 on all axes
        bubble.transform.localScale = Vector3.one * 0.5f;

        BubbleNumberController bubbleNumberController = bubble.GetComponent<BubbleNumberController>();


        if (bubbleNumberController != null)
        {
            bubbleNumberController.Setup(selectedBubble.bubbleSprite, randomNumber);
        }
        else
        {
            Debug.LogError("Bubble prefab is missing the Bubble script.");
        }

        Debug.Log($"Bubble spawned at {transform.position} with sprite: {selectedBubble.bubbleSprite.name}");

        
    }

    
}