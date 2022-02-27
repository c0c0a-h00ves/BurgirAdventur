using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kotlet : MonoBehaviour
{
    private Animator anim;
    private float timer = 0f;
    private float facing_direction;
    private Vector3 collisionSide;
    private Vector3 vectorDifference;
    private bool isSpawned;
    public bool isOnKotlet;
    private Vector2 boxCoordinates;


    [SerializeField] float jumpVelocity;
    [SerializeField] float timeUntilDestruction = 5f;
    [SerializeField] float spawnDistance = 0.5f;

    private void Start()
    {
        anim = GameObject.Find("burger").GetComponent<Animator>();
    }
    //wykrywanie kolizji
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PlayerPrefab")
        {
            vectorDifference = collision.gameObject.transform.position - transform.position;
            collisionSide = vectorDifference.normalized;
            if (collisionSide.x == -1.0f || collisionSide.x == 1.0f)
            {
                collision.gameObject.transform.position = transform.position;
            }

            isOnKotlet = true;
            //dodanie gornego velocity
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * jumpVelocity;
            //odpalenie animacji skoku i wylaczenie animacji chodzenia
            anim.SetTrigger("jump");
            anim.SetBool("isWalking", false);
        }
    }
    public void SpawnKotlet(GameObject burger)
    {
        //coordy gdzie bedzie kotlet
        boxCoordinates = new Vector2(burger.transform.position.x + (spawnDistance * facing_direction), burger.transform.position.y);
        //sprawdzenie czy nie probuje sie zrespic w srodku innego collidera i zrespienie kotleta
        if (!Physics2D.OverlapBox(boxCoordinates, transform.localScale, transform.eulerAngles.z) && isOnKotlet == false)
        {
            gameObject.SetActive(true);
            //jak patrzy w prawo to sie respi po prawo jak nie to po lewo
            transform.position = new Vector3(burger.transform.position.x + (spawnDistance * facing_direction), burger.transform.position.y, 0);
            isSpawned = true;
            timer = 0;
        }
    }
    void Update()
    {
        //sprawdzenie czy kierunek jest w lewo czy prawo
        if (Input.GetAxisRaw("Horizontal") != 0f)
            facing_direction = Input.GetAxisRaw("Horizontal");
        //liczenie czasu do znikniecia kotleta 
        if (isSpawned)
        {
            timer += Time.deltaTime;
            if(timer >= timeUntilDestruction)
            {
                gameObject.SetActive(false);
                timer = 0f;
                isSpawned = false;
            }
        }
    }
}
