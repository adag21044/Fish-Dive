using UnityEngine;
using DG.Tweening;
using System;

public class HandHider : MonoBehaviour
{
    [SerializeField] private GameObject handObject;
    
    private Tween hintTween;
    private bool isCountDownFinished = false;
    private bool hasShown = false;

    private void Awake()
    {
        handObject.SetActive(false);
        Countdown.Instance.OnCountdownFinished += HandleCountdownFinished;
    }

    private void OnEnable()
    {
        FishMove.OnFirstTouchDetected += HandleFirstTouch;
    }

    private void OnDisable()
    {
        FishMove.OnFirstTouchDetected -= HandleFirstTouch;
    }

    private void Update()
    {
        if (!isCountDownFinished || hasShown)
            return;

        if (LevelManager.Instance.CurrentLevelData.level != 1)
        {
            HideHandHint();
            return;
        }

        ShowHandHint();
    }

    private void HandleCountdownFinished()
    {
        isCountDownFinished = true;
    }

    private void HandleFirstTouch()
    {
        HideHandHint();
        hasShown = true;
    }

    private void ShowHandHint()
    {
        if (handObject.activeSelf) return;

        handObject.SetActive(true);
        StartHintAnimation();
        Debug.Log("Hint shown.");
    }

    private void HideHandHint()
    {
        if (!handObject.activeSelf) return;

        handObject.SetActive(false);

        if (hintTween != null && hintTween.IsActive())
        {
            hintTween.Kill();
        }

        Debug.Log("Hint hidden.");
    }

    private void StartHintAnimation()
    {
        Transform t = handObject.transform;

        hintTween?.Kill();

        hintTween = t.DORotate(new Vector3(30f, 0f, 0f), 0.6f, RotateMode.Fast)
                     .SetEase(Ease.InOutSine)
                     .SetLoops(-1, LoopType.Yoyo);
    }
}
