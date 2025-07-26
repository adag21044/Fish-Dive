using DG.Tweening;
using UnityEngine;

public class HeartDestroyer : MonoBehaviour
{
    [SerializeField] private float shrinkDuration = 1f;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            DestroyHeart();
        }
    }

    [ContextMenu("Test DestroyHeart")]
    public void DestroyHeart()
    {
        transform.DOScale(Vector3.zero, shrinkDuration)
            .SetEase(Ease.Linear)  
            .SetUpdate(UpdateType.Normal)
            .OnComplete(() =>
            {
                Destroy(gameObject);
                Debug.Log("Heart destroyed: " + gameObject.name);
            });
    }
}