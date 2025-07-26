using DG.Tweening;
using UnityEngine;

public class HandHint : MonoBehaviour
{
    [SerializeField] private GameObject handObject; // Reference to the hand object
    [SerializeField] private FishMove fishMove; // Reference to the FishMove script
    private Tween handTween;

    private void Update()
    {
        if (!fishMove.IsTouched )
        {
            Debug.Log("Showing hand hint");
            ShowHandHint();
        }
        else
        {
            HideHandHint();
        }
    }

    private void ShowHandHint()
    {
        if (handObject == null)
        {
            return;
        }

        if (!handObject.activeSelf)
        {
            handObject.SetActive(true);
            HandHintAnimation();
            Debug.Log("Hand hint shown.");
        }
        else
        {
            // Restart the animation if it's already active
            if (handTween == null || !handTween.IsActive())
            {
                HandHintAnimation();
            }
        }
    }

    private void HideHandHint()
    {
        if (handObject == null)
        {
            return;
        }
        
        if (handObject.activeSelf)
        {
            handObject.SetActive(false);

            if (handTween != null && handTween.IsActive())
            {
                handTween.Kill(true);
            }
            Debug.Log("Hand hint hidden.");
        }
    }

    private void HandHintAnimation()
    {
        Transform t = handObject.transform;

        // Başlangıç açısı 0, hedef açı (30, 0, 0)
        handTween = t.DORotate(new Vector3(30f, 0f, 0f), 0.6f, RotateMode.Fast)
                    .SetEase(Ease.InOutSine)
                    .SetLoops(-1, LoopType.Yoyo);
    }
}