using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class Turret : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private Transform rotatePoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [Header ("Attribute")]
    [SerializeField] private float Range = 1f;
    [SerializeField] private float Cadence = 1f;

    private Transform target;
    private float timeUntilFire;

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, Range);
    }

    void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }
        RotateTurretTarget();
        if (!CheckTargetIsInRange())
        {
            target = null;
        }else{ 
        timeUntilFire = Time.deltaTime;
            if (timeUntilFire >=1f/ Cadence)
            {
                shoot();
                timeUntilFire = 0f;
            }
        }
       
    }
    private void RotateTurretTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg -90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = targetRotation;
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, Range, (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= Range;
    }

    private void shoot()
    {
        GameObject bulletObj= Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

}
