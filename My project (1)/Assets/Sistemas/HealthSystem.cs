using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    private float currentHealth;
    public float damage;
    private Animator animator;
    public Slider slider;

    private void Awake()
    {
        maxHealth = Mathf.Clamp(maxHealth, 0, maxHealth);
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        currentHealth = maxHealth;    
        slider.maxValue = currentHealth;
        slider.value=maxHealth;
    }
    private void Update()
    {
        damage = 0;
        if (animator != null && maxHealth==0)
        {
            animator.SetBool("IsDead", true);
        }
    }
    public void TakeDamage()
    {
        maxHealth=Mathf.Clamp(maxHealth, 0, maxHealth);
        maxHealth -= damage;
        slider.value = maxHealth;
        maxHealth = Mathf.Clamp(maxHealth, 0, maxHealth);
    }
    //esta funcion se llama al final de la animacion de muerte del player
    private void Death()
    {
        Destroy(gameObject);
    }
}