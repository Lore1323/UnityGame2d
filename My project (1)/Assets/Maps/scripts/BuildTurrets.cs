using TMPro;
using UnityEngine;

public class BuildTurrets : MonoBehaviour
{
    public static BuildTurrets Instance { get; private set; }

    [SerializeField] public ShopTower[] towers;
    private int selectedTurretIndex = 0;

    public LayerMask placeTurret;
    public bool TurretSelected { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public ShopTower GetSelectedTurret()
    {
        TurretSelected = true;
        return towers[selectedTurretIndex];
    }

    public void SetSelectedTurret(int index)
    {
        if (index >= 0 && index < towers.Length)
        {
            selectedTurretIndex = index;  
        }
    }
}