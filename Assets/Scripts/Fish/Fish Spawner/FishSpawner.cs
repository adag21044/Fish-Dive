using UnityEngine;
using DG.Tweening;
using System;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fish;
    [SerializeField] Transform spawnPoint;
    public event Action OnFishSpawned; // 🎯 trigger when fish spawn
    [SerializeField] private Timer timer;
    
    public void SpawnFish() // artık dışarıdan çağrılabilir
    {
        if (fish != null && spawnPoint != null)
        {
            fish.SetActive(true);
            Debug.Log("Fish spawned at: " + spawnPoint.position);

            SpriteRenderer renderer = fish.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                Color color = renderer.color;
                color.a = 0f;
                renderer.color = color;

                renderer.DOFade(1f, 1f).SetEase(Ease.OutQuad).OnComplete(() =>
                {
                    OnFishSpawned?.Invoke(); // 🎯 balık animasyonu bitince event
                });
            }
            else
            {
                OnFishSpawned?.Invoke(); // fallback
            }
        }
        else
        {
            Debug.LogWarning("Fish or spawn point is not assigned in the FishSpawner.");
        }

        if (timer != null)
        {
            timer.StartTimer(); // ✅ oyun süresi başlasın
            Debug.Log("Timer started after fish spawn.");
        }
    }

}