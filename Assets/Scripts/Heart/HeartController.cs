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

        // No hearts left -> tell GameManager we lost by hearts (do NOT reload here)
        if (nextIndex >= heartDestroyers.Length)
        {
            Debug.LogWarning("No hearts left to destroy. Notify GameManager (lose).");
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoseByHearts(); // ends the game; does not restart
            }
            return;
        }

        // Destroy this heart and advance
        heartDestroyers[nextIndex].DestroyHeart();
        nextIndex++;

        // If after destroying we ran out of hearts, notify lose as well
        if (nextIndex >= heartDestroyers.Length)
        {
            Debug.Log("All hearts destroyed. Notify GameManager (lose).");
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoseByHearts(); // still no restart here
            }
        }
    }
}
