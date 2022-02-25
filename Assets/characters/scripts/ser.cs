using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ser : MonoBehaviour
{
    private GameObject burger;
    private BoxCollider2D boxCollider2d;
    private GameObject spermokula;
    private GameObject level;



    private Vector3 initialPosition;
    private Vector3 initialPosition2;

    public bool cheesed;
    private bool isSpawned;
    public bool isLaunched;

    private float facing_direction;
    private float launchTime;

    //  [SerializeField] float timeUntilDestruction = 5;
    [SerializeField] float spawnDistance = 0.1f;
    [SerializeField] float launchPower = 25;
    [SerializeField] float maxlaunchTime = 3;

    
    private void Awake()
    {
        burger = GameObject.Find("PlayerPrefab");
        spermokula = GameObject.Find("spermokula");
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        level = GameObject.Find("Level1-1");
        initialPosition2 = transform.position;
        initialPosition = spermokula.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == burger && collision.contacts[0].normal.x < -0.5 || collision.gameObject == burger && collision.contacts[0].normal.x > 0.5)
        {
            cheesed = true;
        }
    }






    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == burger)
        {
            cheesed = false;
        }
    }


    private void Update()
    {
        // RaycastHit2D raycastHit2d = Physics2D.Raycast(burger.transform.position, new Vector2(0, 1), 10);
        // Debug.DrawRay(burger.transform.position, Vector2.right, Color.green);

        if (Input.GetAxisRaw("Horizontal") != 0f)
        {
            facing_direction = Input.GetAxisRaw("Horizontal");
        }


        if (isLaunched)
        { // sprawdzanie czy wolno sie porusza i czy zresetowac mape
            launchTime += Time.deltaTime;
        }

        if (launchTime > maxlaunchTime)
        {
            spermokula.transform.position = initialPosition2;
            spermokula.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0);

            isLaunched = false;
            launchTime = 0;
        }


        //Debug.Log(facing_direction);

        if (facing_direction == -1)
        {
            RaycastHit2D raycastHit2d = Physics2D.Raycast(burger.transform.position, new Vector2(-5, 3));
            Debug.DrawRay(burger.transform.position, new Vector2(-5, 3), Color.green);
            if (Input.GetKeyDown(KeyCode.E))
            {
                launchTime = 0;
                spermokula.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
                spermokula.transform.position = new Vector3(burger.transform.position.x + (spawnDistance * facing_direction), burger.transform.position.y, 0);


                Vector2 directionToInitialPosition = initialPosition - (burger.transform.position - new Vector3(-5, 3));
                spermokula.GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * launchPower);
                isLaunched = true;
            }
        }

        if (facing_direction == 1)
        {
            RaycastHit2D raycastHit2d = Physics2D.Raycast(burger.transform.position, new Vector2(5, 3));
            Debug.DrawRay(burger.transform.position, new Vector2(5, 3), Color.green);
            if (Input.GetKeyDown(KeyCode.E))
            {
                launchTime = 0;
                spermokula.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
                spermokula.transform.position = new Vector3(burger.transform.position.x + (spawnDistance * facing_direction), burger.transform.position.y, 0);


                initialPosition = spermokula.transform.position;
                Vector2 directionToInitialPosition = initialPosition + (burger.transform.position + new Vector3(5, 3));
                spermokula.GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * launchPower);
                isLaunched = true;
            }
        }
    }

}