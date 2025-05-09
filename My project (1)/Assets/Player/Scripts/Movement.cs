using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;


public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform SpawnPoint;

    public PlayerController playerController;
    private Vector2 moved;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    public GameObject shop;
    public static bool isShopOpen =false;
    public bool isAttack=false;
    public static bool modeAttack = true;

    readonly int Fire_hash = Animator.StringToHash("Fire");


    private void Awake()
    {
        playerController = new PlayerController();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        playerController.Combat.Attack.started += _ => Attacks();
    }
    
    private void OnEnable(){
        playerController.Enable();
    }

    private void Update()
    {
        if (isShopOpen == false)
            PlayerInput();
        if (moved.x != 0 || moved.y != 0)
        {
            myAnimator.SetFloat("LastX", moved.x);
            myAnimator.SetFloat("LastY", moved.y);
        }
    }


    private void FixedUpdate(){
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mouseWorldPosition.x < transform.position.x)
        {
            mySpriteRenderer.flipX = false;
            Debug.Log("izq");
        }
        else
        {
            mySpriteRenderer.flipX = true;
            Debug.Log("der");
        }
        if (isAttack == false){
            Move();
        }
}

    private void PlayerInput(){
        if (modeAttack == true){
            moved = playerController.MovemenT.Move.ReadValue<Vector2>();
            myAnimator.SetFloat("MoveX", moved.x);
            myAnimator.SetFloat("MoveY",moved.y); 
        }
    }        

    void Move(){
        rb.MovePosition(rb.position + moved * (moveSpeed * Time.fixedDeltaTime)); 
        
    }

    void Attacks(){
        if (modeAttack == true){
            if (isShopOpen == false){
                myAnimator.SetBool("IsAttacking", true);
                isAttack = true;
                Shoot();
            }
        }
    }
    private void EndAttack(){
        myAnimator.SetBool("IsAttacking", false);
        isAttack= false;
    }
    private void Shoot()
    {
        
    }
}