using UnityEngine;

public class burgier_movement : MonoBehaviour
{
// Zmienne ktore modyfikuje silnik
    float horizontal_direction = 0f;
    float horizontal_move = 0f;
    Vector3 complete_move;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;

// Zmienne ktore modyfikujemy my (w przyszlosci do beda stale)
// serialize daje ze mozna zmieniac zmienna w komponencie skryptu
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpVelocity = 3f;
    [SerializeField] private LayerMask platformslayerMask;

    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    void Update(){
        horizontal_direction = Input.GetAxisRaw("Horizontal");
        
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
        }
    }

    void FixedUpdate(){
        //mnożąc kierunek przez prędkość uzyskujemy ruch horyzontalny jaki chcemy wykonać       
        horizontal_move = horizontal_direction * speed;  
        complete_move = new Vector2(horizontal_move, 0f);
        rigidbody2d.AddForce(complete_move);
    
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, platformslayerMask);
        Debug.Log(raycastHit2d);
        return raycastHit2d.collider != null;
    }

}
