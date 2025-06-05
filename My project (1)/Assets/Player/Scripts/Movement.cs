using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;


public class Movement : MonoBehaviour
{
    [Header("Attribute")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] public bool isAttack = false;
    [SerializeField] private float detectionRadius = 5f;
    [Header("References")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] public GameObject shop;
    [Header("Enemy")]
    [SerializeField] private GameObject enemy;
    [SerializeField] private LayerMask enemyLayer;

    public PlayerController playerController;
    private Vector2 moved;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    public static bool isShopOpen = false;
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

    private void OnEnable() {
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


    private void FixedUpdate() {
        if (isAttack == false) {
            Move();
        }
    }
    //esta funcion recibe los input para mover al player y tambien se encarga de las animaciones
    private void PlayerInput() {
        if (modeAttack == true) {
            moved = playerController.MovemenT.Move.ReadValue<Vector2>();
            myAnimator.SetFloat("MoveX", moved.x);
            myAnimator.SetFloat("MoveY", moved.y);
        }
    }
    //esta funcion mueve el rigidbody2d del player
    void Move() {
        rb.MovePosition(rb.position + moved * (moveSpeed * Time.fixedDeltaTime));
    }
    //esta funcion se encarga del ataque 
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
    //esta funcion termina el ataque 
    private void EndAttack(){
        myAnimator.SetBool("IsAttacking", false);
        isAttack= false;
    }
    //esta funcion se encarga de instaciar las balas donde se indique y con la direccion donde se haga click
    private void Shoot()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector2 direction = ((Vector2)mouseWorldPos - (Vector2)spawnPoint.position).normalized;
        
        GameObject newBullet = ObjectPoolManager.SpawnObject(bullet, spawnPoint.position, Quaternion.identity);
        var proj = newBullet.GetComponent<Projectile>();
        if (proj != null)
            proj.SetDirection(direction);
        
        newBullet.SetActive(true);
    }
    //esta funcion se encarga de que el player siga con la mirada al mouse
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