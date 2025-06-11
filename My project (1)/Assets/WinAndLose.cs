using UnityEngine;
using UnityEngine.SceneManagement;

public class WinAndLose : MonoBehaviour
{
    public void OnRetry()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void OnMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
