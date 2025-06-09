using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DynamicSortingOrder : MonoBehaviour
{
    [Tooltip("Usar solo una vez al inicio (útil para objetos estáticos)")]
    [SerializeField] private bool updateOnceOnStart = false;

    [Tooltip("Valor base para evitar números negativos")]
    [SerializeField] private int baseSortingOrder = 10000;

    [Tooltip("Offset opcional para ajustar prioridad")]
    [SerializeField] private int orderOffset = 0;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (updateOnceOnStart)
            UpdateSortingOrder();
    }

    void LateUpdate()
    {
        if (!updateOnceOnStart)
            UpdateSortingOrder();
    }

    private void UpdateSortingOrder()
    {
        int sortingOrder = baseSortingOrder + Mathf.RoundToInt(-transform.position.y * 100f) + orderOffset;
        spriteRenderer.sortingOrder = sortingOrder;
    }
}
