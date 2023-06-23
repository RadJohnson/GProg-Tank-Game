using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCode : MonoBehaviour
{
    public int sceneToLoad;

    void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadSceneButton(int _sceneToLoad)
    {
        SceneManager.LoadScene(_sceneToLoad);
    }

    public void NextScene()
    {
        int nextScene;
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextScene);
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    //public void Resume()
    //{
    //    pauseMenu.SetActive(false);
    //    isPaused = false;
    //    Cursor.lockState = CursorLockMode.Locked;
    //}

}
