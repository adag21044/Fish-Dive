using UnityEngine;
using DG.Tweening;
using System;

public class SceneDrawer : MonoBehaviour
{
    [SerializeField] private GameObject[] sceneObjects;
    public event Action OnSceneDrawn;

    private void Start()
    {
        DrawScene();
    }

    private void DrawScene()
    {
        if (sceneObjects.Length == 0 || sceneObjects[0] == null)
        {
            Debug.LogError("Scene object not assigned!");
            return;
        }

        var target = sceneObjects[0].transform;
        target.DOMoveY(target.position.y + 10f, 2f)
            .SetEase(Ease.Unset)
            .OnComplete(() =>
            {
                Debug.Log("Scene drawn successfully!");

                OnSceneDrawn?.Invoke();
            });
    }
}