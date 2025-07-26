using UnityEngine;

public class HeartController : MonoBehaviour
{
    HeartDestroyer[] heartDestroyers;

    private void DestroyHeart(HeartDestroyer[] heartDestroyers, int index)
    {
        if (index < 0 || index >= heartDestroyers.Length)
        {
            Debug.LogError("Index out of bounds for heartDestroyers array.");
            return;
        }

        heartDestroyers[index].DestroyHeart();
    }

}