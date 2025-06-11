using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using System.Collections;


public class Movement : MonoBehaviour
{
    [Header("Attribute")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] public bool isAttack = false;
    [SerializeField] private float fireCooldown = 0.5f;
    [SerializeField] private int maxShotsBeforeReload = 14;
    [SerializeField] private float reloadTime = 2f;
    [Header("References")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] public GameObject shop;

    public PlayerController playerController;
    private Vector2 moved;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    public static bool isShopOpen = false;
    public static bool modeAttack = true;
    private Vector3 originalScale;
    private float lastFireTime = -Mathf.Infinity;
    readonly int Fire_hash = Animator.StringToHash("Fire");
    private int currentShots = 0;
    private bool isReloading = false;


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
        if (modeAttack == true && isShopOpen == false)
        {
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
        if (isReloading)
            return;

        if (Time.time < lastFireTime + fireCooldown)
            return;

        if (currentShots >= maxShotsBeforeReload)
        {
            StartCoroutine(Reload());
            return;
        }

        lastFireTime = Time.time;
        currentShots++;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = ((Vector2)mouseWorldPos - (Vector2)spawnPoint.position).normalized;

        GameObject newBullet = ObjectPoolManager.SpawnObject(bullet, spawnPoint.position, Quaternion.identity);
        var proj = newBullet.GetComponent<Projectile>();
        if (proj != null)
            proj.SetDirection(direction);

        newBullet.SetActive(true);
    }
    private IEnumerator Reload()
    {
        isReloading = true;


        // Aquí podrías añadir efectos visuales o de sonido de recarga

        yield return new WaitForSeconds(reloadTime);

        currentShots = 0;
        isReloading = false;
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