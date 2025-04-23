using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    public PlayerController playerController;
    private Vector2 moved;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    public GameObject shop;
    
    
    private void Awake()
    {
        playerController = new PlayerController();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();  
        
    }
    
    private void OnEnable()
    {
        playerController.Enable();
    }

    private void Update()
    {
        PlayerInput();   
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void PlayerInput()
    {
        moved = playerController.MovemenT.Move.ReadValue<Vector2>();  
        myAnimator.SetFloat("MoveX", moved.x);
        myAnimator.SetFloat("MoveY", moved.y);
        playerController.Combat.Attack.started += _ => Attacks();
        playerController.Interact.OpenShop.started += _ => OpenShop();
    }        

    void Move()
    {
        rb.MovePosition(rb.position + moved * (moveSpeed * Time.fixedDeltaTime));   
    }
    void Attacks()
    {
        myAnimator.SetBool("IsAttacking", true);
        
    }
    private void EndAttack()
    {
        myAnimator.SetBool("IsAttacking", false);
    }
    public void OpenShop()
    {
        Debug.Log("ando");
        shop.SetActive(true);

    }

}