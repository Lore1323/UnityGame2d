using UnityEditor;
using UnityEngine;

public class BuildTurrets : MonoBehaviour
{
    public static BuildTurrets main;
    public static bool IsSelected= false;  

    [Header("References")]
    [SerializeField] private ShopTower[] turretPrefabs;

    private int selectedTurret = 0;
    public LayerMask placeTurret;

    private void Awake()
    {
        main = this;
    }
    
    public ShopTower GetSelectedTurret()
    {
        return turretPrefabs[selectedTurret];
    }
    public void SetSelectecTurret(int _selectedTurret)
    {
        if (IsSelected == false)
        {
            selectedTurret = _selectedTurret;
            IsSelected = true;
        }
    }

}
