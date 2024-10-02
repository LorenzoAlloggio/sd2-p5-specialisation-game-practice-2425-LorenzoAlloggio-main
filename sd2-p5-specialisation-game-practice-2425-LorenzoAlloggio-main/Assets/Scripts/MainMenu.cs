using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the next scene in the build index (ensure your game scenes are added in Build Settings)
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        // Quit the application (works only in a build, not in the editor)
        Application.Quit();
    }
}
