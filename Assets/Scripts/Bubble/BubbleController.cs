using UnityEngine;

public class BubbleController : MonoBehaviour
{
    [SerializeField] private BubbleDestroyAnimator destroyAnimator;
    [SerializeField] private Collider2D bubbleCollider;
    [SerializeField] private BubbleSoundPlayer soundPlayer;

    private BubbleSpawner bubbleSpawner;
    private Vector2 spawnPosition;

    private bool isPopped = false;

    public void Pop()
    {
        if (isPopped)
        {
            return;
        }

        isPopped = true;

        bubbleCollider.enabled = false;

        if (soundPlayer != null)
        {
            soundPlayer.PlayPopSound();
        }


        if (destroyAnimator != null)
        {
            destroyAnimator.StartDestroyAnimation();
        }

        if(bubbleSpawner != null)
        {
            bubbleSpawner.FreePosition(spawnPosition);
        }
    }
    
    public void Initialize(Vector2 position, BubbleSpawner spawner)
    {
        spawnPosition = position;
        bubbleSpawner = spawner;
    }
}
