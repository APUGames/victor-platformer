using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
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
    }

}
