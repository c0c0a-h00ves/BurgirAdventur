using UnityEngine;
using UnityEngine.SceneManagement;

public class burgier_death : MonoBehaviour
{
    burger_deathCount deathCountScript;
    private GameObject burger;
    public bool isDead = false;
    private float _timer = 0;
    private Rigidbody2D _rigidBody;

    private GameObject checkpoint;

    [SerializeField] private float thrust = 1f;
    [SerializeField] private float _timeToResetScene = 2;


    // zmienne z checkpoint.cs
    public Vector3 spawnPoint;
    public Collider2D colliderCheckpointa;

    private void Awake()
    {
        burger = GameObject.Find("PlayerPrefab");
        deathCountScript = GameObject.Find("deathCounterObject").GetComponent<burger_deathCount>();
        checkpoint = GameObject.Find("checkpoint");
    }

    public void Death()
    {
        //robienie ze burger podskakuje po smierci
        _rigidBody = transform.GetComponent<Rigidbody2D>();
        _rigidBody.velocity = Vector2.up * thrust;
        deathCountScript.addDeath();
        //wylaczenie collidera
        GetComponent<BoxCollider2D>().enabled = false; // jak bedzie polygon collider to zmienic
        //wylaczenie poruszania sie po smierci
        GetComponent<burgier_movement>().enabled = false;
    }
    private void Update()
    {
        if (_timer >= _timeToResetScene)
        {
            // tu powinien byc skrypt na resetowanie wszystkiego tf




            //przeladowanie sceny
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);

            //burger.transform.position = checkpoint.GetComponent<checkpoint>().spawnPoint;
            Debug.Log(burger.GetComponent<checkpointinfo>().aktywnyCheckpoint);
        }
        if (isDead == true)
        {
            _timer += Time.deltaTime;
        }
    }
}
