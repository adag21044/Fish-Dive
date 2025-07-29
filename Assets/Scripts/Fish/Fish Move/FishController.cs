using UnityEngine;

public class FishController : MonoBehaviour
{
    [SerializeField] private FishMove fishMove;
    [SerializeField] public FishAnimator fishAnimator;
    [SerializeField] private FishSoundManager fishSoundManager;

    private void Awake()
    {
        fishMove.OnStartMoving += HandleStartMoving;
        fishMove.OnStopMoving += HandleStopMoving;
    }

    private void HandleStartMoving()
    {
        fishSoundManager.PlaySwimSound();
        fishAnimator.SetMovingState(true);
    }

    private void HandleStopMoving()
    {
        fishSoundManager.BeginFadeOut();
        fishAnimator.SetMovingState(false);
    }
} 