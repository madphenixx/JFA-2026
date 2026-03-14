using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionReference moveRef;
    public InputActionReference jumpRef;
    public InputActionReference sprintRef;
    public InputActionReference dodgeRef;
    public float playerSpeed;
    public float basePlayerSpeed;
    public float jumpForce = 10;
    public float direction;
    public float sprintSpeed = 4f;
    public float dodgeSpeed = 8f;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    // public Animator playerAnimator;
    public Transform playerTransform;
    public bool facingRight = true;
    public bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // playerAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = GetComponent<Transform>();

        moveRef.action.started += Move;
        moveRef.action.performed += Move;
        moveRef.action.canceled += Move;

        jumpRef.action.started += Jump;
        jumpRef.action.canceled += Jump;

        sprintRef.action.started += Sprint;
        sprintRef.action.canceled += Sprint;

        dodgeRef.action.started += Dodge;
        dodgeRef.action.canceled += Dodge;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * playerSpeed * Time.deltaTime, rb.linearVelocityY);
    }

    // Update is called once per frame
    void Update()
    {
        if (direction < 0 && facingRight)
        {
            Flip();
        } 
        else if (direction > 0 && !facingRight)
        {
            Flip();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void Move(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled)
        {
            direction = ctx.ReadValue<float>();
            // playerAnimator.SetTrigger("IsWalking");
        }
        else
        {
            direction = 0;
            // playerAnimator.SetTrigger("StopWalking");
        }
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if(!ctx.canceled && isGrounded)
        {
            // playerAnimator.SetTrigger("JumpUp");
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
        else
        {
            // playerAnimator.SetTrigger("JumpDown");
            rb.AddForce(new Vector2(0f, -jumpForce), ForceMode2D.Impulse);
        }
    }

    void Sprint(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            playerSpeed = playerSpeed * sprintSpeed;
            StartCoroutine(SprintTime());
            // playerAnimator.SetTrigger("IsRunning");
        }
    }

    void Dodge(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            playerSpeed = basePlayerSpeed * dodgeSpeed;
            StartCoroutine(DodgeTime());
            // playerAnimator.SetTrigger("IsDodging");
        }
    }

    private IEnumerator DodgeTime()
    {
        direction = -1;
        while(direction < 0)
        {
            direction += 5 * Time.deltaTime;
            yield return null;
        }
        direction = 0;
        playerSpeed = basePlayerSpeed;
    }

    private IEnumerator SprintTime()
    {
        float time = -1;
        while (time < 0)
        {
            time += 5 * Time.deltaTime;
            yield return null;
        }
        playerSpeed = basePlayerSpeed;
    }

    void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
