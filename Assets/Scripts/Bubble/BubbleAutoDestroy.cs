using System;
using UnityEngine;

public class BubbleAutoDestroy : MonoBehaviour
{
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
