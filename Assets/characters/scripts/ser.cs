using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ser : MonoBehaviour
{
    private GameObject burger;

    private void Awake()
    {
        burger = GameObject.Find("PlayerPrefab");
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
 
        if (collision.gameObject == burger && collision.contacts[0].normal.x < -0.5 || collision.gameObject == burger && collision.contacts[0].normal.x > 0.5)
        {
            Debug.Log("chuj");
            burger.GetComponent<Rigidbody2D>().gravityScale = 0;
            Debug.Log(burger.GetComponent<Rigidbody2D>().gravityScale);

        }

    }

}