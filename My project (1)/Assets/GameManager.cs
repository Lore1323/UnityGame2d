using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int towersCompleted = 0;
    public int totalTowers = 5;
    
    public Spawner spawner;
    public TextMeshProUGUI text;

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
        

        if (towersCompleted >= totalTowers)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        
        SceneManager.LoadScene("Victoria"); 
    }
}
