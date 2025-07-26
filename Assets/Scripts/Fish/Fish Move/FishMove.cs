using System;
using UnityEngine;

public class FishMove : MonoBehaviour
{
    public event Action OnStartMoving;
    public event Action OnStopMoving;

    private Vector2 touchStartPosition;
    private Vector2 direction;
    private bool isDragging = false;
    private bool wasMovingLastFrame = false;

    [SerializeField] private float speed = 2.5f;
    public bool isMoving => isDragging && direction != Vector2.zero;
    private bool isTouched = false;
    public bool IsTouched => isTouched;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isTouched = true;
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

        if (isMoving != wasMovingLastFrame)
        {
            if (isMoving)
                OnStartMoving?.Invoke();
            else
                OnStopMoving?.Invoke();

            wasMovingLastFrame = isMoving;
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