using UnityEngine;
using DG.Tweening;

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

    [Header("Shake Settings")]
    [SerializeField] private float shakeDuration = 0.4f;
    [SerializeField] private float shakeStrength = 4f;
    private Vector3 originalPos;
    public bool isShaking { get; private set; }


    private void Start()
    {
        SetAnimator();
    }

    private void SetAnimator()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalPos = transform.localPosition;
    }

    private void Update()
    {
        if (!isMoving) return;
        AnimateMove(); 
    }

    private void AnimateMove()
    {
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

    public void PlayShake()
    {
        Debug.Log("Playing shake animation on fish.");


        transform.DOKill();


        transform.DOShakeRotation(
            shakeDuration, strength: new Vector3(0, 0, shakeStrength * 100), vibrato: 5, randomness: 0, fadeOut: true)
         .SetEase(Ease.InOutSine)
         .OnComplete(() => {
        transform.localRotation = Quaternion.identity;
        isShaking = false;
    });
         
    }
}