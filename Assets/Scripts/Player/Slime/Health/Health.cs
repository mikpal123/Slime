using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Enemies and player use the same script for hp, so its important to create triggers in animation with exactly same name
public class Health : MonoBehaviour
{
    //variables available in editor
    [SerializeField] private float startingHealth;          //How much HP player will have on staart
    [SerializeField] private float healTimer;               //how long character will regenerae 1hp cointainer
    [SerializeField] private bool canAutoHeal;               //if true character can regenerate HP

    //variables unavailable in editor
    public float currentHealth { get; private set; }        //Current HP of character
    private Animator anim;                                  //Anim reference
    public bool dead { get; private set; }                  //if true character is dead

    private void Awake()
    {   //setup variables and get references
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        StartCoroutine(HealTimer());
    }
   
   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))                   //just to check dmg is working
            TakeDamage(1);

    }

    //Player will automatily heal for a certain amount
    IEnumerator HealTimer()
    {
        WaitForSeconds wait = new WaitForSeconds(healTimer);    //start timer
        while (!dead)                                           //if character is't dead
        {
            Heal(1);                                            //restore 1 HP
            yield return wait;
        }      
    }

    //Taking damage
    public void TakeDamage(float _damage)
    {
        //Character hurt
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth); //Change character HP, the value will not exceed clamp
        if (currentHealth > 0)                                                   //Check if character still have more than 0 HP
        {                                                                        //if true
            anim.SetTrigger("hurt");                                             //start anim
        }
        else                                                                    //if false
        {
            //player die
            if (!dead)                                                          //make sure the player die once
            {
                anim.SetTrigger("die");                                         //start anim
                //for player
                if(GetComponent<PlayerMovement>() != null)                      //check if its not null
                {
                    GetComponent<PlayerMovement>().enabled = false;             //disable every usable component
                    GetComponent<PlayerAttack>().enabled = false;               //
                    GetComponent<WaterJetpack>().enabled = false;               //
                    GetComponent<PlayerEating>().enabled = false;               //
                }


                //for enemy
                if (GetComponent<PlayerMovement>() != null)                     //check if its not null
                {
                    GetComponent<MeleEnemy>().enabled = false;

                }                      
                   



                dead = true;                                                    //make sure player is dead :p
            }

        }
    }

    //Healing
    public void Heal(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth + _damage, 0, startingHealth); //Change character HP, the value will not exceed clamp
    }

}
