using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fish;
    [SerializeField] Transform spawnPoint;
    [SerializeField] private SceneDrawer sceneDrawer;

    private void Awake()
    {
        if (sceneDrawer != null)
        {
            sceneDrawer.OnSceneDrawn += SpawnFish;
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