using UnityEditor;
using UnityEngine;

public class BuildTurrets : MonoBehaviour
{
    public static BuildTurrets main;

    [Header("References")]
    [SerializeField] private GameObject[] turretPrefabs;

    private int selectedTurret = 0;
    public LayerMask placeTurret;

    private void Awake()
    {
        main = this;
    }
    
    public GameObject GetSelectedTurret()
    {
        return turretPrefabs[selectedTurret];
    }

}
