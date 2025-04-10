using UnityEngine;

public class Sword : MonoBehaviour
{
    private PlayerController playerControls;
    private Animator animator;


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
}
