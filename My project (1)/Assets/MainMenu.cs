using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Settings;
    public GameObject Credits;
    public GameObject HTP;
    public void OnEnterLevel()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void OnEnterSetting()
    {
        Settings.SetActive(true);
    }
    public void ExitSetting()
    {
        Settings.SetActive(false);
    }
    public void EnterCredits()
    {
        Credits.SetActive(true);
        HTP.SetActive(false);
    }
    public void EnterHTP()
    {
        HTP.SetActive(true);
        Credits.SetActive(false);
    }
}
