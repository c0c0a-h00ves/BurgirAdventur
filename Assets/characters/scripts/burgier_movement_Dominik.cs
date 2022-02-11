using UnityEngine;

public class burgier_movement_Dominik : MonoBehaviour
{
    // ustawiane przez nas
    [Header("Horizontal Movement")]
    public float moveSpeed = 5f;
    [SerializeField] private float jump_backward_drag = 2.5f;
    [Range(0, 0.3f)] [SerializeField] private float m_MovementSmoothing = 0.05f;

    [Header("Vertical Movement")]
    public float jumpSpeed = 5f;
    public float jumpDelay = 0.25f;

    [Header("Components")]
    public Rigidbody2D rb;
    private Animator anim;
    
    [Header("Physics")]
    public float max_speed = 7f;
    public float linerDrag = 10f;
    public float gravity = 1;
    public float fallMultiplier = 2f;

    [Header("Collision")]
    public float groundLength = 0.6f;
    [SerializeField] private LayerMask platformslayerMask;

// tymi zarządza skrypt
    private BoxCollider2D boxCollider2d;
    private Vector3 complete_move;
    private float jump_direction;
    private Vector2 counter_drag;
    public Vector2 direction;
    private float jumpTimer;
    private Vector3 m_Velocity = Vector3.zero;
    private GameObject burger;
    


    private void Awake()
    {
        burger = GameObject.Find("burger");
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        anim = burger.GetComponent<Animator>();

    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = Time.time + jumpDelay;
        }

        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));


        //               Animacja            //
        if (direction.x != 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("BurgirMovment_Jump") && IsGrounded())
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        if(Input.GetButtonDown("Jump") && !anim.GetCurrentAnimatorStateInfo(0).IsName("BurgirMovment_Jump"))
        {
            anim.SetBool("isWalking", false);
            anim.SetTrigger("jump");
        }
    }

    private void FixedUpdate()
    {
        moveCharacter(direction.x);

        if (jumpTimer > Time.time && IsGrounded())
        {
            Jump();
        }

        modifyPhysics();
    }

    void moveCharacter(float horizontal)
    {
        complete_move = new Vector3(horizontal * moveSpeed, rb.velocity.y);
        if(!IsGrounded() && jump_direction != horizontal){
            counter_drag = new Vector2(Mathf.Abs(rb.velocity.x), 0f);
            rb.AddForce(counter_drag *jump_backward_drag); //Vector2.right = [1, 0]
        }
        
        rb.velocity =Vector3.SmoothDamp(rb.velocity, complete_move, ref m_Velocity, m_MovementSmoothing,  max_speed);
        
    }

    void Jump()
    {
        jump_direction = direction.x;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse); // Pierwszy parametr kierunek i moc, drugi parametr oznacza ze sie robi to instant jak zupka
        jumpTimer = 0;
    }

    void modifyPhysics()
    {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

        if (IsGrounded())
        {
            if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
            {
                rb.drag = linerDrag;
            }
            else
            {
                rb.drag = 0f;
            }
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = gravity;
            rb.drag = linerDrag * 0.15f;
            
            if(rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            } else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }

  
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, platformslayerMask);
        //Debug.Log(raycastHit2d);
        return raycastHit2d.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundLength);
    }
}
