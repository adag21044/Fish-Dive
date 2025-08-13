using UnityEngine;
using System;

public class BubbleDestroyer : MonoBehaviour
{
    public event Action<Collider2D> OnBubbleHit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnBubbleHit?.Invoke(other); // Notify subscribers that a bubble has been hit
    }
}
