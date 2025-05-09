using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PlaceTurret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TilemapCollider2D tmC;
    [SerializeField] private LayerMask placeTurret;
    [SerializeField] private LayerMask turretLayer;
    public GameObject Shop;
    public static bool canPurchaseThis = true;
    private GameObject turret;
    
   
    

    void Update(){
        if (UIShop.modeShop == true){
            if (UIShop.TurretSelected == true){
                if (Input.GetMouseButtonDown(0)){
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, placeTurret);
                    if (hit.collider != null) {
                        Vector3Int cellPosition = tmC.GetComponent<Tilemap>().WorldToCell(hit.point);
                        Vector3 cellCenter = tmC.GetComponent<Tilemap>().GetCellCenterWorld(cellPosition);
                        float checkRadius = 0.3f;  // Ajusta si es necesario según el tamaño de tu celda
                        Collider2D hitTurret = Physics2D.OverlapCircle(cellCenter, checkRadius, turretLayer);
                        if (hitTurret != null)
                        {
                            Debug.Log("Ya hay una torreta aquí.");
                            return;
                        }
                        
                            ShopTower turretToBuild = BuildTurrets.main.GetSelectedTurret();
                        
                        if (turretToBuild.cost > ShopManager.main.currency){
                            Debug.Log("no hay plata");
                            BuildTurrets.SelectTurret = false;
                            UIShop.TurretSelected = false;
                            return;
                        }
                        ShopManager.main.SpendCurrency(turretToBuild.cost);
                        turret = Instantiate(turretToBuild.turretPrefab, hit.point, Quaternion.identity);
                        UIShop.TurretSelected = false;
                        Shop.SetActive(true);
                    }else{
                        UIShop.TurretSelected = false;
                        Shop.SetActive(true);
                        Movement.modeAttack = true;
                    }
                }
            }
        }
    }
}
