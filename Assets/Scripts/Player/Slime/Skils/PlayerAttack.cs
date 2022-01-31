using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //variables available in editor
    [SerializeReference] private float attackCooldown;          //Player Attack speed
    [SerializeReference] private Transform firePoint;           //Reference to point from where all projectiles will be spawn
    [SerializeReference] private GameObject[] WaterAttack;      //Reference to water projectile

    //variables unavailable in editor
    private Animator anim;                                      //Reference to player anim                     
    private PlayerEating playerEating;                          //Reference to player Eating skill
    private float cooldownTimer = Mathf.Infinity;               //cooldown timer for player attack


    //Do every time when script instance is being loaded
    private void Awake()
    {   //take references from object
        anim = GetComponent<Animator>();
        playerEating = GetComponent<PlayerEating>();
    }
    

    // Update is called once per frame
    void Update()
    {
        //chceck if player can attack
        if (Input.GetMouseButton(0)                             //when player press LMB
            && cooldownTimer > attackCooldown                   //attack isn't on cd
            && GetComponent<PlayerMovement>().canAttack()       //any movement isn't blocking player from attack
            && playerEating.waterOwned > 0)                     //and have enought watter to fire 
            Attack();                                           //then attack
        
        cooldownTimer += Time.deltaTime;                        //decrese CD of attack
    }

    private void Attack()
    {
        playerEating.waterOwned += -0.1f;                       //take some watter from player to fire
        anim.SetTrigger("attack");                              //start anim
        cooldownTimer = 0;                                      //start cd of attack

        //taking free prefab and making transform
        WaterAttack[FindWaterAttacks()].transform.position = firePoint.position;                                        //set position for projectile. It will always fire from fire point 
        WaterAttack[FindWaterAttacks()].GetComponent<WatterAttack>().SetDirection(Mathf.Sign(transform.localScale.x));  //st direcion for projecetile. It will always fire to direction looking by player

    }

    //search for a free prefab to eject
    private int FindWaterAttacks()
    {
        for (int i = 0; i < WaterAttack.Length; i++)
        {
            if (!WaterAttack[i].activeInHierarchy)              //check if there are any free projectiles to fire
                return i;
        }
        return 0;
    }

}
