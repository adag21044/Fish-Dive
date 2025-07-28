using UnityEngine;

public class BubbleMover : MonoBehaviour
{
    // TODO: Create safe destination distance between bubbles
    public void MoveBubble(Vector3 direction, float speed)
    {
        // Move the bubble in the specified direction at the given speed
        transform.position += direction * speed * Time.deltaTime;
    }
}