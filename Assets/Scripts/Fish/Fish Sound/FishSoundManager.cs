using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FishSoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip fishMovementSound;
    [SerializeField] private AudioSource audioSource;

    [Header("Fish Movement")]
    [SerializeField] private FishMove fishMover;

    [Header("Fade Settings")]
    [SerializeField] private float fadeOutTime = 0.6f;   // saniye
    [SerializeField] private AnimationCurve fadeCurve =
        AnimationCurve.EaseInOut(0, 1, 1, 0);            // eksp. hissi veren eğri

    private Coroutine fadeRoutine;

    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.clip = fishMovementSound;
        audioSource.loop = true;
        audioSource.volume = 1f;

        fishMover.OnStartMoving += PlaySwimSound;
        fishMover.OnStopMoving  += BeginFadeOut;
    }

    private void PlaySwimSound()
    {
        // Fade varsa iptal et, anında tam sesle devam et
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        audioSource.volume = 1f;

        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    private void BeginFadeOut()
    {
        if (audioSource.isPlaying && fadeRoutine == null)
            fadeRoutine = StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float startVol = audioSource.volume;
        float t = 0f;

        while (t < fadeOutTime)
        {
            t += Time.deltaTime;
            float normalized = t / fadeOutTime;
            // Eğriye göre ses seviyesi
            audioSource.volume = startVol * fadeCurve.Evaluate(normalized);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = 1f;   // Sonraki çalma için sıfırla
        fadeRoutine = null;
    }
}
