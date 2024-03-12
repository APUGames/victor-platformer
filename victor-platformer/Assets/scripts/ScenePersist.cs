using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    int startingSceneIndex;
    void Awake()
    {
        // Will find the number of occurrences of this Game Object
        int numScenePersist = FindObjectsOfType<ScenePersist>().Length;
        if (numScenePersist > 1)
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
    void Start()
    {
        startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    
    void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(currentSceneIndex != startingSceneIndex)
        {
            Destroy(gameObject);
        }

    }

}
