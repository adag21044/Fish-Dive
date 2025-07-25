using UnityEngine;

public class FishAnimator : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;

    private SpriteRenderer spriteRenderer;
    private bool spriteFlag = false;
    private float timer = 0f;
    private bool isMoving = false;

    [Header("Animation Settings")]
    [SerializeField] private float switchInterval = 0.3f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!isMoving) return;

        timer += Time.deltaTime;
        if (timer >= switchInterval)
        {
            spriteFlag = !spriteFlag;
            spriteRenderer.sprite = spriteFlag ? sprite1 : sprite2;
            timer = 0f;
        }
    }

    public void SetMovingState(bool moving)
    {
        isMoving = moving;
        timer = 0f;
        spriteFlag = false;
        spriteRenderer.sprite = sprite1;
    }
}