using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickSceneReset : MonoBehaviour
{
    [SerializeField]
    private KeyCode resetSceneKey;

    void Update()
    {
        if (Input.GetKeyDown(resetSceneKey))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
