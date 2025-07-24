using UnityEngine;

public class BubbleDestroyer : MonoBehaviour
{
    private BubbleDestroyAnimator bubbleDestroyAnimator;

    private void Start()
    {
        bubbleDestroyAnimator = GetComponent<BubbleDestroyAnimator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            Debug.Log("Bubble destroyed by fish: " + other.name);
            bubbleDestroyAnimator.StartDestroyAnimation();
            GetComponent<Collider2D>().enabled = false; // Disable the collider to prevent multiple triggers
        }
    }

}