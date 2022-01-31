using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEating : MonoBehaviour
{

    [SerializeReference] private GameObject EatingObject;               //Eating objecet reference                                              
    private PlayerMovement playerMovement;                              //Player movement refernce
    public float waterOwned;                                            //How much water player currently have
    public bool isEating { get; private set; }                          //true if player is using this skill now

    // Start is called before the first frame update
    void Start()
    {
        //take references from object and setup basic variables
        playerMovement = GetComponent<PlayerMovement>();
        isEating = false;
        waterOwned = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerMovement.CanEat())     //when player press 'e' and any movement isn't blocking player from use skill
        {                                                               //if true
            isEating = true;                                            //player currently using this skill
            EatingObject.SetActive(true);                               //show eating skill                        
            GetComponent<PlayerAttack>().enabled = false;               //disable attack  

        }
        if (Input.GetKeyUp(KeyCode.E) || !playerMovement.CanEat())      //when player release 'e' OR any movement will blocking player from use skill
        {
            isEating=false;                                             //player stops using the skill
            EatingObject.SetActive(false);                              //hide eating skill
            GetComponent<PlayerAttack>().enabled = true;                //enable attack  
        }
        if (isEating)                                                   //when player using skill
        {
            Eating();                                                   //Start eat 
        }
    }

    //Eating skill. Mainly check with what objects skill colides
    private void Eating()
    {
        if (FindObjectOfType<Water>().canGaingWater)                    //if collides with water
        {
            EatingWatter();                                             //Start gainging water
        }
    }

    //Currently only refil your water value
    private void EatingWatter()  
    {   
        if(waterOwned <=10)                                             //check if can gain more water
        waterOwned += 0.01f;                                            //if true, gain more water
    }

}
