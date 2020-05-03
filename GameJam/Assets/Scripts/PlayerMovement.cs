using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;

    public bool isJumpig;
    public bool isGrounded;

    public Transform groundChek;
    public float groundChekRadius;
    public LayerMask collisionLayer;
    public float jumpForce;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;

    void Update()
    {




        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumpig = true;
        }

        Flip(rb.velocity.x);


        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
    }


    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundChek.position, groundChekRadius, collisionLayer);
        MovePlayer(horizontalMovement);
    }
    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);

        if (isJumpig == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumpig = false;
        }

    }


    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundChek.position, groundChekRadius);
    }
}
