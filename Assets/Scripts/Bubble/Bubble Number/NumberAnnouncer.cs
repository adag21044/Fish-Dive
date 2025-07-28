using UnityEngine;

public class NumberAnnouncer : MonoBehaviour
{
    [SerializeField] private AudioClip[] numberClips; // 0 zero, 1 one, 2 two, etc.
    [SerializeField] private AudioSource audioSource;

    // for only testing purposes
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AnnounceNumber(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AnnounceNumber(2);
        }
    }
    
    private void AnnounceNumber(int number)
    {
        if (number < 0 || number >= numberClips.Length)
        {
            Debug.LogError("Number out of range for announcement: " + number);
            return;
        }

        AudioClip clip = numberClips[number];
        GameObject tempGO = Instantiate(audioSource, transform.position, Quaternion.identity).gameObject;
        AudioSource tempAudioSource = tempGO.GetComponent<AudioSource>();

        tempAudioSource.clip = clip;
        tempAudioSource.Play();
        Destroy(tempGO, clip.length + 0.1f); // Cleanup after clip finishes
    }
}