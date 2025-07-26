using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fish;
    [SerializeField] Transform spawnPoint;
    [SerializeField] private Countdown countdown;

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
        }
        else
        {
            Debug.LogWarning("Fish or spawn point is not assigned in the FishSpawner.");
        }
    }
}