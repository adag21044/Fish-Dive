using UnityEngine;
using System;

public class AnswerObserver : MonoBehaviour
{
    [SerializeField] private BubbleNumberSelector bubbleNumberSelector;
    [SerializeField] private HeartController heartController;
    [SerializeField] private FishController fishController;
    [SerializeField] private int correctAnswerCount = 0;
    [SerializeField] private AudioSource wrongAnswerSound;
    private const float levelDuration = 90f;
    private float timer = 0f;
    public static event Action<int> OnCorrectAnswer;
    public static event Action OnWrongAnswer;

    private void Update()
    {
        RunTimer();

        if (timer >= levelDuration)
        {
            CheckLevelCompletion();
        }
    }

    private void RunTimer()
    {
        timer += Time.deltaTime;
    }

    private int IncreaseCorrectAnswerCount()
    {
        Debug.Log("Increasing correct answer count.");
        correctAnswerCount++;
        return correctAnswerCount;
    }


    private void CheckLevelCompletion()
    {
        int requiredCorrect = LevelManager.Instance.CurrentLevelData.optimumquestioncount;

        if (correctAnswerCount >= requiredCorrect)
        {
            Debug.Log("LEVEL PASSED! Loading next level...");
            LevelManager.Instance.LoadLevel(LevelManager.Instance.CurrentLevelData.level + 1);
            
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Debug.Log("Level failed or repeated.");
        }
    }

    private void OnEnable()
    {
        bubbleNumberSelector.OnFishEnteredTrigger += HandleFishEnteredTrigger;
    }

    private void OnDisable()
    {
        bubbleNumberSelector.OnFishEnteredTrigger -= HandleFishEnteredTrigger;
    }

    private void HandleFishEnteredTrigger()
    {
        // Logic to handle when a fish enters the trigger
        Debug.Log("Fish entered the trigger, handling selection logic.");

        if (bubbleNumberSelector.SelectedBubbleNumber == NumberAnnouncer.announcedNumber)
        {
            Debug.Log("Correct number selected: " + bubbleNumberSelector.SelectedBubbleNumber);
            // Handle correct selection logic here
            int newCount = IncreaseCorrectAnswerCount();
            OnCorrectAnswer?.Invoke(newCount);
        }
        else
        {
            Debug.Log("Incorrect number selected: " + bubbleNumberSelector.SelectedBubbleNumber);
            PlayWrongAnswerSound();
            // Handle incorrect selection logic here
            heartController.DestroyNextHeart();
            if (fishController?.fishAnimator != null)
            {
                fishController.fishAnimator.PlayShake();
            }
            else
            {
                Debug.LogWarning("FishAnimator is null! Check assignments in Inspector.");
            }
            OnWrongAnswer?.Invoke();
        }
        NumberAnnouncer.Instance.StartAnnouncing();
    }

    private void PlayWrongAnswerSound()
    {
        if (wrongAnswerSound != null)
        {
            wrongAnswerSound.Play();
        }
    }
}