using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int health;
    [SerializeField] private int worth;
    public int DealtDamage;
    public Transform target;
    public float speed = 5f;
    private Transform currentTarget;
    private float distance;
    public Transform playerTarget;
    private float detectPlayer;
    
    
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, detectPlayer );
    }
    
    void Start()
    {
        currentTarget = target;
    }

    private void Update()
    {
        if (target != null)
        {
            distance = Vector2.Distance(transform.position, currentTarget.transform.position);
            Vector2 direction = currentTarget.transform.position - transform.position;
            direction.Normalize();
            transform.position = Vector2.MoveTowards(this.transform.position, currentTarget.transform.position, speed * Time.deltaTime);
        }
        
    }
    
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            currentTarget = playerTarget;
        }
        
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentTarget = target;
        }
    }

    public void TakeDamage()
    {
        health -= DealtDamage;

        if (health <= 0)
        {
            Die();
            Debug.Log("Muere");
            ShopManager.main.IncreaseCurrency(worth);
        }
        
    }

    void Die()
    {
        ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }
}
