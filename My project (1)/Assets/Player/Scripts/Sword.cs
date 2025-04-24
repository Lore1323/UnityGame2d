using UnityEngine;

public class Sword : MonoBehaviour
{
    private PlayerController playerControls;
    private Animator animator;
    [SerializeField] private int attackDamage;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerControls = new PlayerController();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
    }
    private void Attack()
    {
        
    }
    private void Update()
    {
        GetPlayerLook();
    }
    private void GetPlayerLook()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("ENTRO EL ENEMIGO");
            Enemy enemigo = other.GetComponent<Enemy>();
            
            if (enemigo != null)
            {
                enemigo.DealtDamage += attackDamage;
                enemigo.TakeDamage();
            }
        }
    }
}
