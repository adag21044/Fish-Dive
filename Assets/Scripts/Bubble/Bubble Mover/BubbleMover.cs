using UnityEngine;

public class BubbleMover : MonoBehaviour
{

    [SerializeField] private float speed = 2.5f;
    private void Update()
    {
        MoveBubble(Vector3.up, speed);
    }

    // TODO: Create safe destination distance between bubbles

    private void MoveBubble(Vector3 direction, float speed)
    {
        // Move the bubble in the specified direction at the given speed
        transform.position += direction * speed * Time.deltaTime;
    }
}