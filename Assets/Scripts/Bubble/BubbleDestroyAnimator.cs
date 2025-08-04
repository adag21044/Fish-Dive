using System.Collections;
using UnityEngine;

public class BubbleDestroyAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] destroyFrames;
    [SerializeField] private float frameInterval = 0.1f;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        InitializeSpriteRenderer();
    }

    private void InitializeSpriteRenderer()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void StartDestroyAnimation()
    {
        StartCoroutine(PlayDestroyAnimation());
    }

    private IEnumerator PlayDestroyAnimation()
    {
        foreach (Sprite frame in destroyFrames)
        {
            spriteRenderer.sprite = frame;
            yield return new WaitForSeconds(frameInterval);
        }
        Destroy(gameObject); // Destroy the bubble after the animation
    }
}