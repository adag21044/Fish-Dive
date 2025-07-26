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

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing on the FishMove GameObject.");
        }
    }

    private void Update()
    {
        // Input her zaman Update() içinde alınır
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
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            direction = Vector2.zero;
        }

        if (isMoving != wasMovingLastFrame)
        {
            if (isMoving) OnStartMoving?.Invoke();
            else OnStopMoving?.Invoke();

            wasMovingLastFrame = isMoving;
        }
    }

    private void FixedUpdate()
    {
        if (direction != Vector2.zero)
        {
            MoveFish(direction);
        }
    }

    private void MoveFish(Vector2 direction)
    {
        Vector2 moveDir = direction.normalized;
        Vector2 targetPos = rb.position + moveDir * speed * Time.fixedDeltaTime; // ✅ FIXED

        rb.MovePosition(targetPos);

        if (moveDir != Vector2.zero)
        {
            transform.up = moveDir;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) // ✅ DOĞRU METOT BU
    {
        if (other.collider.CompareTag("Border"))
        {
            Debug.Log("Fish collided with border!");
        }
    }
}
