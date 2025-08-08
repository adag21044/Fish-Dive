using UnityEngine;

public class HeartController : MonoBehaviour
{
    [SerializeField] private HeartDestroyer[] heartDestroyers;
    private int nextIndex = 0;

    public void DestroyHeart(int index)
    {
        if (index < 0 || index >= heartDestroyers.Length)
        {
            Debug.LogWarning("Invalid heart index: " + index);
            return;
        }

        if (heartDestroyers[index] == null)
        {
            Debug.LogWarning("Heart at index is null.");
            return;
        }

        heartDestroyers[index].DestroyHeart();
    }

    // Destroy next available heart
    public void DestroyNextHeart()
    {
        // Find next non-null, active heart
        while (nextIndex < heartDestroyers.Length &&
               (heartDestroyers[nextIndex] == null ||
                !heartDestroyers[nextIndex].gameObject.activeInHierarchy))
        {
            nextIndex++;
        }

        if (nextIndex >= heartDestroyers.Length)
        {
            Debug.LogWarning("No hearts left to destroy.");
            return;
        }

        heartDestroyers[nextIndex].DestroyHeart();
        nextIndex++;

        if (nextIndex >= heartDestroyers.Length)
        {
            GameManager.StartGame(); // End game if all hearts are destroyed
        }
    }
}
