using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleCollider : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject burger = GameObject.Find("PlayerPrefab");
        if(collision.gameObject == burger)
        {
            Debug.Log("dead");
            burger.GetComponent<burgier_death>().Death();
            burger.GetComponent<burgier_death>().isDead = true;
        }
        return;
    }
}
