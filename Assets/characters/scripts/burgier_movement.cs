using UnityEngine;

public class burgier_movement : MonoBehaviour
{
// Zmienne ktore modyfikuje silnik
    float horizontal_direction = 0f;
    float horizontal_move = 0f;
    Vector3 complete_move;
    bool isAllowed_right = true;
    bool isAllowed_left = true;

// Zmienne ktore modyfikujemy my (w przyszlosci do beda stale)
// serialize daje ze mozna zmieniac zmienna w komponencie skryptu
    [SerializeField] float speed = 1f;

    void Update(){
        horizontal_direction = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate(){
        Vector3 pozycja_temp = transform.position;

        horizontal_move = horizontal_direction * speed;
      
        complete_move = new Vector2(horizontal_move, 0f);
        GetComponent<Rigidbody2D>().AddForce(complete_move );
    
    
    }

}
