using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;


public class DelayedCorrectBubbleSpawner : MonoBehaviour
{
    [SerializeField] private BubbleSpawner bubbleSpawner;
    [SerializeField] private NumberAnnouncer numberAnnouncer;
    [SerializeField] private int delayCount = 3; // change this according json file

    private int delayCounter = 0;
    private bool correctBubbleSpawned = false;

    private void OnEnable()
    {
        NumberAnnouncer.OnNumberAnnounced += ResetLogic;
    }

    private void OnDisable()
    {
        NumberAnnouncer.OnNumberAnnounced -= ResetLogic;
    }


    private void Update()
    {
        // Eğer doğru sayı zaten ekrandaysa bir şey yapma
        if (IsCorrectBubbleAlreadySpawned())
        {
            correctBubbleSpawned = true;
        }
    }

    public void NotifyBubbleSpawned()
    {
        if (correctBubbleSpawned) return;

        // Ekranda varsa bir şey yapma
        if (IsCorrectBubbleAlreadySpawned())
        {
            correctBubbleSpawned = true;
            return;
        }

        // Sayım başlasın
        delayCounter++;

        if (delayCounter >= delayCount)
        {
            Debug.Log("DELAYCOUNT aşıldı, doğru balon zorunlu olarak spawn ediliyor.");
            SpawnCorrectBubble();
            correctBubbleSpawned = true;
        }
    }

    private bool IsCorrectBubbleAlreadySpawned()
    {
        bubbleSpawner.spawnedBubbles.RemoveAll(b => b == null);

        // Geçici olarak silinecek objeleri toplayalım
        List<GameObject> toRemove = new List<GameObject>();

        foreach (GameObject bubble in bubbleSpawner.spawnedBubbles)
        {
            if (bubble == null)
            {
                toRemove.Add(bubble); // destroy edilmiş obje
                continue;
            }

            BubbleNumberController ctrl = bubble.GetComponent<BubbleNumberController>();
            if (ctrl != null && ctrl.bubbleNumber == NumberAnnouncer.announcedNumber)
            {
                return true;
            }
        }

        // Temizle
        foreach (GameObject b in toRemove)
        {
            bubbleSpawner.spawnedBubbles.Remove(b);
        }

        return false;
    }


    private void SpawnCorrectBubble()
    {
        int number = NumberAnnouncer.announcedNumber;
        Vector2 spawnPos = GenerateSafeSpawnPosition();
        if (spawnPos == Vector2.zero) return;

        BubbleSO bubbleData = bubbleSpawner.bubbleData[Random.Range(0, bubbleSpawner.bubbleData.Length)];
        GameObject bubble = Instantiate(bubbleSpawner.BubblePrefab, spawnPos, Quaternion.identity);

        bubble.transform.localScale = Vector3.zero;
        bubble.transform.DOScale(0.5f, 0.35f).SetEase(Ease.InSine);

        BubbleNumberController numberController = bubble.GetComponent<BubbleNumberController>();
        if (numberController != null)
        {
            numberController.Setup(bubbleData.bubbleSprite, number);
        }

        BubbleController ctrl = bubble.GetComponent<BubbleController>();
        if (ctrl != null)
        {
            ctrl.Initialize(spawnPos, bubbleSpawner);
        }

        bubbleSpawner.spawnedBubbles.Add(bubble);

        DOVirtual.DelayedCall(3f, () => bubbleSpawner.FreePosition(spawnPos));
    }

    private Vector2 GenerateSafeSpawnPosition()
    {
        float radius = 1.1f;
        int maxAttempts = 20;

        for (int i = 0; i < maxAttempts; i++)
        {
            float x = Random.Range(-1.72f + radius, 1.72f - radius);
            float y = Random.Range(-2.75f + radius, 3.9f - radius);
            Vector2 pos = new Vector2(x, y);

            if (IsSafe(pos)) return pos;
        }

        return Vector2.zero; // başarısız
    }

    private bool IsSafe(Vector2 pos)
    {
        foreach (var p in bubbleSpawner.spawnedBubbles)
        {
            if (Vector2.Distance(p.transform.position, pos) < 1.0f)
                return false;
        }
        return true;
    }
    
    public void ResetLogic()
    {
        delayCounter = 0;
        correctBubbleSpawned = false;
    }
}