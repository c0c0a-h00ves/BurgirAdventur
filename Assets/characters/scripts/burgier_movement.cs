using UnityEngine;

public class burgier_movement : MonoBehaviour
{
// Zmienne ktore modyfikuje silnik
    float horizontal_direction = 0f;
    float horizontal_move = 0f;
    Vector3 complete_move;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;
    float jump;

// Zmienne ktore modyfikujemy my (w przyszlosci do beda stale)
// serialize daje ze mozna zmieniac zmienna w komponencie skryptu
    [SerializeField] float speed = 2f;
    [SerializeField] float jumpVelocity = 200f;
    [SerializeField] private LayerMask platformslayerMask;

    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    private Vector3 m_Velocity = Vector3.zero;

    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        jump = rigidbody2d.velocity.y;

    }

    void Update(){
        horizontal_direction = Input.GetAxisRaw("Horizontal");
        
        horizontal_move = horizontal_direction * speed;  
        complete_move = new Vector2(horizontal_move, rigidbody2d.velocity.y);

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2d.AddForce(new Vector2(0f, jumpVelocity));
        }

        rigidbody2d.velocity = Vector3.SmoothDamp(rigidbody2d.velocity, complete_move, ref m_Velocity, m_MovementSmoothing);
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, platformslayerMask);
        //Debug.Log(raycastHit2d);
        return raycastHit2d.collider != null;
    }

}
