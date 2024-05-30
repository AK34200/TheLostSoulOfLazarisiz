using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class New_Movement : MonoBehaviour
{
    [Header("Movement booleans")]
    public bool canGlide = true;
    public bool canJump = true;
    public bool canMove = true;
    public bool canFlip = true;

    public bool canDash = true;

    [Header("Le reste")]
    [SerializeField] Animator Player_Animator;
    [SerializeField] private float Speed = 8f;
    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private float horizontal;
    private Vector2 move;
    [SerializeField] int power_JP;
    [SerializeField] bool jumped = true;
    [SerializeField] bool IsGliding = false;
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float fallSpeed = 0.3f;

    [Header("isGrounded")]
    public Transform groundCheck;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] float DataGravityScale;

    IEnumerator coroutine;
    private int move_Speed;

    void Start()
    {
        canDash = true;
        canJump = true;
        canFlip = true;
        canMove = true;
        canGlide = true;

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void Update()
    {
        rb.velocity = new Vector2(horizontal * Speed, rb.velocity.y);
            if (!isFacingRight && horizontal > 0f)
            {
                Flip();
            }
            else if (isFacingRight && horizontal < 0f)
            {
                Flip();
            }

        //Debug.Log(Player_Animator.GetBool("RUN"));

    }

    public bool IsGrounded()                                               // ============== JUMP : GROUND DETECTION [NEW]
    {
        canGlide = true;
        Debug.Log("efsefsdfsdf");
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        
    }


    private void FallFast()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = DataGravityScale * 1.5f;

            if (rb.velocity.y < -65)
            {
                rb.velocity = new Vector2(rb.velocity.x, -65);
            }
        }
    }


    private void Flip()                                                      // ============== FLIP
    {
        if(canFlip)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        Player_Animator.SetBool("RUN", false);

        {


            horizontal = context.ReadValue<Vector2>().x;
            Player_Animator.SetBool("RUN", horizontal != 0);

            if (context.canceled)
            {
                horizontal = 0;  // Reset horizontal movement
                Player_Animator.SetBool("RUN", false);
            }

        }

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (canJump)
        {
            Debug.Log("ISGrounded = "+IsGrounded());
            if(!IsGrounded())
            {
                Player_Animator.SetBool("JUMP", true);
            } else
            {
                Player_Animator.SetBool("JUMP", false);
            }

            if (context.performed && IsGrounded())  // Si je saute et que je suis au sol
            {
                rb.velocity = new Vector2(rb.velocity.x, power_JP);
                Player_Animator.SetBool("JUMP", true);
                
            }

            if (context.canceled && rb.velocity.y > 0f && !IsGrounded())        // Si je lâche le saut
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * fallSpeed);
                Player_Animator.SetBool("JUMP", false);
            }

            if (context.performed && !IsGrounded() && !IsGliding && canGlide)   // Si je suis en chute et que je ne plane pas
            {
                rb.gravityScale = 0.2f;
                IsGliding = true;
                canGlide = true;
                Player_Animator.SetBool("JUMP", true);
            }

            if (context.canceled && !IsGrounded() && IsGliding)     // Si je suis en chute et que je plane
            {
                rb.gravityScale = 1f;
                IsGliding = false;
                canGlide = false;
                Player_Animator.SetBool("JUMP", false);
                if (IsGrounded())
                {
                    //rb.gravityScale = 1f;
                    canGlide = true;
                    IsGliding = false;
                }
            }
            if (context.canceled)
            {
                rb.gravityScale = 1f;
                canGlide = true;
                IsGliding = false;
            }


        }
    }
    public void Run()
    {
        Player_Animator.SetBool("RUN", true);
    }
}
