using UnityEngine;

public class BubbleAutoDestroy : MonoBehaviour
{
    [SerializeField] private float topYLimit = 5.2f;
    [SerializeField] private BubbleController bubbleController;

    private void Update()
    {
        if (transform.position.y > topYLimit)
        {
            bubbleController.DisappearAtTop();
        }
    }
}
