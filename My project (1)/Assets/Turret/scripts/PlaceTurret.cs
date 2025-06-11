using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceTurret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TilemapCollider2D tilemapCollider;
    [SerializeField] private LayerMask placeableLayer;
    [SerializeField] private LayerMask turretLayer;
    [SerializeField] private GameObject shopUI;

    [Header("Settings")]
    [SerializeField] private float checkRadius = 0.3f;

    private void Update()
    {
        if (!UIShop.modeShop || !UIShop.TurretSelected) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, placeableLayer);

            if (hit.collider != null)
            {
                Vector3Int cell = tilemap.WorldToCell(hit.point);
                Vector3 cellCenter = tilemap.GetCellCenterWorld(cell);

                if (Physics2D.OverlapCircle(cellCenter, checkRadius, turretLayer) != null)
                {    
                    ResetSelection();
                    return;
                }

                ShopTower turretToBuild = BuildTurrets.Instance.GetSelectedTurret();

                if (ShopManager.Instance.Currency < turretToBuild.cost)
                {
                    ResetSelection();
                    return;
                }

                ShopManager.Instance.SpendCurrency(turretToBuild.cost);
                Instantiate(turretToBuild.turretPrefab, cellCenter, Quaternion.identity);

                ResetSelection();
            }
            else
            {
                ResetSelection();
            }
        }
    }

    private void ResetSelection()
    {
        UIShop.TurretSelected = false;
        UIShop.modeShop = false;
        Movement.modeAttack = true;
        shopUI.SetActive(true);
    }
}
