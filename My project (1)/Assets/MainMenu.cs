using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Settings;
    public GameObject Credits;
    public GameObject HTP;
    public GameObject Sound;
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
        Sound.SetActive(false);
    }
    public void EnterHTP()
    {
        HTP.SetActive(true);
        Credits.SetActive(false);
        Sound.SetActive(false);
    }
    public void OnEnterSound()
    {
        HTP.SetActive(false );
        Credits.SetActive(false);
        Sound.SetActive(true);
    }
}
