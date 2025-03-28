using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private float damageRadius = 1.5f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private bool isPlayer;
    [SerializeField] private KeyCode attackKey = KeyCode.Mouse0;

    private float _attackCooldown = 1f;
    private float _lastAttackTime;

    private void Update()
    {
        if (isPlayer && Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (Time.time - _lastAttackTime < _attackCooldown) return;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, damageRadius, targetLayer))
        {
            HealthSystem targetHealth = hit.collider.GetComponent<HealthSystem>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damageAmount);
                Debug.Log(gameObject.name + " attacked" + hit.collider.name);
            }
        }

        _lastAttackTime = Time.time;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isPlayer && other.CompareTag("Player"))
        {
            Attack();
        }
    }
}