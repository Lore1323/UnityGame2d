using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalEnterLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {  
            Invoke("CargarNivel", 1.5f);
        }
    }
    public void CargarNivel()
    {
        SceneManager.LoadScene("Level_1");
    }
}
