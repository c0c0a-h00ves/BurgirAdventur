using UnityEngine;

public class burgier_movement : MonoBehaviour
{
    [Header("Horizontal Movment")]
    public float moveSpeed = 10f;
    public Vector2 direction;

    [Header("Components")]
    public Rigidbody2D rb;

    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linerDrag = 4f;

    private void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        moveCharacter(direction.x);
        modifyPhysics();
    }

    void moveCharacter(float horizontal)
    {
        rb.AddForce(Vector2.right * horizontal * moveSpeed);

        if(Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }

    void modifyPhysics()
    {
        if(Mathf.Abs(direction.x) < 0.4f)
        {
            rb.drag = linerDrag;
        }
        else
        {
            rb.drag = 0f;
        }
    }
}
