using UnityEngine;
using UnityEngine.InputSystem;

public class BoatMovements : MonoBehaviour
{
    public InputActionReference moveRef;
    public float playerSpeed;
    public Vector3 direction;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public Transform playerTransform;
    public bool facingRight = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = GetComponent<Transform>();

        moveRef.action.started += MoveBoat;
        moveRef.action.performed += MoveBoat;
        moveRef.action.canceled += MoveBoat;
    }

    private void FixedUpdate()
    {
        playerTransform.position += new Vector3(direction.x * playerSpeed * Time.deltaTime, direction.y * playerSpeed * Time.deltaTime, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (direction.x < 0 && facingRight)
        {
            Flip();
        } 
        else if (direction.x > 0 && !facingRight)
        {
            Flip();
        }
    }

    void MoveBoat(InputAction.CallbackContext ctx)
    {
        Debug.Log(ctx.ReadValue<Vector2>());
        if (!ctx.canceled)
        {
            direction = ctx.ReadValue<Vector2>();
            Debug.Log("dir = " + direction);
            // playerAnimator.SetTrigger("IsWalking");
        }
        else
        {
            Debug.Log(ctx.ReadValue<Vector2>());
            direction = new Vector2(0,0);
            // playerAnimator.SetTrigger("StopWalking");
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
