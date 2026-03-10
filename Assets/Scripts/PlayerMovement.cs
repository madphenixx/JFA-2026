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
    public float dodgeSpeed = 8f;
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


        MoveRef.action.started += Move;
        MoveRef.action.performed += Move;
        MoveRef.action.canceled += Move;

        SprintRef.action.started += Sprint;
        //SprintRef.action.performed += Sprint;
        SprintRef.action.canceled += Sprint;

        DodgeRef.action.started += Dodge;
        DodgeRef.action.performed += Dodge;
        DodgeRef.action.canceled += Dodge;
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
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerSpeed);
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
            // playerAnimator.SetTrigger("IsRunning");
        }
    }


    private IEnumerator DodgeTime()
    {
        direction = -1;
        while(direction < 0)
        {
            direction += 5*Time.deltaTime;
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
