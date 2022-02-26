using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    private GameObject burger;
    private BoxCollider2D boxCollider2d;
    private Collider2D colliderCheckpointa;
    [SerializeField] public float odleglosc = 0.11f;

    private void Awake()
    {
        burger = GameObject.Find("PlayerPrefab");
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == burger)
        {
            // przekazanie spawnpointa do checkpointinfo.cs
            burger.GetComponent<checkpointinfo>().aktywnyCheckpoint = transform.position + new Vector3(0, odleglosc);
            colliderCheckpointa = GetComponent<Collider2D>(); // zdobycie wielkoœci klocka 
            GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
