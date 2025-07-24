using UnityEngine;

public class FishAnimator : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;

    [Header("Fish Movement")]
    [SerializeField] private FishMove fishMover;
    private SpriteRenderer spriteRenderer;

    [Header("Animation Settings")]
    [SerializeField] private float switchInterval = 0.3f;
    private float timer = 0f;
    private bool spriteFlag = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        AnimateMovement();
    }

    private void AnimateMovement()
    {
        if (fishMover != null && fishMover.isMoving)
        {
            timer += Time.deltaTime;

            if (timer >= switchInterval)
            {
                spriteFlag = !spriteFlag;
                spriteRenderer.sprite = spriteFlag ? sprite1 : sprite2;
                timer = 0f;
            }
        }
        else
        {
            timer += Time.deltaTime;

            if (timer >= switchInterval)
            {
                spriteFlag = !spriteFlag;
                spriteRenderer.sprite = spriteFlag ? sprite1 : sprite2;
                timer = 0f;
            }
        }
    }
}