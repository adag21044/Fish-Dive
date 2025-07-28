using System;
using UnityEngine;

public class BubbleNumberSelector : MonoBehaviour
{
    [SerializeField] private Collider2D fishCollider;
    public event Action OnFishEnteredTrigger;
    public int SelectedBubbleNumber { get; private set; }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bubble"))
        {
            // Logic to select a bubble number when the fish enters the trigger
            Debug.Log(other.GetComponent<BubbleNumberController>().GetNumber() + " selected by fish");
            // You can add more logic here to handle the selection of bubble numbers
            SelectedBubbleNumber = other.GetComponent<BubbleNumberController>().GetNumber();
            OnFishEnteredTrigger?.Invoke();
        }
    }
}