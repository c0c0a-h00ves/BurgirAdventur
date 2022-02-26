using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    private GameObject burger;
    private BoxCollider2D boxCollider2d;
    private GameObject text;
    private GameObject HexagonPointedTop;

    private Collider2D colliderCheckpointa;



    [SerializeField] public float odleglosc = 0.11f;

    private void Awake()
    {
        burger = GameObject.Find("PlayerPrefab");
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        text = GameObject.Find("checkpoint-text");
        HexagonPointedTop = GameObject.Find("HexagonPointedTop");
        // initialPositionBurger = burger.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == burger)
        {
            //spawnPoint = transform.position+new Vector3(0,odleglosc);

            burger.GetComponent<checkpointinfo>().aktywnyCheckpoint = transform.position + new Vector3(0, odleglosc);

            colliderCheckpointa = GetComponent<Collider2D>(); // zdobycie wielkoœci klocka 
            //sizeCheckPointa = colliderCheckpointa.bounds.size;
            //wielkosci = new Vector2(sizeCheckPointa.x, sizeCheckPointa.y);

            //Debug.Log(spawnPoint);

           // burger.transform.position = new Vector2(spawnPoint.x,spawnPoint.y+odleglosc);
            HexagonPointedTop.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            GetComponent<SpriteRenderer>().color = Color.green;
          //  text.text = ("chuj");
            Debug.Log("chuj"); 
        }
    }





}
