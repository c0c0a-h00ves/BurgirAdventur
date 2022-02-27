using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_kotlet : MonoBehaviour
{
    [SerializeField] kotlet kotletScript;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            kotletScript.SpawnKotlet(gameObject);
        }
    }
}
