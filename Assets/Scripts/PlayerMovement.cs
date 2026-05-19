using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;

    [Header("Jump")]
    public float jumpForce = 14f;
    public int maxJumps = 2;
    float doubleJumpTimer;

    [Header("Wall Slide")]
    public float wallSlideSpeed = 2f;
    public float wallJumpForceX = 10f;
    public float wallJumpForceY = 14f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Wall Check")]
    public Transform wallCheck;
    public float wallCheckDistance = 0.5f;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    DeathMenu deathMenu;

    float moveInput;

    bool isGrounded;
    bool isTouchingWall;
    bool isWallSliding;

    bool doubleJumpTriggered;

    int jumpsRemaining;
    public AudioClip jumpSound;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        deathMenu = FindFirstObjectByType<DeathMenu>();
      

        jumpsRemaining = maxJumps;
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        CheckGround();
        CheckWall();

        HandleJump();
        HandleWallSlide();

        if (doubleJumpTimer > 0)
        {
            doubleJumpTimer -= Time.deltaTime;
        }
        HandleAnimations();
        Flip();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(
            moveInput * moveSpeed,
            rb.linearVelocity.y
        );
    }

    void HandleJump()
    {
        if (isGrounded && rb.linearVelocity.y <= 0)
        {
            jumpsRemaining = maxJumps;
        }

        if (Input.GetButtonDown("Jump"))
        {
            // wall jump
            if (isWallSliding)
            {
                float direction = -Mathf.Sign(transform.localScale.x);

                rb.linearVelocity = new Vector2(
                    direction * wallJumpForceX,
                    wallJumpForceY
                );

                return;
            }

            // normal jump
            if (jumpsRemaining > 0)
            {
                rb.linearVelocity = new Vector2(
                    rb.linearVelocity.x,
                    jumpForce
                );
                
                AudioSource.PlayClipAtPoint(jumpSound, transform.position);
                // double jump
                if (!isGrounded && jumpsRemaining == 1)
                {
                    doubleJumpTimer = 0.2f;
                }

                jumpsRemaining = Mathf.Max(
                    jumpsRemaining - 1,
                    0
                );
            }
        }
    }

    void HandleWallSlide()
    {
        isWallSliding = false;

        if (isTouchingWall && !isGrounded && moveInput != 0)
        {
            isWallSliding = true;

            jumpsRemaining = maxJumps;

            if (rb.linearVelocity.y < -wallSlideSpeed)
            {
                rb.linearVelocity = new Vector2(
                    rb.linearVelocity.x,
                    -wallSlideSpeed
                );
            }
        }
    }

    void HandleAnimations()
    {
        // wall slide
        if (isWallSliding)
        {
            animator.Play("player_wall_slide");
            return;
        }

        // double jump
        if (doubleJumpTimer > 0)
        {
            animator.Play("player_double_jump");
            return;
        }

        // jump
        if (!isGrounded && rb.linearVelocity.y > 0.1f)
        {
            animator.Play("player_jump");
            return;
        }

        // fall
        if (!isGrounded && rb.linearVelocity.y < -0.1f)
        {
            animator.Play("player_fall");
            return;
        }

        // run
        if (Mathf.Abs(moveInput) > 0.1f)
        {
            animator.Play("player_run");
            return;
        }

        // idle
        animator.Play("player_idle");
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }

    void CheckWall()
    {
        Vector2 direction =
            transform.localScale.x > 0
            ? Vector2.right
            : Vector2.left;

        isTouchingWall = Physics2D.Raycast(
            wallCheck.position,
            direction,
            wallCheckDistance,
            groundLayer
        );
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;

        if (moveInput > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else if (moveInput < 0)
        {
            scale.x = -Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (
            other.CompareTag("Enemy") ||
            other.CompareTag("Hazard")
        )
        {
            Die();
        }
    }

    void Die()
    {
        deathMenu.ShowDeathMenu();
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(
                groundCheck.position,
                groundCheckRadius
            );
        }

        if (wallCheck != null)
        {
            Gizmos.color = Color.red;

            Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

            Gizmos.DrawLine( wallCheck.position, wallCheck.position + (Vector3)(direction * wallCheckDistance));
        }
    }
}