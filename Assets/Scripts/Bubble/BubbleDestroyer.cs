using UnityEngine;

public class BubbleDestroyer : MonoBehaviour
{
    private BubbleController bubbleController;
    private BubbleDestroyAnimator bubbleDestroyAnimator;
    
    private void Start()
    {
        bubbleDestroyAnimator = GetComponent<BubbleDestroyAnimator>();
        bubbleController = GetComponent<BubbleController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            bubbleController.Pop();
        }
    }

}