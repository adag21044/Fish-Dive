using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void StartGame()
    {

    }

    private void EndGame()
    {

    }

    private void LoadGame()
    {
        // Load game logic here
    }
}