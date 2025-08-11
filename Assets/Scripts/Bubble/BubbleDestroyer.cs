using UnityEngine;
using System;

public class BubbleDestroyer : MonoBehaviour
{
    private static BubbleDestroyer instance;
    public static BubbleDestroyer Instance => instance;
    public static event Action OnBubbleCollidedWithFish;
    public static event Action OnBubbleCollidedWithDestroyer;
    private BubbleController bubbleController;
    [SerializeField] private BubbleNumberController bubbleNumberController;
    
    private void Start()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        bubbleController = GetComponent<BubbleController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            OnBubbleCollidedWithFish?.Invoke();
            bubbleNumberController.DeactiveNumber();
            bubbleController.Pop();
            Debug.Log(gameObject.name + " popped by fish");
        }
        else if (other.CompareTag("Destroyer"))
        {
            OnBubbleCollidedWithDestroyer?.Invoke();
            bubbleNumberController.DeactiveNumber();
            bubbleController.Pop();
            Debug.Log(gameObject.name + " popped by destroyer");
        }
        }
}