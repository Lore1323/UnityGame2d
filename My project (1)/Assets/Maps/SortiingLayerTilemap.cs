using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapRenderer))]
public class TilemapDynamicSorting : MonoBehaviour
{
    [Header("Opcional: Sumar o restar un offset si quieres ajustar el orden final")]
    [SerializeField] private int orderOffset = 0;

    [Header("Para objetos estáticos que no cambian de posición (ej: árboles, decoraciones)")]
    [SerializeField] private bool updateOnceOnStart = true;

    private TilemapRenderer tilemapRenderer;

    void Start()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();

        if (updateOnceOnStart)
            UpdateSortingOrder();
    }

    void LateUpdate()
    {
        if (!updateOnceOnStart)
            UpdateSortingOrder();
    }

    void UpdateSortingOrder()
    {
        int sortingOrder = Mathf.RoundToInt(-transform.position.y * 100) + orderOffset;
        tilemapRenderer.sortingOrder = sortingOrder;
    }
}
