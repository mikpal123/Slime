using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemy : MonoBehaviour
{   
    [SerializeField] private float attackCd; //Enemy attack cooldown
    [SerializeField] private int damage; //Enemy attack damage
    [SerializeField] private Health playerHealth; //Player HP reference
    private bool isAttacking; //true if enemy is attacking now
    private bool onCd; // true if enemy is still on attack cooldown
    private bool canHitPlayer;// true if player is still in range of Attack
    private Animator anim; //reference to enemy anim

    
    void Awake()
    { 
        anim = GetComponent<Animator>(); 
    }

  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player") //when enemy enter collison with enemy
        {
            canHitPlayer = true;                  //can hit player
            if (!onCd)                            //check if enemy attack is on cooldown. If no
            {                           
                anim.SetTrigger("attack");        //Start anim attack
                onCd = true;                      //set cd on attack 
                StartCoroutine(AttackCdTimer());  //Start timer to reset attack cd
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canHitPlayer = false;                   //check if player is still in range of attack
    }


    private void DealDamage()
    {
        if(canHitPlayer)                        //if player is still in range of attack
        playerHealth.TakeDamage(damage);        //deal damage to player
    }

    IEnumerator AttackCdTimer()
    {   
        WaitForSeconds wait = new WaitForSeconds(attackCd);     //start timer to reset cd od attack
        onCd = false;                                           //when timer end counting down enemy can attack again
        yield return wait;
    }

    private void IsAtacking(float _value)
    {
        if (_value != 0)
            anim.SetBool("isAttacking", true);
        else
            anim.SetBool("isAttacking", false);
    }
}
