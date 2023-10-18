using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void NextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        SceneManager.LoadScene(nextSceneIndex);
    }
    public void Restart()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
