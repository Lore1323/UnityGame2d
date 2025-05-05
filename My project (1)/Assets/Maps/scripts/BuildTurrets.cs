using UnityEditor;
using UnityEngine;

public class BuildTurrets : MonoBehaviour
{
    public static BuildTurrets main;

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
        return towers[selectedTurret];
    }
    public void SetSelectecTurret(int _selectedTurret)
    {
            selectedTurret = _selectedTurret;
            Debug.Log("torreta Seleccionada");
    }
}
