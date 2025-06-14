using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] public AudioSource source;
    [SerializeField] public AudioClip damageSound;
    [SerializeField] public AudioClip deathSound;
    
    
    private Animator animator;
    public Slider slider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {   
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value=currentHealth;
        
    }
    private void Update()
    {   
        if (animator != null && currentHealth<=0)
        {
            animator.SetBool("IsDead", true);
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        currentHealth -= damage;
        source.PlayOneShot(damageSound);
        slider.value = currentHealth;
    }
    //esta funcion se llama al final de la animacion de muerte del player
    private void Death()
    {
        source.PlayOneShot(deathSound);
        Invoke("LoadDefeatScene", 1f);
    }
    private void LoadDefeatScene()
    {
        SceneManager.LoadScene("Derrota");
    }
}