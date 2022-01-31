using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{   //variables available in editor
    [SerializeField] private float movementResistance;      //devine player movement speed by that value, so it must be higher than 0

    //variables unavailable in editor
    private float basicMovement=1;                          //basic value of player movement
    private bool inWater  = false ;                           //true if player is in watter
    public bool canGaingWater { get; private set; }         //true if player is near watter


    private void OnTriggerEnter2D(Collider2D collision)
    {   
        //change movement speed if player is in water
        if (collision.gameObject.tag == "Player")           //check if player start colison with watter
        {                                                   //if true
            basicMovement = movementResistance;             //change player movement speed
            inWater = true;                                 //set player is in water
        }
        else if (collision.gameObject.tag == "Eating")      //if player start colision with eating skill
        {
            canGaingWater = true;                           //player can refil water
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {   
        //change movement speed if player is out of water
        if (collision.gameObject.tag == "Player")           //check if player end colison with watter
        {                                                   //if true
            basicMovement = 1;                              //reset player movement speed to default value
            inWater = false;                                //and set player is out of watter

        }   
        else if(collision.gameObject.tag == "Eating")       //if player end colision with eating skill
        {
            canGaingWater = false;                          //player can't refil water
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
