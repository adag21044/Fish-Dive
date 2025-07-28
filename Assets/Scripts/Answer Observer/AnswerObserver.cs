using UnityEngine;

public class AnswerObserver : MonoBehaviour
{
    [SerializeField] private BubbleNumberSelector bubbleNumberSelector;
    [SerializeField] private HeartController heartController;

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

        }
    }
}