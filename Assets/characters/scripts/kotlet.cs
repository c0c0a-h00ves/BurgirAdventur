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
            vectorDifference = collision.gameObject.transform.position - transform.position;
            collisionSide = vectorDifference.normalized;
            burger = collision.gameObject;
            burger.GetComponent<spawn_kotlet>().isOnKotlet = true;
            if (collisionSide.x == -1.0f || collisionSide.x == 1.0f)
            {
                Debug.Log("side");
                sideHit = true;
                burger.GetComponent<burgier_movement>().enabled = false;
            }
            else
            {
                burger.GetComponent<spawn_kotlet>().Bounce();
                Debug.Log("not side");
            }
        }
    }
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
