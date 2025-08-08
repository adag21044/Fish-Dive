using UnityEngine;
using DG.Tweening;

public class MegaphoneAnimator : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private Transform megaphone;
    [SerializeField] private Transform[] arrows;

    [Header("Megaphone")]
    [SerializeField] private float baseScale   = 0.48f;   // Original size
    [SerializeField] private float maxScale    = 0.75f;   // Target size during punch
    [SerializeField] private float punchTime   = 0.15f;   // Grow / shrink time

    [Header("Arrows")]
    [SerializeField] private float arrowScaleTo = 0.75f;
    [SerializeField] private float arrowTime    = 0.25f;
    [SerializeField] private float arrowDelay   = 0.15f;

    private void Awake()
    {
        // Ensure megaphone starts at its base size
        megaphone.localScale = Vector3.one * baseScale;
    }

    private void OnEnable()
    {
        NumberAnnouncer.OnNumberAnnounced += PlayMegaphoneAnimation;
    }

    private void OnDisable()
    {
        NumberAnnouncer.OnNumberAnnounced -= PlayMegaphoneAnimation;
    }

    /// <summary>
    /// Runs whenever a number is announced: punch-scale megaphone, flash arrows.
    /// </summary>
    private void PlayMegaphoneAnimation()
    {
        // 1) Arrow flash
        foreach (Transform arrow in arrows)
        {
            arrow.gameObject.SetActive(true);
            arrow.localScale = Vector3.zero;

            arrow.DOScale(arrowScaleTo, arrowTime)
                 .SetDelay(arrowDelay)
                 .SetEase(Ease.OutSine)
                 .OnComplete(() => arrow.DOScale(0f, arrowTime)
                 .SetEase(Ease.InSine));
        }

        // 2) Megaphone punch (baseScale ➜ maxScale ➜ baseScale)
        Sequence seq = DOTween.Sequence();
        seq.Append(megaphone.DOScale(Vector3.one * maxScale, punchTime).SetEase(Ease.OutQuad));
        seq.Append(megaphone.DOScale(Vector3.one * baseScale, punchTime).SetEase(Ease.InQuad));
        seq.OnComplete(ResetMegaphone);
    }

    /// <summary>
    /// Hides arrows and guarantees the megaphone is at base size.
    /// </summary>
    private void ResetMegaphone()
    {
        megaphone.localScale = Vector3.one * baseScale;

        foreach (Transform arrow in arrows)
            arrow.gameObject.SetActive(false);
    }
}
