using UnityEngine;

public class AnswerObserver : MonoBehaviour
{
    [SerializeField] private BubbleNumberSelector bubbleNumberSelector;
    [SerializeField] private HeartController heartController;
    [SerializeField] private FishController fishController;
    private int correctAnswerCount = 0;
    private float levelDuration = 90f;
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= levelDuration)
        {
            CheckLevelCompletion();
        }

        if (bubbleNumberSelector.SelectedBubbleNumber == NumberAnnouncer.announcedNumber)
        {
            Debug.Log("Correct number selected: " + bubbleNumberSelector.SelectedBubbleNumber);
            correctAnswerCount++;
        }

    }
    
    private void CheckLevelCompletion()
    {
        int requiredCorrect = LevelManager.Instance.CurrentLevelData.optimumquestioncount;

        if (correctAnswerCount >= requiredCorrect)
        {
            Debug.Log("LEVEL PASSED! Loading next level...");
            LevelManager.Instance.LoadLevel(LevelManager.Instance.CurrentLevelData.level + 1);
            // Sahneyi yeniden başlatabilirsin ya da bir animasyon geçişi koyabilirsin
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Debug.Log("Level failed or repeated.");
            // Retry logic veya aynı seviye tekrarı
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
        // You can add more logic here to process the selected bubble number

        if (bubbleNumberSelector.SelectedBubbleNumber == NumberAnnouncer.announcedNumber)
        {
            Debug.Log("Correct number selected: " + bubbleNumberSelector.SelectedBubbleNumber);
            // Handle correct selection logic here
        }
        else
        {
            Debug.Log("Incorrect number selected: " + bubbleNumberSelector.SelectedBubbleNumber);

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
        }
    }
}