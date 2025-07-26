using UnityEngine;
using DG.Tweening;

public class HandHider : MonoBehaviour
{
    [SerializeField] private GameObject handObject; // Reference to the hand object
    [SerializeField] private FishMove fishMove; // Reference to the FishMove script

    private Tween hintTween;
    private bool isCountDownFinished = false;

    private void Awake()
    {
        handObject.SetActive(false); // Ensure the hand object is initially hidden

        Countdown.Instance.OnCountdownFinished += HandleCountdownFinished;
    }

    private void Update()
    {
        if (!isCountDownFinished)
        {
            return;
        }

        if (!fishMove.IsTouched)
        {
            ShowHandHint();
        }
        else
        {
            HideHandHint();
        }

    }

    private void HandleCountdownFinished()
    {
        isCountDownFinished = true;
        ShowHandHint();
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
            Debug.Log("Hand hint hidden.");
        }
    }

    private void ShowHandHint()
    {
        if (handObject.activeSelf) return;

        handObject.SetActive(true);
        StartHintAnimation();
    }
    
    private void StartHintAnimation()
    {
        Transform t = handObject.transform;

        // Eski tween'i temizle
        hintTween?.Kill();

        // 0°  ↔  30° X-rotasyon
        hintTween = t.DORotate(new Vector3(30f, 0f, 0f), 0.6f, RotateMode.Fast)
                     .SetEase(Ease.InOutSine)
                     .SetLoops(-1, LoopType.Yoyo);
    }
}