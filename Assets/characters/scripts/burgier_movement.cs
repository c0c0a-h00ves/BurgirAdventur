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
    float speed;
    float jump_direction;

// Zmienne ktore modyfikujemy my (w przyszlosci do beda stale)
// serialize daje ze mozna zmieniac zmienna w komponencie skryptu
    [SerializeField] float input_speed = 2f;
    [SerializeField] float jumpVelocity = 200f;
    [SerializeField] private LayerMask platformslayerMask;

    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    private Vector3 m_Velocity = Vector3.zero;

    private void Awake()
    {
        speed = input_speed;
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        jump = rigidbody2d.velocity.y;

    }

    void Update(){
        horizontal_direction = Input.GetAxisRaw("Horizontal");
        
        horizontal_move = horizontal_direction * speed;  
        complete_move = new Vector2(horizontal_move, rigidbody2d.velocity.y);
        if(!IsGrounded() && horizontal_direction!=jump_direction){
            speed = input_speed / 2f;
        }
        else{
            speed = input_speed;
        }
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            jump_direction = horizontal_direction;
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
