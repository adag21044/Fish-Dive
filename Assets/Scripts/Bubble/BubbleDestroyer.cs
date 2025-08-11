using UnityEngine;
using System;

public class BubbleDestroyer : MonoBehaviour
{
    // BubbleController dinleyecek
    public event Action<Collider2D> OnBubbleHit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnBubbleHit?.Invoke(other); // Hiçbir şey bilmez, sadece bildirir
    }
}
