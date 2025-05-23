using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using UnityEditor.Rendering.Analytics;

public class Enemy : MonoBehaviour
{
    [Header("Attribute")]
    [SerializeField] public int health;
    [SerializeField] private int damage;
    [SerializeField] private int worth;
    [SerializeField] private float detectPlayer;
    [SerializeField] private float attackRange;
    [SerializeField] public float speed = 5f;
    [SerializeField] public int DealtDamage;
    [Header("References")]
    [SerializeField] public Transform playerTarget;
    [SerializeField] public Transform target;
    
    private Transform currentTarget;
    private float distance;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        health = 100;
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnDrawGizmosSelected()
    {
        //Rango de Deteccion del player
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, detectPlayer);
        //rango de Ataque
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.forward, attackRange);
    }

    void Start()
    {
        //seteo del objetivo a seguir
        currentTarget = target;
    }

    private void Update()
    {
        FollowObject();
        AttackObjective();
        myAnimator.SetInteger("Life", health);    
    }
    private void FollowObject()
    {
        if (target != null)
        {
            myAnimator.SetBool("CanWalk", true);
            SearchPlayer();
            distance = Vector2.Distance(transform.position, currentTarget.transform.position);
            Vector2 direction = currentTarget.transform.position - transform.position;
            direction.Normalize();
            transform.position = Vector2.MoveTowards(this.transform.position, currentTarget.transform.position, speed * Time.deltaTime);
        }
    }
    private void SearchPlayer()
    {
        if (CheckPlayerIsInRange())
        {
            currentTarget = playerTarget;
        }
        else
            currentTarget = target;
    }
    private bool CheckPlayerIsInRange()
    {
        return Vector2.Distance(playerTarget.position, transform.position) <= detectPlayer;
    }
    private void AttackObjective()
    {
        if (IsInAttackRange()) 
        {
            HealthSystem health= currentTarget.GetComponent<HealthSystem>();
            myAnimator.SetBool("IsAttacking", true);
            myAnimator.SetBool("CanWalk", false);
            health.damage+=damage;
            health.TakeDamage();
        }
        else
            myAnimator.SetBool("IsAttacking", false);
    }

    private bool IsInAttackRange()
    {
        return Vector2.Distance(currentTarget.position, transform.position) <= attackRange;
    }

    public void TakeDamage()
    {
        health -= DealtDamage;
        if (health <= 0)
        {
            health = Mathf.Clamp(health, 0, health);
            Debug.Log("Muere");
            ShopManager.main.IncreaseCurrency(worth);
            
        }
    }
    public void ReturnPool()
    {
        ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }
}
