using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    
    
    private Animator animator;
    public Slider slider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        currentHealth= maxHealth;
        slider.maxValue = maxHealth;
        slider.value=currentHealth;
    }
    private void Update()
    {
        
        if (animator != null && currentHealth==0)
        {
            animator.SetBool("IsDead", true);
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        currentHealth -= damage;
        slider.value = currentHealth;
    }
    //esta funcion se llama al final de la animacion de muerte del player
    private void Death()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Derrota");
    }
}