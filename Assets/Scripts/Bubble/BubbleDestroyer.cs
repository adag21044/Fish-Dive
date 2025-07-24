using UnityEngine;

public class BubbleDestroyer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            Debug.Log("Bubble destroyed by fish: " + other.name);
            Destroy(gameObject);
        }
    }

}