using System;
using UnityEngine;

public class BubbleAutoDestroy : MonoBehaviour
{
    private static BubbleAutoDestroy instance;
    public static BubbleAutoDestroy Instance => instance;
    [SerializeField] private float topYLimit = 5.2f;
    public event Action OnBubbleReachedTop; 

    private void Update()
    {
        CheckDistance();
    }
    
    private void CheckDistance()
    {
        if (transform.position.y > topYLimit)
        {
            OnBubbleReachedTop?.Invoke();
        }
    }
}
