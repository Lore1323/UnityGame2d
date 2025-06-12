using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int towersCompleted = 0;
    public int totalTowers = 5;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RegisterTowerCompleted()
    {
        towersCompleted++;
        Debug.Log($"Torres completadas: {towersCompleted}/{totalTowers}");

        if (towersCompleted >= totalTowers)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        Debug.Log("¡Juego ganado!");
        SceneManager.LoadScene("Victoria"); 
    }
}
