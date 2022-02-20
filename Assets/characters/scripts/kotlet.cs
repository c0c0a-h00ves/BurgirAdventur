using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kotlet : MonoBehaviour
{
    private GameObject burger;
    private GameObject level;
    private Animator anim;
    private float timer = 0f;
    private Vector2 initialPosition;
    private float facing_direction;
    private bool isSpawned;
    private Vector2 boxCoordinates;
    private Vector2 boxSize;


    [SerializeField] float jumpVelocity;
    [SerializeField] float timeUntilDestruction = 5;
    [SerializeField] float spawnDistance = 0.5f;

    private void Start()
    {
        initialPosition = transform.position;
        burger = GameObject.Find("PlayerPrefab");
        level = GameObject.Find("Level1-1");
        anim = GameObject.Find("burger").GetComponent<Animator>();
    }
    //wykrywanie kolizji
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //sprawdzenie czy kolizja jest z burgerem i czy nastapila od gory
        if(collision.gameObject == burger && collision.contacts[0].normal.y < -0.5)
        {
            //dodanie gornego velocity
            burger.GetComponent<Rigidbody2D>().velocity = transform.up * jumpVelocity;
            //odpalenie animacji skoku i wylaczenie animacji chodzenia
            anim.SetTrigger("jump");
            anim.SetBool("isWalking", false);
        }
        if(collision.gameObject == level)
        {
            Debug.Log("ddddd");
            transform.position = initialPosition;
        }

    }


    void Update()
    {
        //coordy gdzie bedzie kotlet
            boxCoordinates = new Vector2(burger.transform.position.x + (spawnDistance * facing_direction), burger.transform.position.y);
        //rozmiar kotleta
        boxSize = new Vector2(transform.position.x, transform.position.y);
        //sprawdzenie czy kierunek jest w lewo czy prawo
        if (Input.GetAxisRaw("Horizontal") != 0f)
            facing_direction = Input.GetAxisRaw("Horizontal");
        //sprawdzenie czy nie probuje sie zrespic w srodku innego collidera i zrespienie kotleta
        if (Input.GetKeyDown(KeyCode.Q) && !Physics2D.OverlapBox(boxCoordinates, transform.localScale, transform.eulerAngles.z))
        {
            //jak patrzy w prawo to sie respi po prawo jak nie to po lewo
               transform.position = new Vector3(burger.transform.position.x + (spawnDistance * facing_direction), burger.transform.position.y, 0);
            isSpawned = true;
            timer = 0;
        }
        //liczenie czasu do znikniecia kotleta 
        if (isSpawned)
        {
            timer += Time.deltaTime;
            if(timer >= timeUntilDestruction)
            {
                transform.position = initialPosition;
                timer = 0f;
                isSpawned = false;
            }
        }
    }
}
