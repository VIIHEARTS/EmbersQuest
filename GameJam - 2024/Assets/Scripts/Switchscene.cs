using UnityEngine;
using UnityEngine.SceneManagement;

public class Switchscene : MonoBehaviour
{
    public string sceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.Instance.LoadScene(sceneName, "CrossFade");
        }
    }
}
