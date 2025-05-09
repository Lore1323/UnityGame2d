using UnityEditor;
using UnityEngine;

public class BuildTurrets : MonoBehaviour
{
    public static BuildTurrets main;
    public static bool SelectTurret;
    [Header("References")]
    [SerializeField] private ShopTower[] towers;

    private int selectedTurret = 0;
    public LayerMask placeTurret;

    private void Awake()
    {
        main = this;
    }
    
    public ShopTower GetSelectedTurret()
    {
        SelectTurret = true;
        return towers[selectedTurret];
    }
    public void SetSelectecTurret(int _selectedTurret)
    {
        if (SelectTurret == true)
        {
            selectedTurret = _selectedTurret;
            Debug.Log("torreta Seleccionada");
        }
    }
}
