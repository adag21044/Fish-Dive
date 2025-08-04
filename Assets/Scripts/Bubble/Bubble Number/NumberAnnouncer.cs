using UnityEngine;
using System;


public class NumberAnnouncer : MonoBehaviour
{
    [SerializeField] private AudioClip[] numberClips; // 0 zero, 1 one, 2 two, etc.
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private MegaphoneAnimator megaphoneAnimator;
    [SerializeField] public static int announcedNumber = 1;
    public static event Action OnNumberAnnounced;
    [SerializeField] private float announceInterval = 5f; // Time interval between announcements
    private float announceTimer;
    private bool isRunning = false;


    private void Update()
    {
        // for only testing purposes
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    AnnounceNumber(GenerateRandomNumber());
        //}

        if (!isRunning)
        {
            return;
        }

        announceTimer += Time.deltaTime;
        if (announceTimer >= announceInterval)
        {
            announceTimer = 0f;
            AnnounceNumber(GenerateRandomNumber());
        }
    }

    public void StartAnnouncing()
    {
        announceTimer = 0f;
        isRunning = true;
    }

    private int AnnounceNumber(int number)
    {
        if (number < 0 || number >= numberClips.Length)
        {
            Debug.LogError("Number out of range for announcement: " + number);
            return 0;
        }

        AudioClip clip = numberClips[number];
        GameObject tempGO = Instantiate(audioSource, transform.position, Quaternion.identity).gameObject;
        AudioSource tempAudioSource = tempGO.GetComponent<AudioSource>();

        tempAudioSource.clip = clip;
        tempAudioSource.Play();

        // Trigger megaphone animation
        megaphoneAnimator.PlayMegaphoneAnimation();

        // Stop megaphone after sound
        float duration = clip.length;
        Invoke(nameof(StopMegaphone), duration);

        Destroy(tempGO, duration + 0.1f);
        OnNumberAnnounced?.Invoke();
        return number;
    }

    private void StopMegaphone()
    {
        megaphoneAnimator?.StopMegaphone();
    }
    
    private int GenerateRandomNumber()
    {
        var data = LevelManager.Instance.CurrentLevelData;
        int randomNumber = UnityEngine.Random.Range(data.minnumber, data.maxnumber + 1);
        announcedNumber = randomNumber;
        return randomNumber;
    }
}