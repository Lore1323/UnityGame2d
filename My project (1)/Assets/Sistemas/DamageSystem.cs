using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    [SerializeField] public int damageAmount = 10;
    [SerializeField] private float damageRadius = 1.5f;
    [SerializeField] private bool isPlayer;
    [SerializeField] private KeyCode attackKey = KeyCode.Mouse0;
    [SerializeField] private float attackCooldown = 1f;

    private float lastAttackTime;

    private void Update()
    {
        if (isPlayer && Input.GetMouseButtonDown(0))
        {
            Attack();
            Debug.Log("atacó");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayer)
        {
            if (other.CompareTag("Player"))
            {
                AttackTarget(other);
            }

            Debug.Log("colisión");
        }
    }

    public void Attack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                AttackTarget(hitCollider);
            }
        }

    }

    private void AttackTarget(Collider target)
    {
        if (Time.time - lastAttackTime < attackCooldown) return;

        HealthSystem targetHealth = target.GetComponent<HealthSystem>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damageAmount);
            Debug.Log(gameObject.name + "atacó a" + target.name);
        }

        lastAttackTime = Time.time;
    }
}
    