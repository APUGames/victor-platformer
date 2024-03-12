using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int playerScore = 0;
    [SerializeField] private Text lives;
    [SerializeField] private Text score;


    void Start()
    {
        lives.text = playerLives.ToString();
        score.text = playerScore.ToString();
    }
    void Awake()
    {
        // Will find the number of occurrences of this Game Object
        int numGameSessions =
        FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            // This will make it so that if the Scene restarts,
            // the Singleton will not be destroyed
            DontDestroyOnLoad(gameObject);
        }
    }

    // Making this a public method so that other classes
    // can access it
    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            SubtractLife(); // Not yet written
        }
        else
        {
            ResetGameSession(); // Not yet written
        }
    }

    public void ProcessPlayerScore(int points)
    {
        playerScore += points;
        score.text = playerScore.ToString();
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0); // Need to import Unity’s
                                   // SceneManagement namespace!

        Destroy(gameObject);
    }

    private void SubtractLife()
    {
        playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        lives.text = playerLives.ToString();
    }

}
