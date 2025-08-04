using UnityEngine;

public class BubbleDestroyer : MonoBehaviour
{
    private BubbleController bubbleController;
    [SerializeField] private BubbleDestroyAnimator bubbleDestroyAnimator;
    [SerializeField] private BubbleNumberController bubbleNumberController;
    
    private void Start()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        bubbleDestroyAnimator = GetComponent<BubbleDestroyAnimator>();
        bubbleController = GetComponent<BubbleController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            bubbleNumberController.DeactiveNumber();
            bubbleController.Pop();
            Debug.Log(gameObject.name + " popped by fish");
        }
        else if (other.CompareTag("Destroyer"))
        {
            bubbleNumberController.DeactiveNumber();
            bubbleController.Pop();
            Debug.Log(gameObject.name + " popped by destroyer");
        }
    }
}