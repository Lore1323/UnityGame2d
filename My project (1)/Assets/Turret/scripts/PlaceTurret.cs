using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PlaceTurret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TilemapCollider2D tmC;
    [SerializeField] private LayerMask placeTurret;

    private GameObject turret;
    
   
    private void OnMouseDown()
    {
        if (turret == null) return;
        turret  =BuildTurrets.main.GetSelectedTurret();
        turret = Instantiate(turret,transform.position, Quaternion.identity);
        Debug.Log("Build Tower Here: " + name);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Click izquierdo
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Hacer un Raycast en la posición del mouse para detectar el Tilemap
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, placeTurret);

            if (hit.collider != null) // Si golpea algo en la capa del Tilemap
            {
                
              
            turret = BuildTurrets.main.GetSelectedTurret();
            Instantiate(turret, hit.point, Quaternion.identity);
            Debug.Log("Torreta colocada en: " + hit.point);
                
            }
            
            else
            {
                Debug.Log("No puedes colocar la torreta aquí.");
            }
        }
    }
}
