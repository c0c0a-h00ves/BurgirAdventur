using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class spawn_kotlet : MonoBehaviour
{
    [SerializeField] float jumpVelocity;
    [SerializeField] kotlet kotletScript;

    private Vector2 boxCoordinates;
    private float spawnDistance = 0.5f;
    private float facing_direction;
    private float direction;
    public bool isOnKotlet = false;
    kotlet kotlet;
    void Start()
    {
        
    }
    public bool SpawnKotlet()
    {
        //coordy gdzie bedzie kotlet
        boxCoordinates = new Vector2(transform.position.x + (spawnDistance * facing_direction), transform.position.y);
        //sprawdzenie czy nie probuje sie zrespic w srodku innego collidera i zrespienie kotleta
        if (!Physics2D.OverlapBox(boxCoordinates, kotletScript.transform.localScale, kotletScript.transform.eulerAngles.z) && isOnKotlet == false)
        {
            if (!kotlet)
            {
                //jak patrzy w prawo to sie respi po prawo jak nie to po lewo
                kotlet = Instantiate(kotletScript, boxCoordinates, Quaternion.identity);
                Destroy(kotlet.gameObject, 5f);
                return true;
            }
        }
        return false;
    }
    public void Bounce()
    {
        GetComponent<burgier_movement>().enabled = true;
        kotlet.GetComponent<kotlet>().sideHit = false;
        Debug.Log(kotletScript.sideHit);
        //dodanie gornego velocity
        GetComponent<Rigidbody2D>().velocity = transform.up * jumpVelocity;
        //odpalenie animacji skoku i wylaczenie animacji chodzenia
        GetComponentInChildren<Animator>().SetTrigger("jump");
        GetComponentInChildren<Animator>().SetBool("isWalking", false);
    }
    void Update()
    {
        direction = gameObject.GetComponent<burgier_movement>().direction.x;
        if(direction != 0.0f)
        {
            if(direction > 0.0f)
            {
                facing_direction = 1;
            }
            else
            {
                facing_direction = -1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnKotlet();
        }
    }
}
