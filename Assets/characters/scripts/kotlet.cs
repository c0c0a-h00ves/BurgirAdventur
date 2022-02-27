using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kotlet : MonoBehaviour
{
    private Animator anim;
    private Vector3 collisionSide;
    private Vector3 vectorDifference;
    public bool isSpawned;
    private float speed = 5f;
    public bool sideHit = false;
    private float dist;
    GameObject burger;

    [SerializeField] float jumpVelocity;

    private void Start()
    {
        anim = GameObject.Find("burger").GetComponent<Animator>();
    }
    //wykrywanie kolizji
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PlayerPrefab")
        {
            Debug.Log("gfhusfdgh");
            vectorDifference = collision.gameObject.transform.position - transform.position;
            collisionSide = vectorDifference.normalized;
            burger = collision.gameObject;
            burger.GetComponent<spawn_kotlet>().isOnKotlet = true;
            if (collisionSide.x == -1.0f || collisionSide.x == 1.0f)
            {
                sideHit = true;
                burger.GetComponent<burgier_movement>().enabled = false;
            }
            else if(collisionSide.y > 0.5f)
                burger.GetComponent<spawn_kotlet>().Bounce();
        }
    }
    /*public void Bounce()
    {
        burger.GetComponent<burgier_movement>().enabled = true;
        sideHit = false;
        //dodanie gornego velocity
        burger.GetComponent<Rigidbody2D>().velocity = transform.up * jumpVelocity;
        //odpalenie animacji skoku i wylaczenie animacji chodzenia
        anim.SetTrigger("jump");
        anim.SetBool("isWalking", false);
    }*/
    void Update()
    {
        if (sideHit)
        {
            dist = Vector2.Distance(transform.position, burger.transform.position);

            if (dist < 0.01f)
            {
                Debug.Log("bounce");
                burger.GetComponent<spawn_kotlet>().Bounce();
            }
            else
            {
                burger.gameObject.transform.position = Vector3.MoveTowards(burger.transform.position, transform.position, speed * Time.deltaTime);
            }
        }
    }
}
