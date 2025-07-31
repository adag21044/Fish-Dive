using UnityEngine;
using DG.Tweening;
using System;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fish;
    [SerializeField] Transform spawnPoint;
    [SerializeField] private Countdown countdown;
    public event Action OnFishSpawned;

    private void Awake()
    {
        if (countdown != null)
        {
            countdown.OnCountdownFinished += SpawnFish;
        }
    }

    private void SpawnFish()
    {
        if (fish != null && spawnPoint != null)
        {
            fish.SetActive(true);
            Debug.Log("Fish spawned at: " + spawnPoint.position);

            SpriteRenderer renderer = fish.GetComponent<SpriteRenderer>();

            if (renderer != null)
            {
                Color color = renderer.color;
                color.a = 0f; // Ensure fish is fully invisible at start
                renderer.color = color;

                renderer.DOFade(1f, 1f).SetEase(Ease.OutQuad).OnComplete(() =>
                {
                    OnFishSpawned?.Invoke(); // 🎯 Event trigged
                });
            }
            else
            {
                OnFishSpawned?.Invoke(); // fallback
            }
            
            Debug.Log("Fish spawned at: " + spawnPoint.position);
        }
        else
        {
            Debug.LogWarning("Fish or spawn point is not assigned in the FishSpawner.");
        }
    }
}