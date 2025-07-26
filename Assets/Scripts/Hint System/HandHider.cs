using UnityEngine;

public class HandHider : MonoBehaviour
{
    [SerializeField] private GameObject handObject; // Reference to the hand object
    [SerializeField] private FishMove fishMove; // Reference to the FishMove script
    

    private void Update()
    {
        if (fishMove.IsTouched)
        {
            HideHandHint();
        }
        
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
}