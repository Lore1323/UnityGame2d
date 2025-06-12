using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 1f;

    public AudioSource source;
    public AudioClip footStepClip;

    private PlayerController playerController;
    private Vector2 moved;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;

    public bool canMove = true;
    public static bool isShopOpen = false;
    public static bool modeAttack = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;

        playerController = new PlayerController();
    }

    private void OnEnable()
    {
        playerController.Enable();
    }

    private void OnDisable()
    {
        playerController.Disable();
    }

    private void Update()
    {
        if (!isShopOpen)
        {
            PlayerInput();
            AdjustPlayerFacingDirection();
            HandleFootstepSound(); // 🎧 Reproducir sonido aquí
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void PlayerInput()
    {
        if (modeAttack)
        {
            moved = playerController.MovemenT.Move.ReadValue<Vector2>();
            animator.SetFloat("MoveX", moved.x);
            animator.SetFloat("MoveY", moved.y);
        }
    }

    private void Move()
    {
        if (!AttackHandler.isAttacking && canMove)
        {
            rb.MovePosition(rb.position + moved * (moveSpeed * Time.fixedDeltaTime));
        }
    }

    // 🎧 Lógica del sonido de pasos
    private void HandleFootstepSound()
    {
        bool isMoving = moved != Vector2.zero && canMove && !AttackHandler.isAttacking;

        if (isMoving)
        {
            if (!source.isPlaying)
            {
                source.clip = footStepClip;
                source.loop = true;
                source.Play();
            }
        }
        else
        {
            if (source.isPlaying && source.clip == footStepClip)
            {
                source.Stop();
            }
        }
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x && !spriteRenderer.flipX)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        else
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
    }
}
