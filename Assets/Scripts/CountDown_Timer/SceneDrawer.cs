using UnityEngine;
using DG.Tweening;
using System;

public class SceneDrawer : MonoBehaviour
{
    [SerializeField] private GameObject[] sceneObjects;
    [SerializeField] private GameObject megaphone;
    public event Action OnSceneDrawn;
    [SerializeField] private Timer timer;

    private void Start()
    {
        DrawScene();
        OnSceneDrawn += ShowHearts;
        OnSceneDrawn += ShowMegaphone;
        OnSceneDrawn += () => timer.SetTimerActive();
        OnSceneDrawn += timer.StartTimer; 
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

    private void ShowHearts()
    {
        foreach (var obj in sceneObjects)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
            else
            {
                Debug.LogWarning("One of the heart objects is null!");
            }
        }
    }

    private void ShowMegaphone()
    {
        if (megaphone != null)
        {
            megaphone.SetActive(true);
            Debug.Log("Megaphone shown!");
        }
        else
        {
            Debug.LogWarning("Megaphone object is not assigned!");
        }
    }
}