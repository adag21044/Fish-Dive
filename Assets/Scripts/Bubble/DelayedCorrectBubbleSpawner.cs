using UnityEngine;
using System;

public class DelayedCorrectBubbleSpawner : MonoBehaviour
{
    // BubbleSpawner listens this and spawns the correct bubble immediately
    public static event Action<int> OnForceCorrectBubble;

    [SerializeField] private int delayCount = 3;
    private int delayCounter = 0;
    private bool correctBubbleSpawned = false;

    private void OnEnable()
    {
        NumberAnnouncer.OnNumberAnnounced += ResetLogic;
        BubbleController.OnAnyBubbleSpawned += HandleBubbleSpawned;
        BubbleController.OnAnyBubbleDestroyed += HandleBubbleDestroyed; // optional
    }

    private void OnDisable()
    {
        NumberAnnouncer.OnNumberAnnounced -= ResetLogic;
        BubbleController.OnAnyBubbleSpawned -= HandleBubbleSpawned;
        BubbleController.OnAnyBubbleDestroyed -= HandleBubbleDestroyed;
    }

    private void Start()
    {
        delayCount = GetDelayCount();
    }

    private int GetDelayCount()
    {
        var data = LevelManager.Instance.CurrentLevelData;
        return UnityEngine.Random.Range(data.mindelaycount, data.maxdelaycount + 1);
    }

    private void HandleBubbleSpawned(BubbleController bubble, int numberOnBubble)
    {
        if (correctBubbleSpawned) return;

        // Stop if correct bubble already appeared naturally
        if (numberOnBubble == NumberAnnouncer.announcedNumber)
        {
            correctBubbleSpawned = true;
            return;
        }

        // Count wrong spawns
        delayCounter++;

        // Threshold reached -> request a correct bubble
        if (delayCounter >= delayCount)
        {
            Debug.Log("Delay threshold reached. Forcing a correct bubble spawn.");
            OnForceCorrectBubble?.Invoke(NumberAnnouncer.announcedNumber);
            correctBubbleSpawned = true;
        }
    }

    private void HandleBubbleDestroyed(BubbleController bubble)
    {
        // Reserved for future behavior (optional)
    }

    public void ResetLogic()
    {
        delayCounter = 0;
        correctBubbleSpawned = false;
        delayCount = GetDelayCount(); // optional re-roll each announce
    }
}
