using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{   //variables available in editor
    [SerializeField] private float movementResistance;

    //variables unavailable in editor
    private PlayerMovement playerMovement;  
    private float basicMovement=1;
    private bool inWater = false;

    void Awake()
    {
        //take references from object
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        //change movement speed if player is in water
        if (collision.gameObject.tag == "Player")
        {
            basicMovement = movementResistance;
            inWater = true;
            
        }          
    }

    private void OnTriggerExit2D(Collider2D collision)
    {   
        //change movement speed if player is out of water
        if (collision.gameObject.tag == "Player")
        {
            basicMovement = 1;
            inWater = false;
           
        }          
    }

    //just for returning basicMovement value (default is 1)
    public float BasicMovement()
    { 
        return basicMovement; 
    }

    //just for returning grounded value
    public bool IsInWater()
    {
        return inWater;
    }
}
