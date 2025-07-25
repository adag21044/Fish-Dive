using UnityEngine;

public class BubbleSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource popSound;

    public void PlayPopSound()
    {
        if (popSound != null)
        {
            popSound.Play();
        }   
    }
}
