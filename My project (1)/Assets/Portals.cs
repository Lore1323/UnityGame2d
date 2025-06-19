using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portals : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {

            Invoke("CargarMenu",1.5f);
        }
    }
    public void CargarMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
