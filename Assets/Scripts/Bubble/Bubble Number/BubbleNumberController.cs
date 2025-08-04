using UnityEngine;
using TMPro;

public class BubbleNumberController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TextMeshPro text;
    public int bubbleNumber;

    private void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (text == null)
        {
            text = GetComponentInChildren<TextMeshPro>();
        }
    }

    public void Setup(Sprite sprite, int number)
    {
        bubbleNumber = number;
        spriteRenderer.sprite = sprite;
        text.text = number.ToString();
    }

    public int GetNumber() => bubbleNumber;

    public void DeactiveNumber()
    {
        text.gameObject.SetActive(false);
    }
}