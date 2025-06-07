using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System.Threading;


public class ObjectToDefense : MonoBehaviour
{
    [Header("Attributes")]  
    [SerializeField] private Vector2 boxSize = new Vector2(2f, 2f);
    [SerializeField] private Vector2 boxOffset = new Vector2(0f, -0.5f);
    [SerializeField] private float influenceRange;
    [SerializeField] private LayerMask enemy;

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private GameObject spawnPointToActivate;

    public PlayerController controller;
    private void Awake()
    {
        controller = new PlayerController();
    }
    private void OnEnable()
    {
        controller.Enable();
        controller.B.Interact.started += ctx => Activate();
    }
    private void OnDisable()
    {
        controller.Disable();
        controller.B.Interact.started -= ctx => Activate();
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.orange;
        Vector3 position = transform.position+(Vector3)boxOffset;
        Handles.DrawWireCube(position, boxSize);

        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, influenceRange);
    }   
    private void Activate()
    {
        if (CheckPlayerInBox())
        {
            spawnPointToActivate.SetActive(true);
        }
    }
    private void Update()
    {
        AssignTargetsToEnemiesInRange();

    }

    private void AssignTargetsToEnemiesInRange()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, influenceRange, enemy);

        foreach (Collider2D hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.target = this.transform;
            }
        }
    }
    
    private bool CheckPlayerInBox()
    {
        Vector2 center = (Vector2)transform.position + boxOffset;
        Collider2D hit = Physics2D.OverlapBox(center, boxSize, 0f, LayerMask.GetMask("Player")); // Asegúrate de que el Player esté en esta layer
        return hit != null;
    }
}
