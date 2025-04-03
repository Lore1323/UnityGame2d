using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(0,currentHealth);
        
        Debug.Log(damage);

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Debug.Log("Muerto");
        Destroy(gameObject);
    }
}