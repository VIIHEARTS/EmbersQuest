using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        MusicManager.Instance.PlayMusic("MainMenu");
    }

    public void Play()
    {
        LevelManager.Instance.LoadScene("Game", "CrossFade");
        MusicManager.Instance.PlayMusic("Game");
        MusicManager.Instance.PlayMusic("Forest");
    }

    public void Settings()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
