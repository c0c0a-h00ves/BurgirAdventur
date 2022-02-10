using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kotlet : MonoBehaviour
{
    GameObject burger;
    GameObject level;
    Animator anim;
    float timer = 0f;
    Vector2 initialPosition;
    bool isSpawned;

    [SerializeField] float jumpVelocity;
    [SerializeField] float timeUntilDestruction = 5;
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.position = new Vector3(burger.transform.position.x + 0.5f, burger.transform.position.y, 0);
            isSpawned = true;
        }
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
