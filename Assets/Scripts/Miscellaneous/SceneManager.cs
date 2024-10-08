using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void ChangeSceneToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void ChangeSceneToGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ChangeSceneToUpgrade()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void ChangeSceneToSettings()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
}
