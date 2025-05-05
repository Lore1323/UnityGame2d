using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PlaceTurret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TilemapCollider2D tmC;
    [SerializeField] private LayerMask placeTurret;
    public GameObject Shop;
    public static bool canPurchaseThis = true;

    private GameObject turret;
    
   
    

    void Update()
    {
        if (UIShop.modeShop == true)
        {
            if (UIShop.TurretSelected == true)
            {
                if (Input.GetMouseButtonDown(0)) // Click izquierdo
                {
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    // Hacer un Raycast en la posición del mouse para detectar el Tilemap
                    RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, placeTurret);

                    if (hit.collider != null) // Si golpea algo en la capa del Tilemap
                    {
                        ShopTower turretToBuild = BuildTurrets.main.GetSelectedTurret();
                        if (turretToBuild.cost > ShopManager.main.currency)
                        {
                            Debug.Log("compra");
                            return;
                        }
                        ShopManager.main.SpendCurrency(turretToBuild.cost);
                        turret = Instantiate(turretToBuild.turretPrefab, hit.point, Quaternion.identity);
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
}
