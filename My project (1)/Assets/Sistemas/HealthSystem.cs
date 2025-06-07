using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    public int damage;
    private Animator animator;
    public Slider slider;

    private void Awake()
    {
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
        if (animator != null)
        {
            animator.SetInteger("Life", maxHealth);
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
        Debug.Log("Muerto");
        Destroy(gameObject);
    }
}