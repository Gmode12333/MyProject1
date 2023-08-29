using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveInput;
    public float moveSpeed;
    public float jumpForce;
    public bool isFacingRight;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private AudioClip _jump;
    [SerializeField] private AudioClip _dash;
    private Rigidbody2D rb;
    private Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerMove();
        Flip();
        Jump();
    }
    private void PlayerMove()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2 (moveInput * moveSpeed, rb.velocity.y);
        anim.SetFloat("Move", Mathf.Abs(moveInput));
    }
    private void Flip()
    {
        if (isFacingRight && moveInput < 0f || !isFacingRight && moveInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            anim.SetBool("Jump", true);
            SoundManager.Instance.PlaySound(_jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        anim.SetBool("Jump", !IsGrounded());
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
