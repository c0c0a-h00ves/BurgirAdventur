using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleCollider : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject burger = GameObject.Find("burger");
        if(collision.gameObject == burger)
        {
            burger.GetComponent<burgier_death>().Death();
        }
    }
}
