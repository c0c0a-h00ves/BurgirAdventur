using UnityEngine;
using UnityEngine.SceneManagement;

public class burgier_death : MonoBehaviour
{
    burger_deathCount deathCountScript;

    public bool isDead = false;
    private float _timer = 0;
    private Rigidbody2D _rigidBody;

    [SerializeField] private float thrust = 1f;
    [SerializeField] private float _timeToResetScene = 2;
    private void Start()
    {
        deathCountScript = GameObject.Find("deathCounterObject").GetComponent<burger_deathCount>();
    }

    public void Death()
    {
        //robienie ze burger podskakuje po smierci
        _rigidBody = transform.GetComponent<Rigidbody2D>();
        _rigidBody.velocity = Vector2.up * thrust;

        //wylaczenie collidera
        GetComponent<BoxCollider2D>().enabled = false; // jak bedzie polygon collider to zmienic
        //wylaczenie poruszania sie po smierci
        GetComponent<burgier_movement>().enabled = false;
    }
    private void Update()
    {
        if (_timer >= _timeToResetScene)
        {
            //przeladowanie sceny
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
        if (isDead == true)
        {
            _timer += Time.deltaTime;
        }
    }
}
