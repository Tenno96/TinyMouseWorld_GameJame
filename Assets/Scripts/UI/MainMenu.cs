using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OpenLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
        
        #else
            Application.Quit();
        #endif
    }
}
