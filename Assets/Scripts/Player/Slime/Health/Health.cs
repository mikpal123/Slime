using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //variables available in editor
    [SerializeField] private float startingHealth;
    [SerializeField] private float healTimer; //in seconds

    //variables unavailable in editor
    public float currentHealth { get; private set; }
    private Animator anim;
    public bool dead { get; private set; }

    private void Awake()
    {   //setup variables and get references
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        StartCoroutine(HealTimer());
    }
   
   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) //just to check dmg is working
            TakeDamage(1);

    }

    //Player will automatily heal for a certain amount
    IEnumerator HealTimer()
    {
        WaitForSeconds wait = new WaitForSeconds(healTimer);
        while (!dead)
        {
            Heal(1);
            yield return wait;
        }      
    }
    public void TakeDamage(float _damage)
    {
        //player hurt
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth); //hp dont go above or below this clamp
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt"); //start anim
        }
        else
        {
            //player die
            if (!dead) //make sure the player die once
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }

        }
    }
    public void Heal(float _damage)
    {
        //player hurt
        currentHealth = Mathf.Clamp(currentHealth + _damage, 0, startingHealth); //hp dont go above or below this clamp
    }

}
