using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fish;
    [SerializeField] Transform spawnPoint;

    private void Awake()
    {
        SpawnFish();
    }

    private void SpawnFish()
    {
        if (fish != null && spawnPoint != null)
        {
            fish.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Fish or spawn point is not assigned in the FishSpawner.");
        }
    }
}