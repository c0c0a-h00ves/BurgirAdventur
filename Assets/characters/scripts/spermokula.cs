using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spermokula : MonoBehaviour
{
    private GameObject burger;
    private GameObject klocek;
    private GameObject ser;
    private GameObject level;

    private BoxCollider2D boxCollider2d;


    private Vector3 initialPosition;
    private Vector3 initialPosition2;
    private Vector3 initialPosition3;

    private float wielkosci;
    private float iks;
    private Collider2D colliderSera;
    private Vector3 sizeSera;
    private float iks2;
    private bool przyklejone;


    private float czas;
    [SerializeField] private float maxCzas = 5f;

    private Collider2D colliderKlocka;
    private Vector3 sizeKlocka;

    private void Awake()
    {
        burger = GameObject.Find("PlayerPrefab");
        ser = GameObject.Find("ser");
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        level = GameObject.Find("Level1-1");
        klocek = GameObject.Find("klocek");
        initialPosition = ser.transform.position;
        initialPosition2 = transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject == ser && collision.contacts[0].normal.x < -0.5 || collision.gameObject == ser && collision.contacts[0].normal.x > 0.5)
        {
            Debug.Log("ser");
            ser.GetComponent<ser>().isLaunched = false;
            czas = 0;
            transform.position = initialPosition2; // pozycja startowa poza map¹
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
        }


            if (collision.gameObject == klocek && collision.contacts[0].normal.x < -0.5 || collision.gameObject == klocek && collision.contacts[0].normal.x > 0.5) 
        {
           // wielkosci = klocek.GetComponent<Transform>().localScale.x;
           // iks = wielkosci / 2;
            Debug.Log(wielkosci);
            Debug.Log("sciana");
            initialPosition3 = transform.position; // to jest pozycja spermokulki jak trafi w œciane
            transform.position = initialPosition2; // wyjebanie sera poza mape
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0); // ustawienie szybkoœci kuli na 0


            colliderKlocka = klocek.GetComponent<Collider2D>(); // zdobycie wielkoœci klocka 
            sizeKlocka = colliderKlocka.bounds.size;
            iks = sizeKlocka.x/2; // wielkoœæ X'owa 

            colliderSera = ser.GetComponent<Collider2D>(); // zdobycie wielkoœci sera
            sizeSera = colliderSera.bounds.size;
            iks2 = sizeSera.x / 2; // wielkoœæ X'owa 


            if (collision.contacts[0].normal.x < -0.5)
            {   // przeniesienie sera na dan¹ pozycje
                ser.transform.position = new Vector2(klocek.transform.position.x - iks + iks2 - 0.005f, initialPosition3.y); 
            }
            if (collision.contacts[0].normal.x > 0.5)
            {   // przeniesienie sera na dan¹ pozycje
                ser.transform.position = new Vector2(klocek.transform.position.x + iks - iks2 + 0.005f, initialPosition3.y);
            }




            ser.GetComponent<ser>().isLaunched = false;
            przyklejone = true;
            czas = 0;
        }
    }

    private void Update()
    {
        if (przyklejone)
        { // sprawdzanie czy wolno sie porusza i czy zresetowac mape
            czas += Time.deltaTime;
            if (czas >= maxCzas)
            {
                ser.transform.position = initialPosition;
                
                czas = 0f;
                przyklejone = false;

            }
        }
    }
}