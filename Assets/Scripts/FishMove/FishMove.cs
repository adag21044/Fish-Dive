using UnityEngine;

public class FishMove : MonoBehaviour
{
    private Vector2 touchStartPosition;
    private Vector2 direction;
    private bool isDragging = false;
    [SerializeField] private float speed = 2.5f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPosition = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 currentPos = Input.mousePosition;
            direction = (currentPos - touchStartPosition).normalized;

            MoveFish(direction);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            direction = Vector2.zero;
        }
    }

    private void MoveFish(Vector2 direction)
    {
        Vector3 moveDir = new Vector3(direction.x, direction.y, 0f);
        transform.position += moveDir * speed * Time.deltaTime;

        if (moveDir != Vector3.zero)
        {
            transform.up = moveDir; // Rotate the fish to face the direction of movement
        }
    }


}
