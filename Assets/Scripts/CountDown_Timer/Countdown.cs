using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;
using System;

public class Countdown : MonoBehaviour
{
    public static Countdown Instance { get; private set; }
    private int countDownTime = 3;
    [SerializeField] private TextMeshProUGUI countdownText;
    public event Action OnCountdownFinished; // << EVENT
    public bool IsCountdownFinished { get; private set; } = false;

    private void Awake()
    {
        // Singleton 
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        countdownText.gameObject.SetActive(true);

        while (countDownTime > 0)
        {
            countdownText.text = countDownTime.ToString();
            AnimateText();
            countDownTime--;

            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "Go!";
        AnimateText();

        yield return new WaitForSeconds(1f);

        countdownText.DOFade(0, 0.5f).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(0.5f);

        countdownText.gameObject.SetActive(false);

        IsCountdownFinished = true;

        Debug.Log("Countdown finished!");
        OnCountdownFinished?.Invoke(); // << EVENT FIRING
    }

     private void AnimateText()
    {
        countdownText.color = new Color(1, 1, 1, 1); // reset alpha
        countdownText.transform.localScale = Vector3.one * 1.5f;
        countdownText.DOFade(1f, 0f); // reset fade
        countdownText.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
    }

}
