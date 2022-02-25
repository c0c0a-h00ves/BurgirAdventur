using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kotlet : MonoBehaviour
{
    private GameObject burger;
    private Animator anim;
    private float timer = 0f;
    private Vector2 initialPosition;
    private float facing_direction;
    private bool isSpawned;
    public bool isOnKotlet;
    private Vector2 boxCoordinates;


    [SerializeField] float jumpVelocity;
    [SerializeField] float timeUntilDestruction = 5f;
    [SerializeField] float spawnDistance = 0.5f;

    private void Start()
    {
        initialPosition = transform.position;
        burger = GameObject.Find("PlayerPrefab");
        anim = GameObject.Find("burger").GetComponent<Animator>();
    }
    //wykrywanie kolizji
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == burger)
        {
            isOnKotlet = true;
            //dodanie gornego velocity
            burger.GetComponent<Rigidbody2D>().velocity = transform.up * jumpVelocity;
            //odpalenie animacji skoku i wylaczenie animacji chodzenia
            anim.SetTrigger("jump");
            anim.SetBool("isWalking", false);
        }
    }

    void Update()
    {
        //coordy gdzie bedzie kotlet
        boxCoordinates = new Vector2(burger.transform.position.x + (spawnDistance * facing_direction), burger.transform.position.y);
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
