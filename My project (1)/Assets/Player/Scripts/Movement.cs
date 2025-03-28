using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private PlayerController playerController;
    private Vector2 moved;
    private Rigidbody2D rb;

    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;

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
    }
    void Move()
    {
        rb.MovePosition(rb.position + moved * (moveSpeed * Time.fixedDeltaTime));
        if (moved.x < 0f)
        {
            mySpriteRenderer.flipX = false;
        }else {
        mySpriteRenderer.flipX=true;      
        }
    }

}