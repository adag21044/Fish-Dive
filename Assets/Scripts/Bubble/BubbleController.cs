using UnityEngine;

public class BubbleController : MonoBehaviour
{
    [SerializeField] private BubbleDestroyAnimator destroyAnimator;
    [SerializeField] private Collider2D bubbleCollider;
    [SerializeField] private BubbleSoundPlayer soundPlayer;

    private bool isPopped = false;

    public void Pop()
    {
        if (isPopped)
        {
            return;
        }    

        isPopped = true;

        bubbleCollider.enabled = false;

        if (soundPlayer != null)
        {
            soundPlayer.PlayPopSound();
        }


        if (destroyAnimator != null)
        {
            destroyAnimator.StartDestroyAnimation();
        }     
    }
}
