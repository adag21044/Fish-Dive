using UnityEngine;
using DG.Tweening;
using System;

public class BubbleController : MonoBehaviour
{
    [SerializeField] private BubbleDestroyAnimator destroyAnimator;
    [SerializeField] private Collider2D bubbleCollider;
    [SerializeField] private BubbleSoundPlayer soundPlayer;

    private BubbleSpawner bubbleSpawner;
    private BubbleAutoDestroy bubbleAutoDestroy;
    private Vector2 spawnPosition;

    private bool isPopped = false;
    
    private void Awake()
    {
        bubbleAutoDestroy = GetComponent<BubbleAutoDestroy>();

        if (bubbleAutoDestroy != null)
        {
            bubbleAutoDestroy.OnBubbleReachedTop += HandleAutoDestroy;
        }
            
    }

    private void HandleAutoDestroy()
    {
        DisappearAtTop(); 
    }
        
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

        if (bubbleSpawner != null)
        {
            bubbleSpawner.FreePosition(spawnPosition);
        }
    }

    public void Initialize(Vector2 position, BubbleSpawner spawner)
    {
        spawnPosition = position;
        bubbleSpawner = spawner;
    }
    
    public void DisappearAtTop(float fadeDuration = 0.4f)
    {
        if (isPopped) return; // Zaten patladıysa animasyon oynama

        

        isPopped = true;
        bubbleCollider.enabled = false;

        // Ölçek küçülerek ve şeffaflaşarak yok olma
        Sequence seq = DOTween.Sequence();

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            seq.Join(sr.DOFade(0f, fadeDuration));
        }

        seq.Join(transform.DOScale(Vector3.zero, fadeDuration).SetEase(Ease.OutSine));

        seq.OnComplete(() =>
        {
            if (bubbleSpawner != null)
            {
                bubbleSpawner.FreePosition(spawnPosition);
            }

            Destroy(gameObject);
        });
    }
}