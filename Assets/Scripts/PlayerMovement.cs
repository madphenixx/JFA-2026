using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset action;
    public InputAction Player;
    public InputActionReference MoveRef;
    public InputActionReference JumpRef;
    public InputActionReference SprintRef;
    public InputActionReference DodgeRef;
    public float playerSpeed;
    public float basePlayerSpeed;
    public float direction;
    public float sprintSpeed = 4f;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    // public Animator playerAnimator;
    public Transform playerTransform;
    public bool facingRight = true;

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // playerAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = GetComponent<Transform>();
    }
    private void OnEnable()
    { 
        action.Enable();
    }
    private void OnDisable()
    {
        action.Disable();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * playerSpeed * Time.deltaTime, rb.linearVelocityY);

        MoveRef.action.started += Move;
        MoveRef.action.performed += Move;
        MoveRef.action.canceled += Move;

        SprintRef.action.started += Sprint;
        //SprintRef.action.performed += Sprint;
        SprintRef.action.canceled += Sprint;

        DodgeRef.action.started += Dodge;
        //DodgeRef.action.performed += Dodge;
        DodgeRef.action.canceled += Dodge;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction < 0 && facingRight)
            Flip();
        else if (direction > 0 && !facingRight)
            Flip();
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

    void Sprint(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled)
        {
            playerSpeed = playerSpeed * sprintSpeed;
            // playerAnimator.SetTrigger("IsRunning");
        }
        if (ctx.canceled)
        {
            playerSpeed = basePlayerSpeed;
            // playerAnimator.SetTrigger("StopRunning");
        }
    }

    void Dodge(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled)
        {
            direction = -1;
            playerSpeed = playerSpeed * sprintSpeed;
            // playerAnimator.SetTrigger("IsRunning");
        }
        if (ctx.canceled)
        {
            direction = ctx.ReadValue<float>();
            playerSpeed = basePlayerSpeed;
            // playerAnimator.SetTrigger("StopRunning");
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
