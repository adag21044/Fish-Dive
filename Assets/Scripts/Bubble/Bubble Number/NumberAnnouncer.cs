using UnityEngine;

public class NumberAnnouncer : MonoBehaviour
{
    [SerializeField] private AudioClip[] numberClips; // 0 zero, 1 one, 2 two, etc.
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private MegaphoneAnimator megaphoneAnimator;
    [SerializeField] public static int announcedNumber = 1;

    // for only testing purposes
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AnnounceNumber(announcedNumber);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AnnounceNumber(2);
        }
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
        return number;
    }
    
    private void StopMegaphone()
    {
        megaphoneAnimator?.StopMegaphone();
    }
}