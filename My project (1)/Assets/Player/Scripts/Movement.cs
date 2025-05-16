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
    private Vector3 originalScale;

    readonly int Fire_hash = Animator.StringToHash("Fire");


    private void Awake()
    {
        originalScale = transform.localScale;
        playerController = new PlayerController();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    
    private void OnEnable(){
        playerController.Enable();
        playerController.Combat.Attack.started += _ => Attacks();
    }
    private void OnDisable()
    {
        playerController.Disable();
        playerController.Combat.Attack.started -= _ => Attacks();
    }

    private void Update()
    {
        if (isShopOpen == false)
        {
            PlayerInput();
            AdjustPlayerFacingDirection();
        }
    }


    private void FixedUpdate(){   
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
        Debug.Log("Attack ejecutado");
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
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPosition - SpawnPoint.position).normalized;
        GameObject newBullet = Instantiate(bullet, SpawnPoint.position, Quaternion.identity);
        newBullet.GetComponent<Projectile>().SetDirection(direction);

    }
    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x && !mySpriteRenderer.flipX){
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
        else
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        }
    }
}