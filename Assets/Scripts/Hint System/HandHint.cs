using DG.Tweening;
using UnityEngine;

public class HandHint : MonoBehaviour
{
    [SerializeField] private GameObject handObject; // Reference to the hand object
    private Tween handTween;
    private bool hasHandShown = false;

    private void Update()
    {
        if (LevelManager.Instance.CurrentLevelData.level != 1)
        {
            HideHandHint();
            return;
        }
        if (FishMove.Instance.IsTouched && !hasHandShown)
        {
            ShowHandHint();
        }
        else
        if (FishMove.Instance.IsTouched)
        {
            HideHandHint();
        }
        else if (!FishMove.Instance.IsTouched && !hasHandShown)
        {
            ShowHandHint();
            hasHandShown = true;
        }
    }

    private void ShowHandHint()
    {
        if (handObject == null || handObject.activeSelf)
        {
            return;
        }

        handObject.SetActive(true);
        HandHintAnimation();
        Debug.Log("Hand hint shown.");
    }

    private void HideHandHint()
    {
        if (handObject == null || !handObject.activeSelf)
            return;

        handObject.SetActive(false);

        if (handTween != null && handTween.IsActive())
        {
            handTween.Kill(true);
        }
        Debug.Log("Hand hint hidden.");
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