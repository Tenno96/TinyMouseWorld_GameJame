using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Image imageBackground;
    [SerializeField] private GameObject textGameOver;
    [SerializeField] private GameObject verticalLayoutButtons;

    void Start()
    {
        Bus<PlayerDeathEvent>.OnEvent += OnPlayerDeath;
    }

    public void OnDisable()
    {
        Bus<PlayerDeathEvent>.OnEvent -= OnPlayerDeath;
    }

    private void OnPlayerDeath(PlayerDeathEvent evt)
    {
        FadeIn();
    }

    public async void FadeIn()
    {
        Debug.Log("Called FadeIn function!");
        Color bgColor = imageBackground.color;

        while (imageBackground.color.a < 1.0f)
        {
            float newAlpha = imageBackground.color.a + 0.05f;
            Mathf.Clamp01(newAlpha);
            imageBackground.color = new Color(bgColor.r, bgColor.g, bgColor.b, newAlpha);
            await Task.Delay(100);
        }

        EnableButtons();
    }

    private void EnableButtons()
    {
        textGameOver.SetActive(true);
        verticalLayoutButtons.SetActive(true);
    }

    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
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
