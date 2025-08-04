using System;
using UnityEngine;

public class BubbleAutoDestroy : MonoBehaviour
{
    [SerializeField] private float topYLimit = 5.2f;
    [SerializeField] private BubbleController bubbleController;
      

    private void OnEnable()
    {
        BubbleController.OnBubbleReachedTop += DestroyBubble;
    }

    private void Update()
    {
        CheckDistance();
    }

    private void DestroyBubble()
    {
        bubbleController.DisappearAtTop();
    }

    private void CheckDistance()
    {
        if (transform.position.y > topYLimit)
        {
            bubbleController.DisappearAtTop();
        }
    }
}
