using System;
using DG.Tweening;
using UnityEngine;

public class MegaphoneAnimator : MonoBehaviour
{
    [SerializeField] private Transform megaphone;
    [SerializeField] private Transform[] arrows; // to visualize sound

    [SerializeField] private float punchScale = 0.2f;
    [SerializeField] private float punchDuration = 0.3f;

    [SerializeField] private float arrowScaleTo = 1.5f;
    [SerializeField] private float arrowFadeDuration = 0.6f;
    [SerializeField] private float arrowDelay = 0.15f;

    private Sequence[] arrowSequences;

    private void Awake()
    {
        arrowSequences = new Sequence[arrows.Length];   
    }

    public void PlayMegaphoneAnimation()
    {
        Debug.Log("Playing megaphone animation");

        foreach (Transform arrow in arrows)
        {
            arrow.gameObject.SetActive(true);
        }

        // Play animation on megaphone
        megaphone.localScale = Vector3.one;
        megaphone.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 0.1f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    megaphone.DOScale(new Vector3(0.48f, 0.48f, 0.48f), 0.1f);
                });
    }

    public void StopMegaphone()
    {
        foreach (Transform arrow in arrows)
        {
            arrow.gameObject.SetActive(false);
        }
    }
} 