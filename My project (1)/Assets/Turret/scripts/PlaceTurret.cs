using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PlaceTurret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TilemapCollider2D tmC;
    [SerializeField] private LayerMask placeTurret;
    public GameObject Shop;

    private GameObject turret;
    
   
    private void OnMouseDown()
    {
        if (turret != null) return;
        ShopTower turretToBuild  =BuildTurrets.main.GetSelectedTurret();
        turret = Instantiate(turretToBuild.turretPrefab,transform.position, Quaternion.identity);
        Debug.Log("Build Tower Here: " + name);
    }

    void Update()
    {
        if (UIShop.TurretSelected==true)
        {
            if (Input.GetMouseButtonDown(0)) // Click izquierdo
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Hacer un Raycast en la posición del mouse para detectar el Tilemap
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, placeTurret);

                if (hit.collider != null) // Si golpea algo en la capa del Tilemap
                {
                    ShopTower turretToBuild = BuildTurrets.main.GetSelectedTurret();
                    Instantiate(turret, hit.point, Quaternion.identity);
                    UIShop.TurretSelected = false;
                    Shop.SetActive(true);
                }
                else
                {
                    UIShop.TurretSelected = false;
                    Shop.SetActive(true);
                    Movement.modeAttack = true;
                }
            }  
        }
        
    }
}
