using UnityEngine;

public class burgier_movement : MonoBehaviour
{
// Zmienne ktore modyfikuje silnik
    float horizontal_direction = 0f;
    float horizontal_move = 0f;
    Vector3 complete_move;

// Zmienne ktore modyfikujemy my (w przyszlosci do beda stale)
// serialize daje ze mozna zmieniac zmienna w komponencie skryptu
    [SerializeField] float speed = 1f;

    void Update(){
        horizontal_direction = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate(){
        Vector3 pozycja_temp = transform.position;
        
        //łapiemy x aktualnej pozycji (horizontal) i nakładamy na niego wykonany ruch
        horizontal_move = pozycja_temp[0] + (horizontal_direction * speed);

        //wrzucamy horizontal i vertical do nowego vectora, łącząc je w kompletny ruch
        complete_move = new Vector3(horizontal_move, pozycja_temp[1]);
        transform.position = complete_move;

    }
}
