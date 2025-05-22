using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    public int damage;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        currentHealth = maxHealth;
        
    }
    private void Update()
    {
        animator.SetInteger("Life", maxHealth);
    }

    public void TakeDamage()
    {
        maxHealth=Mathf.Clamp(maxHealth, 0, maxHealth);
        maxHealth -= damage;
        Debug.Log(gameObject.name + " takes damage: " + damage);
        maxHealth = Mathf.Clamp(maxHealth, 0, maxHealth);
    }

    private void Death()
    {
        Debug.Log("Muerto");
        Destroy(gameObject);
    }
}