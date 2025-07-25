using UnityEngine;
using System.Collections;

public class Countdown : MonoBehaviour
{
    private int countDownTime = 3; // Countdown starts from 3 seconds

    private void Start()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        while (countDownTime > 0)
        {
            Debug.Log("Countdown: " + countDownTime);
            yield return new WaitForSeconds(1f);
            countDownTime--;
        }

        Debug.Log("Countdown finished!");
    }
}