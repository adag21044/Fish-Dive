using UnityEngine;
using DG.Tweening;
using System;

public class BubbleController : MonoBehaviour
{
    public static event Action<BubbleController, int> OnAnyBubbleSpawned;   // (self, number)
    public static event Action<BubbleController> OnAnyBubbleDestroyed;

    [Header("Components & Dependencies")]
    [SerializeField] private BubbleDestroyAnimator destroyAnimator;
    [SerializeField] private Collider2D bubbleCollider;
    [SerializeField] private BubbleSoundPlayer soundPlayer;
    [SerializeField] private BubbleNumberController numberController; // 🔹 referans buraya taşındı

    private BubbleSpawner bubbleSpawner;
    private BubbleAutoDestroy bubbleAutoDestroy;
    private Vector2 spawnPosition;
    private bool isPopped = false;

    private BubbleDestroyer bubbleDestroyer; 
    private bool spawnEventRaised = false;
    private bool destroyEventRaised = false;

    private void Awake()
    {
        // Auto-destroy event
        bubbleAutoDestroy = GetComponent<BubbleAutoDestroy>();
        if (bubbleAutoDestroy != null)
            bubbleAutoDestroy.OnBubbleReachedTop += HandleAutoDestroy;

        // Destroyer trigger event
        bubbleDestroyer = GetComponent<BubbleDestroyer>();
        if (bubbleDestroyer != null)
            bubbleDestroyer.OnBubbleHit += HandleBubbleHit;

        // Güvenlik: inspector atanmamışsa kendini bul
        if (numberController == null)
            numberController = GetComponent<BubbleNumberController>();
    }

    private void Start()
    {
        // 🔔 Publish "spawned" after Setup() has already run (Spawner calls Setup before Start).
        if (!spawnEventRaised)
        {
            int num = numberController != null ? numberController.bubbleNumber : -1;
            OnAnyBubbleSpawned?.Invoke(this, num);
            spawnEventRaised = true;
        }
    }

    private void OnDestroy()
    {
        if (bubbleAutoDestroy != null)
            bubbleAutoDestroy.OnBubbleReachedTop -= HandleAutoDestroy;
        if (bubbleDestroyer != null)
            bubbleDestroyer.OnBubbleHit -= HandleBubbleHit;
    }

    private void HandleAutoDestroy()
    {
        DisappearAtTop();
    }

    // 🔸 Çarpışma artık burada yönetiliyor
    private void HandleBubbleHit(Collider2D other)
    {
        if (isPopped) return;

        // İhtiyaca göre genişletilebilir
        if (other.CompareTag("Fish") || other.CompareTag("Destroyer"))
        {
            // Number’ı kapatmayı Pop içine de alabiliriz; burada açık yazıyorum:
            numberController?.DeactiveNumber();
            Pop();
            Debug.Log($"{gameObject.name} popped by {other.tag}");
        }
    }

    public void Pop()
    {
        if (isPopped) return;
        isPopped = true;

        if (bubbleCollider != null)
            bubbleCollider.enabled = false;

        if (soundPlayer != null)
            soundPlayer.PlayPopSound();

        if (destroyAnimator != null)
            destroyAnimator.StartDestroyAnimation();

        if (!destroyEventRaised)
        {
            OnAnyBubbleDestroyed?.Invoke(this);
            destroyEventRaised = true;
        }    

        if (bubbleSpawner != null)
            bubbleSpawner.FreePosition(spawnPosition);
    }

    public void Initialize(Vector2 position, BubbleSpawner spawner)
    {
        spawnPosition = position;
        bubbleSpawner = spawner;
    }

    public void DisappearAtTop(float fadeDuration = 0.4f)
    {
        if (isPopped) return;
        isPopped = true;

        if (bubbleCollider != null)
            bubbleCollider.enabled = false;

        Sequence seq = DOTween.Sequence();

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            seq.Join(sr.DOFade(0f, fadeDuration));

        seq.Join(transform.DOScale(Vector3.zero, fadeDuration).SetEase(Ease.OutSine));

        seq.OnComplete(() =>
        {
            if (!destroyEventRaised)
            {
                OnAnyBubbleDestroyed?.Invoke(this);
                destroyEventRaised = true;
            }

            if (bubbleSpawner != null)
                bubbleSpawner.FreePosition(spawnPosition);

            Destroy(gameObject);
        });
    }
}
