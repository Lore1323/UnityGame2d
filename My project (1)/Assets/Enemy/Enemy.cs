using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using UnityEditor.Rendering.Analytics;

public class Enemy : MonoBehaviour
{
    [Header("Attribute")]
    [SerializeField] public float health;
    [SerializeField] private float damage;
    [SerializeField] private int worth;
    [SerializeField] private float attackCooldown = 1f; 
    [SerializeField] private float detectPlayer;
    [SerializeField] private float attackRange;
    [SerializeField] public float speed = 5f;
    [SerializeField] public int DealtDamage;
    [Header("References")]
    [SerializeField] public Transform playerTarget;
    [SerializeField] public Transform target;
    [SerializeField] public AudioSource source;
    [SerializeField] public AudioClip impact;


    [SerializeField] private float actualHealth;
    private Transform currentTarget;
    private float distance;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private float lastAttackTime;
    private bool isDead = false;

    private void Awake()
    {
        actualHealth = health;
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

    private void Start()
    {
        currentTarget = target;

        if (playerTarget == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTarget = player.transform;
            }
            else
            {
                Debug.LogWarning("No se encontró un objeto con la etiqueta 'Player'");
            }
        }
    }


    private void Update()
    {
        CheckPlayerIsInRange();
        FollowObject();
        AttackObjective();
        myAnimator.SetFloat("Life", actualHealth);    
    }
    //esta funcion hace que el enemigo siga al objetivo y setea un bool de las animaciones
    private void FollowObject()
    {
        
        if (target != null && myAnimator.GetBool("CanWalk")==true)
        {
            myAnimator.SetBool("CanWalk", true);
            SearchPlayer();
            distance = Vector2.Distance(transform.position, currentTarget.transform.position);
            Vector2 direction = currentTarget.transform.position - transform.position;
            direction.Normalize();
            transform.position = Vector2.MoveTowards(this.transform.position, currentTarget.transform.position, speed * Time.deltaTime);
        }    
    }
    //esta funcion hace que el objetivo pase a ser el jugador cuando esta dentro del rango
    private void SearchPlayer()
    {
        if (CheckPlayerIsInRange())
        {
            currentTarget = playerTarget;
        }
        else
            currentTarget = target;
    }
    //esta funcion revisa si el player esta en rango
    private bool CheckPlayerIsInRange()
    {
        return Vector2.Distance(playerTarget.position, transform.position) <= detectPlayer;
    }
    //esta funcion se encarga del ataque del enemigo y de las animaciones
    private void AttackObjective()
    {
        if (IsInAttackRange())
        {
            myAnimator.SetBool("CanWalk", false);
            if (Time.time >= lastAttackTime + attackCooldown && myAnimator.GetBool("IsDead") == false)
            {
                myAnimator.SetBool("IsAttacking", true);
                lastAttackTime = Time.time;
                HealthSystem health = currentTarget.GetComponent<HealthSystem>();
                
                if (health != null)
                
                health.TakeDamage(damage);
            }
        }
        else
        {
            myAnimator.SetBool("IsAttacking", false);
            myAnimator.SetBool("CanWalk", true);
        }
    }
    public void ResetAttackAnimation()
    {
        myAnimator.SetBool("IsAttacking", false);
    }
    //esta funcion revisa si el player esta en rango de ataque
    private bool IsInAttackRange()
    {
        return Vector2.Distance(currentTarget.position, transform.position) <= attackRange;
    }
    //esta funcion hace que el enemigo pueda recibir da�o
    public void TakeDamage(int appliedDamage)
    {
        if (isDead) return;
        actualHealth -= appliedDamage;
        source.PlayOneShot(impact);
        actualHealth = Mathf.Clamp(actualHealth, 0,500);
        if (actualHealth <= 0)
        {
            isDead = true;
            myAnimator.SetBool("IsDead", true);
            ShopManager.Instance.AddCurrency(worth);    
        }
    }
    //esta funcion hace que el enemigo vuelva al PoolManager
    public void ReturnPool()
    {    
        ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }
    public void CantAttackDeath()
    {
        myAnimator.SetBool("IsDead", true);
    }
    public void ResetEnemy()
    {
        actualHealth = health * EnemyStats.HealthMultiplier;
        myAnimator.Rebind(); 
        myAnimator.Update(0f); 
   
        myAnimator.SetFloat("Life", actualHealth);
        myAnimator.SetBool("IsDead", false);
        myAnimator.SetBool("IsAttacking", false);
        myAnimator.SetBool("CanWalk", false);
        isDead = false;
        currentTarget = target;
    }
    public void SetStats(int wave)
    {
        if (wave == 1)
        {
            actualHealth = health;
            speed = speed;
            damage = damage;
        }
        else if (wave == 2)
        {
            actualHealth= health * EnemyStats.HealthMultiplier;
            speed*=EnemyStats.speedMultiplier;
            damage*=EnemyStats.damageMultiplier;
        }
        else
        {
            actualHealth *= EnemyStats.HealthMultiplier;
            speed *= EnemyStats.speedMultiplier;
            damage *= EnemyStats.damageMultiplier;
        }
    }
}
