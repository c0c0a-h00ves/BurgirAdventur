using UnityEngine;
using UnityEngine.SceneManagement;

public class burgier_death : MonoBehaviour
{
    public bool isDead = false;
    private float _timer = 0;
    private Rigidbody2D _rigidBody;

    [SerializeField] private float thrust = 1f;
    public void Death()
    {
        _rigidBody = transform.GetComponent<Rigidbody2D>();
        _rigidBody.velocity = Vector2.up * thrust;
        GetComponent<BoxCollider2D>().enabled = false; // jak bedzie polygon collider to zmienic
        GetComponent<burgier_movement>().enabled = false;
    }
    private void Update()
    {
        if (_timer >= 2)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
        if (isDead == true)
        {
            _timer += Time.deltaTime;
        }
    }
}
